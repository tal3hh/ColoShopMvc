using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs.BlogComment;

namespace ServiceLayer.Services.Interfaces
{
    public interface IBlogCommentService
    {
        Task<List<BlogCommentDto>> GetAllAsync();
        Task<BlogCommentDto> GetById(int id);
        Task Remove(int id);
    }
}
