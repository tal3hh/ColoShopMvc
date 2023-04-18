using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs.Blog;
using ServiceLayer.Utilities.Paginations;

namespace ServiceLayer.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogDto>> GetAllAsync();
        Task<Paginate<BlogDto>> GetAllPaginateAsync(int page, int take);
        Task CreateAsync(BlogCreateDto dto);
        Task<BlogDto> GetById(int id);
        Task Update(BlogUpdateDto dto);
        Task Remove(int id);
        Task<BlogUpdateDto> GetByUpdateId(int id);
    }
}
