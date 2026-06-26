using E_Commerce.Domain.Entities.ProductNodule;
using E_Commerce.Shared;
using System.Linq.Expressions;

namespace E_Commerce.Services.Specifications
{
    internal static class ProductSpecificationHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParamas queryParamas)
        {
            return P => (!queryParamas.BrandId.HasValue || P.BrandId == queryParamas.BrandId.Value)
            && (!queryParamas.TypeId.HasValue || P.TypeId == queryParamas.TypeId.Value)
            && (string.IsNullOrEmpty(queryParamas.Search) || P.Name.ToLower().Contains(queryParamas.Search.ToLower()));
        
        }
    }
}
