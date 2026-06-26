using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductNodule;
using E_Commerce.Services.Exceptions;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.ProductDTOs;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryParamas queryParamas)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Spec = new ProductWithTypeAndBrandSpecification(queryParamas);
            var Products = await Repo.GetAllAsync(Spec);
            var DataToReturn = _mapper.Map<IEnumerable<ProductDTO>>(Products);
            var CountOfReturnedData = DataToReturn.Count();
            var CountSpec = new ProductCountSpecification(queryParamas);
            var CountOfAllProducts = await Repo.CounAsync(CountSpec);
            return new PaginatedResult<ProductDTO>(queryParamas.PageIndex, CountOfReturnedData, CountOfAllProducts, DataToReturn);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(types);
        }

        public async Task<Result<ProductDTO>> GetProductAsync(int id)
        {
            var Spec = new ProductWithTypeAndBrandSpecification(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsunc(Spec);
            if (Product is null)
                return Error.NotFound("Product.NotFound", $"Product With Id {id} Is Not Found");
            return Result<ProductDTO>.Ok(_mapper.Map<ProductDTO>(Product));
            // Implict Casting To Result<ProductDTO>.Fail - Result<ProductDTO>.Ok
        }
    }
}
