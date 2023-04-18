using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs.Contact;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class ContactService : IContactService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public ContactService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ContactDto>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<Contact>().AllOrderByAsync(x => x.Id, false);

            return _mapper.Map<List<ContactDto>>(entities);
        }

        public async Task CreateAsync(ContactDto dto)
        {
            if (dto != null)
            {
                var entity = _mapper.Map<Contact>(dto);

                await _uow.GetRepository<Contact>().CreateAsync(entity);

                await _uow.SaveChangesAsync();
            }
            
        }

        public async Task<ContactDto> GetById(int id)
        {
            var entity = await _uow.GetRepository<Contact>().FindAsync(id);

            return _mapper.Map<ContactDto>(entity);
        }

       
        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<Contact>().FindAsync(id);
            if (entity != null)
            {
                _uow.GetRepository<Contact>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
