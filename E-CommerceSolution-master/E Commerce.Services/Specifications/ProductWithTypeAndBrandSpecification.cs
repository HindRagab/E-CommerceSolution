using E_Commerce.Domain.Entities.ProductNodule;
using E_Commerce.Shared;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product , int>
    {
        // Get Product By Id
        public ProductWithTypeAndBrandSpecification(int id) : base(P => P.Id == id)
        {
            AddINclude(P => P.ProductType);
            AddINclude(P => P.ProductBrand);
        }
        // Get All Products
        public ProductWithTypeAndBrandSpecification(ProductQueryParamas queryParamas)
            : base(ProductSpecificationHelper.GetProductCriteria(queryParamas))
        {
            AddINclude(P => P.ProductType);
            AddINclude(P => P.ProductBrand);

            switch(queryParamas.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    AddOrderBy(X => X.Id);
                    break;
            }

            ApplyPagination(queryParamas.PageSize, queryParamas.PageIndex);
        }
    }
}
