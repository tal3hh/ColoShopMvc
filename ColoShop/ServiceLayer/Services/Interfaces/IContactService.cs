using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs.Contact;

namespace ServiceLayer.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetAllAsync();
        Task CreateAsync(ContactDto dto);
        Task<ContactDto> GetById(int id);
        Task Remove(int id);
    }
}
