using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs.Category;

namespace ServiceLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task CreateAsync(CategoryCreateDto dto);
        Task<CategoryDto> GetById(int id);
        Task<CategoryUpdateDto> GetByIdUpdate(int id);
        Task Update(CategoryUpdateDto dto);
        Task Remove(int id);
    }
}
