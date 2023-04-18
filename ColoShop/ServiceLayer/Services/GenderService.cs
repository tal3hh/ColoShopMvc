using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs.Gender;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class GenderService : IGenderService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public GenderService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<GenderDto>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<Gender>().AllOrderByAsync(x => x.Id, false);

            return _mapper.Map<List<GenderDto>>(entities);
        }

        public async Task CreateAsync(GenderCreateDto dto)
        {   
            var entity = _mapper.Map<Gender>(dto);

            if (entity != null)
            {
                await _uow.GetRepository<Gender>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<GenderDto> GetById(int id)
        {
            var entity = await _uow.GetRepository<Gender>().FindAsync(id);

            return _mapper.Map<GenderDto>(entity);
        }

        public async Task<GenderUpdateDto> GetByIdUpdate(int id)
        {
            var entity = await _uow.GetRepository<Gender>().FindAsync(id);

            return _mapper.Map<GenderUpdateDto>(entity);
        }

        public async Task Update(GenderUpdateDto dto)
        {
            var unchangedentity = await _uow.GetRepository<Gender>().FindAsync(dto.Id);

            if (unchangedentity != null)
            {
                var entity = _mapper.Map<Gender>(dto);
                _uow.GetRepository<Gender>().Update(entity, unchangedentity);
                await _uow.SaveChangesAsync();
            }           
        }

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<Gender>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<Gender>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
