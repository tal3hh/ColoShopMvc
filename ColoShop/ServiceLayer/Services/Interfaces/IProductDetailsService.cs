using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs.Product;
using ServiceLayer.DTOs.ProductDetails;
using ServiceLayer.Utilities.Paginations;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductDetailsService
    {
        Task<List<ProductDetailsDto>> GetAllAsync();
        Task<List<ProductDto>> GetProductAllAsync();
        Task<List<ProductDetailsDto>> GetIncludeAllAsync();
        Task<Paginate<ProductDetailsDto>> GetPaginateIncludeAsync(int page, int take);
        Task CreateAsync(ProductDetailsCreateDto dto);
        Task<ProductDetailsDto> GetById(int id);
        Task<ProductDetailsUpdateDto> GetUpdateById(int id);
        Task Update(ProductDetailsUpdateDto dto);
        Task Remove(int id);
    }
}
