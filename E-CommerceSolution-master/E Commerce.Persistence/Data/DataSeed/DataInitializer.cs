using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderModule;
using E_Commerce.Domain.Entities.ProductNodule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                var HasProducts = await _dbContext.Products.AnyAsync();
                var HasBrands = await _dbContext.ProductBrands.AnyAsync();
                var HasTypes = await _dbContext.ProductTypes.AnyAsync();
                var HasDeliveryMethods = await _dbContext.Set<DeliveryMethod>().AnyAsync();   
                if (HasProducts && HasBrands && HasTypes && HasDeliveryMethods) return;

                if (!HasBrands)
                    await SeedDataFromJsonAsync<ProductBrand, int>("brands.json" , _dbContext.ProductBrands);
                if (!HasTypes)
                    await SeedDataFromJsonAsync<ProductType, int>("types.json" , _dbContext.ProductTypes);
                await _dbContext.SaveChangesAsync();
                if (!HasProducts)
                    await SeedDataFromJsonAsync<Product, int>("products.json" , _dbContext.Products);
                if (!HasDeliveryMethods)
                    await SeedDataFromJsonAsync<DeliveryMethod, int>("delivery.json" , _dbContext.Set<DeliveryMethod>());
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Data Seeding Failed : {ex}");
            }
        }

        private async Task SeedDataFromJsonAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\E Commerce.Persistence\Data\DataSeed\JSONFiles\" + fileName;
            if (!File.Exists(FilePath)) throw new FileNotFoundException($"File {fileName} is not Exists");
            try
            {
                using var dataStream = File.OpenRead(FilePath);

                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data is not null)
                {
                    await dbset.AddRangeAsync(data);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error While Reading JSON File: {ex}");
            }
        }
    }
}
