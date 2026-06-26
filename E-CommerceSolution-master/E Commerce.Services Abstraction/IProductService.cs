using E_Commerce.Shared;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.ProductDTOs;

namespace E_Commerce.Services_Abstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryParamas queryParamas);

        Task<Result<ProductDTO>> GetProductAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
