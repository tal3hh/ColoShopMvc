using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs.Blog;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Utilities.Paginations;

namespace ServiceLayer.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public BlogService(IUow uow, IMapper mapper, IWebHostEnvironment env, AppDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _env = env;
            _context = context;
        }

        public async Task<Paginate<BlogDto>> GetAllPaginateAsync(int page, int take)
        {
            var entities1 = from p in _context.Blogs 
                            select p;

            //Paginate
            var allCount = await _context.Blogs.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();

            var dtos = _mapper.Map<List<BlogDto>>(entities2);

            var Dtos = new Paginate<BlogDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<List<BlogDto>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<Blog>().AllOrderByAsync(x => x.Id, false);

            return _mapper.Map<List<BlogDto>>(entities);
        }
       

        public async Task<BlogDto> GetById(int id)
        {
            var entity = await _uow.GetRepository<Blog>().FindAsync(id);

            return _mapper.Map<BlogDto>(entity);
        }


        public async Task<BlogUpdateDto> GetByUpdateId(int id)
        {
            var entity = await _uow.GetRepository<Blog>().FindAsync(id);

            return _mapper.Map<BlogUpdateDto>(entity);
        }


        public async Task CreateAsync(BlogCreateDto dto)
        {
            var entity = _mapper.Map<Blog>(dto);
            if (entity != null)
            {
                string filename = Guid.NewGuid().ToString() + "_" + entity.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", filename);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await entity.Photo.CopyToAsync(stream);
                }
                entity.Image = filename;

                await _uow.GetRepository<Blog>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }


        public async Task Update(BlogUpdateDto dto)
        {
            var DbEntity = await _uow.GetRepository<Blog>().FindAsync(dto.Id);
            if ((DbEntity != null) || (dto != null))
            {
                string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", DbEntity.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + dto.Photo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", fileName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }
                dto.Image = fileName;

                var entity = _mapper.Map<Blog>(dto);
                _uow.GetRepository<Blog>().Update(entity, DbEntity);
                await _uow.SaveChangesAsync();
            }
        }


        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<Blog>().FindAsync(id);

            if (entity  != null)
            {
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/blog", entity.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                _uow.GetRepository<Blog>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
