using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;
using ServiceLayer.DTOs.EnumSize;
using ServiceLayer.DTOs.Product;
using ServiceLayer.Utilities.Paginations;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<List<ProductDto>> GetIncludeAllAsync();
        SizeViewModel GetSizeEnum();
        Task<Paginate<ProductDto>> GetSearchDashboardAsync(string productSearch, int page, int take);
        Task<Paginate<ProductDto>> GetAllGenderAsync(int genderID, string sortOrder, int page, int take);
        Task<Paginate<ProductDto>> GetAllGenderANDCategoryAsync(int genderID, int categoryID, string sortOrder, int page, int take);
        Task<Paginate<ProductDto>> GetAllContrllerFilterAsync(string sortOrder, int page, int take);
        Task<Paginate<ProductDto>> GetAllSizeAsync(List<SizeModel> sizeFilter, string sortOrder, int page, int take);
        Task<Paginate<ProductDto>> GetAllCategoryAsync(int categoryID,string sortOrder,int page, int take);
        Task<Paginate<ProductDto>> GetAllDashboardFilterAsync(string sortOrder,int page, int take);
        Task CreateAsync(ProductCreateDto dto);
        Task<ProductDto> GetById(int id);
        Task<ProductUpdateDto> GetByIdUpdate(int id);
        Task Update(ProductUpdateDto dto);
        Task Remove(int id);
    }
}
