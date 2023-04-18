using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs.ProductComment;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductCommentService
    {
        Task<List<ProductCommentDto>> GetAllAsync();
        Task<ProductCommentDto> GetById(int id);
        Task<List<ProductCommentDto>> GetByIdAllAsync(int ProductId);
        Task Remove(int id);
        Task CreateAsync(ProductCommentDto dto);
    }
}
