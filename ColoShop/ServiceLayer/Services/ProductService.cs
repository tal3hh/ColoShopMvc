using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs.EnumSize;
using ServiceLayer.DTOs.Product;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Utilities.Paginations;

namespace ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IProductDetailsService _productDetailsService;
        public ProductService(IUow uow, IMapper mapper, AppDbContext context, IWebHostEnvironment env, IProductDetailsService productDetailsService = null)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
            _env = env;
            _productDetailsService = productDetailsService;
        }

        public async Task<Paginate<ProductDto>> GetSearchDashboardAsync(string productSearch,int page, int take)
        {
            var entities1 = from p in _context.Products.Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails)
                            select p;

            if (!String.IsNullOrEmpty(productSearch))
            {
                 entities1 = from p in _context.Products.Where(x=> x.Name.Contains(productSearch)).Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails)
                             select p;
            }

            //Paginate
            var allCount = await _context.Products.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();

            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            foreach (var entity in entities2)
            {
                foreach (var dto in dtos)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Availability = entity.ProductDetails.Availability;
                            dto.Count = entity.ProductDetails.Count;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Color = entity.ProductDetails.Color;
                            dto.Size = entity.ProductDetails.Size;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }
        public async Task<Paginate<ProductDto>> GetAllDashboardFilterAsync(string sortOrder,int page, int take)
        {
            
            var entities1 = from p in _context.Products.Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails)
                            select p;

            //Sort Filter
            switch (sortOrder)
            {
                case "count_desc":
                    entities1 = entities1.OrderByDescending(x => x.ProductDetails.Count);
                    break;
                case "count_asc":
                    entities1 = entities1.OrderBy(x => x.ProductDetails.Count);
                    break;
                case "price_desc":
                    entities1 = entities1.OrderByDescending(x => x.Price);
                    break;
                default:
                    entities1 = entities1.OrderBy(x => x.Price);
                    break;
            }


            //Paginate
            var allCount = await _context.Products.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();

            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            foreach (var entity in entities2)
            {
                foreach (var dto in dtos)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Availability = entity.ProductDetails.Availability;
                            dto.Count = entity.ProductDetails.Count;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Color = entity.ProductDetails.Color;
                            dto.Size = entity.ProductDetails.Size;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }
        public async Task<Paginate<ProductDto>> GetAllGenderANDCategoryAsync(int genderID,int categoryID , string sortOrder, int page, int take)
        {

            var entities1 = from p in _context.Products.Where(x => x.GenderID == genderID).Where(x=> x.CategoryID == categoryID).Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails)
                            select p;

            //Sort Filter
            switch (sortOrder)
            {
                case "reyting_desc":
                    entities1 = entities1.OrderByDescending(x => x.ProductDetails.StarCount);
                    break;
                case "reyting_asc":
                    entities1 = entities1.OrderBy(x => x.ProductDetails.StarCount);
                    break;
                case "price_desc":
                    entities1 = entities1.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities1 = entities1.OrderBy(x => x.Price);
                    break;
                default:
                    break;
            }

            //Paginate
            var allCount = await _context.Products.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();


            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            foreach (var entity in entities2)
            {
                foreach (var dto in dtos)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Availability = entity.ProductDetails.Availability;
                            dto.Count = entity.ProductDetails.Count;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Color = entity.ProductDetails.Color;
                            dto.Size = entity.ProductDetails.Size;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }
        public SizeViewModel GetSizeEnum()
        {
            var Size = new SizeViewModel();
            Size.CheckBoxItems = new List<SizeModel>();

            foreach (var size in Enum.GetValues(typeof(Sizes)))
            {
                Size.CheckBoxItems.Add(new SizeModel() { Size = (Sizes)size, IsSelected = false });
            }

            return Size;
        }
        public async Task<Paginate<ProductDto>> GetAllContrllerFilterAsync(string sortOrder, int page, int take)
        {

            var entities1 = from p in _context.Products.Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails)
                            select p;

            //Sort Filter
            switch (sortOrder)
            {
                case "reyting_desc":
                    entities1 = entities1.OrderByDescending(x => x.ProductDetails.StarCount);
                    break;
                case "reyting_asc":
                    entities1 = entities1.OrderBy(x => x.ProductDetails.StarCount);
                    break;
                case "price_desc":
                    entities1 = entities1.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities1 = entities1.OrderBy(x => x.Price);
                    break;
                default:
                    break;
            }


            //Paginate
            var allCount = await _context.Products.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();



            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            foreach (var entity in entities2)
            {
                foreach (var dto in dtos)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Availability = entity.ProductDetails.Availability;
                            dto.Count = entity.ProductDetails.Count;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Color = entity.ProductDetails.Color;
                            dto.Size = entity.ProductDetails.Size;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<Paginate<ProductDto>> GetAllSizeAsync(List<SizeModel> sizeFilter, string sortOrder, int page, int take)
        {

            var entities1 = from p in _context.Products
                            select p;

            foreach (var size in sizeFilter)
            {
                if (size.IsSelected)
                {
                    entities1= entities1.Where(x => x.ProductDetails.Size == size.Size).Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails);
                }
            }


            //Sort Filter
            switch (sortOrder)
            {
                case "reyting_desc":
                    entities1 = entities1.OrderByDescending(x => x.ProductDetails.StarCount);
                    break;
                case "reyting_asc":
                    entities1 = entities1.OrderBy(x => x.ProductDetails.StarCount);
                    break;
                case "price_desc":
                    entities1 = entities1.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities1 = entities1.OrderBy(x => x.Price);
                    break;
                default:
                    break;
            }

            //Paginate
            var allCount = await _context.Products.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();


            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            foreach (var entity in entities2)
            {
                foreach (var dto in dtos)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Availability = entity.ProductDetails.Availability;
                            dto.Count = entity.ProductDetails.Count;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Color = entity.ProductDetails.Color;
                            dto.Size = entity.ProductDetails.Size;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<Paginate<ProductDto>> GetAllGenderAsync(int genderID, string sortOrder, int page, int take)
        {

            var entities1 = from p in _context.Products.Where(x => x.GenderID == genderID).Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails)
                            select p;

            //Sort Filter
            switch (sortOrder)
            {
                case "reyting_desc":
                    entities1 = entities1.OrderByDescending(x => x.ProductDetails.StarCount);
                    break;
                case "reyting_asc":
                    entities1 = entities1.OrderBy(x => x.ProductDetails.StarCount);
                    break;
                case "price_desc":
                    entities1 = entities1.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities1 = entities1.OrderBy(x => x.Price);
                    break;
                default:
                    break;
            }

            //Paginate
            var allCount = await _context.Products.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();


            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            foreach (var entity in entities2)
            {
                foreach (var dto in dtos)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Availability = entity.ProductDetails.Availability;
                            dto.Count = entity.ProductDetails.Count;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Color = entity.ProductDetails.Color;
                            dto.Size = entity.ProductDetails.Size;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<Paginate<ProductDto>> GetAllCategoryAsync(int categoryID, string sortOrder, int page, int take)
        {
            
            var entities1 = from p in _context.Products.Where(x=> x.CategoryID == categoryID).Include(x => x.Gender).Include(x => x.Category).Include(x => x.ProductDetails)
                            select p;

            //Sort Filter
            switch (sortOrder)
            {
                case "reyting_desc":
                    entities1 = entities1.OrderByDescending(x => x.ProductDetails.StarCount);
                    break;
                case "reyting_asc":
                    entities1 = entities1.OrderBy(x => x.ProductDetails.StarCount);
                    break;
                case "price_desc":
                    entities1 = entities1.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities1 = entities1.OrderBy(x => x.Price);
                    break;
                default:
                    break;
            }

            //Paginate
            var allCount = await _context.Products.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities1.Skip((page - 1) * take).Take(take).ToListAsync();


            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            foreach (var entity in entities2)
            {
                foreach (var dto in dtos)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Availability = entity.ProductDetails.Availability;
                            dto.Count = entity.ProductDetails.Count;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Color = entity.ProductDetails.Color;
                            dto.Size = entity.ProductDetails.Size;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var entities = await _uow.GetRepository<Product>().AllOrderByAsync(x => x.Id, false);

            return _mapper.Map<List<ProductDto>>(entities);
        }

        public async Task<List<ProductDto>> GetIncludeAllAsync()
        {
            var entities = await _context.Set<Product>().Include(x => x.Category).Include(x => x.Gender).Include(x => x.ProductDetails)
                .AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();

            var productDto = _mapper.Map<List<ProductDto>>(entities);

            foreach (var entity in entities)
            {
                foreach (var dto in productDto)
                {
                    if (entity.Id == dto.Id)
                    {
                        if (entity.ProductDetails != null)
                        {
                            dto.Size = entity.ProductDetails.Size;
                            dto.Color = entity.ProductDetails.Color;
                            dto.StarCount = entity.ProductDetails.StarCount;
                            dto.Count = entity.ProductDetails.Count;
                        }
                        dto.CategoryName = entity.Category.Name;
                        dto.GenderName = entity.Gender.Name;
                    }
                }
            }
            return productDto;
        }

        public async Task CreateAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            if (entity != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + entity.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await entity.Photo.CopyToAsync(stream);
                }
                entity.Image = fileName;

                await _uow.GetRepository<Product>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<ProductDto> GetById(int id)
        {
            var entity = await _context.Set<Product>().Include(x => x.Category).Include(x => x.Gender).Include(x => x.ProductDetails).FirstOrDefaultAsync(x => x.Id == id);

            var dto = _mapper.Map<ProductDto>(entity);

            dto.CategoryName = entity.Category.Name;
            dto.GenderName = entity.Gender.Name;

            if (entity.ProductDetails != null)
            {
                dto.ProductID = entity.ProductDetails.Id;
                dto.Color = entity.ProductDetails.Color;
                dto.Size = entity.ProductDetails.Size;
                dto.Availability = entity.ProductDetails.Availability;
                dto.Count = entity.ProductDetails.Count;
                dto.StarCount = entity.ProductDetails.StarCount;
            }

            return dto;
        }

        public async Task<ProductUpdateDto> GetByIdUpdate(int id)
        {
            var entity = await _uow.GetRepository<Product>().FindAsync(id);

            return _mapper.Map<ProductUpdateDto>(entity);
        }

        public async Task Update(ProductUpdateDto dto)
        {
            var DbEntity = await _uow.GetRepository<Product>().FindAsync(dto.Id);

            if ((DbEntity != null) || (dto != null))
            {
                string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", DbEntity.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + dto.Photo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }
                dto.Image = fileName;

                var entity = _mapper.Map<Product>(dto);
                _uow.GetRepository<Product>().Update(entity, DbEntity);
                await _uow.SaveChangesAsync();
            }

        }

        public async Task Remove(int id)
        {
            var entity = await _context.Set<Product>().Include(x => x.ProductDetails).FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                //ProductDetails Remove
                if (entity.ProductDetails != null)
                {
                    await _productDetailsService.Remove(entity.ProductDetails.Id);
                }

                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", entity.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                _uow.GetRepository<Product>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
