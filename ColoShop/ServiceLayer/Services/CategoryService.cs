using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs.Category;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public CategoryService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<Category>().AllOrderByAsync(x => x.Id, false);

            return _mapper.Map<List<CategoryDto>>(entities);
        }

        public async Task CreateAsync(CategoryCreateDto dto)
        {
            var entity = _mapper.Map<Category>(dto);

            if (entity != null)
            {
                await _uow.GetRepository<Category>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var entity = await _uow.GetRepository<Category>().FindAsync(id);

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<CategoryUpdateDto> GetByIdUpdate(int id)
        {
            var entity = await _uow.GetRepository<Category>().FindAsync(id);

            return _mapper.Map<CategoryUpdateDto>(entity);
        }

        public async Task Update(CategoryUpdateDto dto)
        {
            var unchangedentity = await _uow.GetRepository<Category>().FindAsync(dto.Id);

            if (unchangedentity != null)
            {
                var entity = _mapper.Map<Category>(dto);
                _uow.GetRepository<Category>().Update(entity, unchangedentity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<Category>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<Category>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
