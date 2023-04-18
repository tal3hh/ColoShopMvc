using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs.ProductComment;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public ProductCommentService(IUow uow, IMapper mapper, AppDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductCommentDto>> GetAllAsync()
        {
            var entities = await _context.ProductComments.Include(x => x.Product).OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync();

            return _mapper.Map<List<ProductCommentDto>>(entities);
        }


        public async Task<List<ProductCommentDto>> GetByIdAllAsync(int ProductId)
        {
            var entities = await _context.ProductComments.Where(x => x.ProductID == ProductId).Include(x => x.Product).OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync();

            return _mapper.Map<List<ProductCommentDto>>(entities);
        }

        public async Task CreateAsync(ProductCommentDto dto)
        {
            var entity = _mapper.Map<ProductComment>(dto);

            await _uow.GetRepository<ProductComment>().CreateAsync(entity);

            await _uow.SaveChangesAsync();
        }

        public async Task<ProductCommentDto> GetById(int id)
        {
            var entity = await _uow.GetRepository<ProductComment>().FindAsync(id);

            return _mapper.Map<ProductCommentDto>(entity);
        }

        //public async Task Update(ProductCommentUpdateDto dto)
        //{
        //    var entity = _mapper.Map<ProductComment>(dto);

        //    _uow.GetRepository<ProductComment>().Update(entity);

        //    await _uow.SaveChangesAsync();
        //}

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<ProductComment>().FindAsync(id);

            _uow.GetRepository<ProductComment>().Remove(entity);

            await _uow.SaveChangesAsync();

        }
    }
}
