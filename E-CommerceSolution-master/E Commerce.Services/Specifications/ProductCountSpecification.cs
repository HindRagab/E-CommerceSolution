using E_Commerce.Domain.Entities.ProductNodule;
using E_Commerce.Shared;

namespace E_Commerce.Services.Specifications
{
    internal class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductQueryParamas queryParamas)
            : base(ProductSpecificationHelper.GetProductCriteria(queryParamas))
        {
            
        }
    }
}
