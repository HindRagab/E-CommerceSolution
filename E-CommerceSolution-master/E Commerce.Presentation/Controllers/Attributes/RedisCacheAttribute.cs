using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace E_Commerce.Presentation.Controllers.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMin;

        public RedisCacheAttribute(int DurationInMin = 5)
        {
            _durationInMin = DurationInMin;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get Cache Service from DI Container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            // Create Cache Key Based On Request Path and Query String
            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            // Check If Cached Data Exists
            var cacheValue = await cacheService.GetAsync(cacheKey);

            // If Exists, Return Cached Data and Skip Execution Of EndPoint
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // If Not Exists, Execute The EndPoint and Store The Result in Cache if 200 OK Response
            var ExecutedContext = await next.Invoke();
            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey, result.Value!, TimeSpan.FromMinutes(_durationInMin));
            }

        }

        // /api/Products?brandId=2&typeId=1
        // /api/Products?typeId=1
        // /api/Products?typeId=1&brandId=2
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path); // api/Products
            foreach (var item in request.Query.OrderBy(X => X.Key))
                Key.Append($"{item.Key}-{item.Value}");
            return Key.ToString();
        }
    }
}
