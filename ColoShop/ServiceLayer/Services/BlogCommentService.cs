using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs.BlogComment;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public BlogCommentService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<BlogCommentDto>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<BlogComment>().AllOrderByAsync(x => x.Id, false);

            return _mapper.Map<List<BlogCommentDto>>(entities);
        }

        //public async Task CreateAsync(BlogCommentCreateDto dto)
        //{
        //    var entity = _mapper.Map<BlogComment>(dto);

        //    await _uow.GetRepository<BlogComment>().CreateAsync(entity);

        //    await _uow.SaveChangesAsync();
        //}

        public async Task<BlogCommentDto> GetById(int id)
        {
            var entity = await _uow.GetRepository<BlogComment>().FindAsync(id);

            return _mapper.Map<BlogCommentDto>(entity);
        }

        //public async Task Update(BlogCommentUpdateDto dto)
        //{
        //    var entity = _mapper.Map<BlogComment>(dto);

        //    _uow.GetRepository<BlogComment>().Update(entity);

        //    await _uow.SaveChangesAsync();
        //}

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<BlogComment>().FindAsync(id);

            _uow.GetRepository<BlogComment>().Remove(entity);

            await _uow.SaveChangesAsync();

        }
    }
}
