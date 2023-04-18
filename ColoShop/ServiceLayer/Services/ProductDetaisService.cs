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
using ServiceLayer.DTOs.Product;
using ServiceLayer.DTOs.ProductDetails;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Utilities.Paginations;

namespace ServiceLayer.Services
{
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public ProductDetailsService(IUow uow, IMapper mapper, AppDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }

     



        public async Task<List<ProductDetailsDto>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<ProductDetails>().AllOrderByAsync(x => x.Id, false);

            return _mapper.Map<List<ProductDetailsDto>>(entities);
        }

        public async Task<Paginate<ProductDetailsDto>> GetPaginateIncludeAsync(int page, int take)
        {
            var entities1 = from p in _context.ProductDetails.Include(x => x.Product).AsNoTracking().OrderByDescending(x=> x.Id)
                            select p;

            //Paginate
            var allCount = await _context.ProductDetails.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();

            var dtos = _mapper.Map<List<ProductDetailsDto>>(entities2);

            var Dtos = new Paginate<ProductDetailsDto>(dtos, page, Totalpage);

            return Dtos;
        }


        public async Task<List<ProductDetailsDto>> GetIncludeAllAsync()
        {
            var entities = await _context.Set<ProductDetails>().Include(x => x.Product).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
            var dtolist = _mapper.Map<List<ProductDetailsDto>>(entities);

            foreach (var entity in entities)
            {
                foreach (var dto in dtolist)
                {
                    if (dto.ProductID == entity.Id)
                    {
                        dto.ProductName = entity.Product.Name;
                    }
                }
            }

            return dtolist;
        }

        public async Task<List<ProductDto>> GetProductAllAsync()
        {
            var entities = await _uow.GetRepository<Product>().AllFilterAsync(x=> x.ProductDetails ==null);

            return _mapper.Map<List<ProductDto>>(entities);
        }

        public async Task CreateAsync(ProductDetailsCreateDto dto)
        {
            var entity = _mapper.Map<ProductDetails>(dto);

            if (entity != null)
            {
                await _uow.GetRepository<ProductDetails>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<ProductDetailsUpdateDto> GetUpdateById(int id)
        {
            var entity = await _uow.GetRepository<ProductDetails>().FindAsync(id);

            return _mapper.Map<ProductDetailsUpdateDto>(entity);
        }

        public async Task<ProductDetailsDto> GetById(int id)
        {
            var entity = await _context.ProductDetails.Include(x => x.Product).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var dto = _mapper.Map<ProductDetailsDto>(entity);
            dto.ProductName = entity.Product.Name;

            return dto;
        }

        public async Task Update(ProductDetailsUpdateDto dto)
        {
            var unchangedentity = await _uow.GetRepository<ProductDetails>().FindAsync(dto.Id);
            if (unchangedentity != null)
            {
                var entity = _mapper.Map<ProductDetails>(dto);
                _uow.GetRepository<ProductDetails>().Update(entity, unchangedentity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<ProductDetails>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<ProductDetails>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
