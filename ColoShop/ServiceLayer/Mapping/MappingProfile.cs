using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Entities;
using ServiceLayer.DTOs.Blog;
using ServiceLayer.DTOs.BlogComment;
using ServiceLayer.DTOs.Category;
using ServiceLayer.DTOs.Contact;
using ServiceLayer.DTOs.Gender;
using ServiceLayer.DTOs.Product;
using ServiceLayer.DTOs.ProductComment;
using ServiceLayer.DTOs.ProductDetails;

namespace ServiceLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Blog, BlogDto>().ReverseMap();
            CreateMap<Blog, BlogCreateDto>().ReverseMap();
            CreateMap<Blog, BlogUpdateDto>().ReverseMap();

            CreateMap<BlogComment, BlogCommentDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();

            CreateMap<Contact, ContactDto>().ReverseMap();


            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<Gender, GenderCreateDto>().ReverseMap();
            CreateMap<Gender, GenderUpdateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();

            CreateMap<ProductComment, ProductCommentDto>().ReverseMap();

            CreateMap<ProductDetails, ProductDetailsDto>().ReverseMap();
            CreateMap<ProductDetails, ProductDetailsUpdateDto>().ReverseMap();
            CreateMap<ProductDetails, ProductDetailsCreateDto>().ReverseMap();

        }
    }
}
