using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs.Gender;

namespace ServiceLayer.Services.Interfaces
{
    public interface IGenderService
    {
        Task<List<GenderDto>> GetAllAsync();
        Task CreateAsync(GenderCreateDto dto);
        Task<GenderDto> GetById(int id);
        Task<GenderUpdateDto> GetByIdUpdate(int id);
        Task Update(GenderUpdateDto dto);
        Task Remove(int id);
    }
}
