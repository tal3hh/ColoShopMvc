using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.DTOs.Account;
using ServiceLayer.DTOs.Blog;
using ServiceLayer.DTOs.BlogComment;
using ServiceLayer.DTOs.Category;
using ServiceLayer.DTOs.Contact;
using ServiceLayer.DTOs.Gender;
using ServiceLayer.DTOs.Product;
using ServiceLayer.DTOs.ProductComment;
using ServiceLayer.DTOs.ProductDetails;
using ServiceLayer.Validations.FluentValidation.AccountValidation;
using ServiceLayer.Validations.FluentValidation.BlogCommentValidation;
using ServiceLayer.Validations.FluentValidation.BlogValidation;
using ServiceLayer.Validations.FluentValidation.CategoryValidation;
using ServiceLayer.Validations.FluentValidation.ContactValidation;
using ServiceLayer.Validations.FluentValidation.GenderValidation;
using ServiceLayer.Validations.FluentValidation.ProductCommentValidation;
using ServiceLayer.Validations.FluentValidation.ProductDetailsValidation;
using ServiceLayer.Validations.FluentValidation.ProductValidation;

namespace ServiceLayer.Extension
{
    public static class ValidationExtension
    {
        public static void AddValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidation>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidation>();

            services.AddScoped<IValidator<ProductDetailsCreateDto>, ProductDetailsCreateValidation>();
            services.AddScoped<IValidator<ProductDetailsUpdateDto>, ProductDetailsUpdateValidation>();

            services.AddScoped<IValidator<BlogUpdateDto>, BlogUpdateValidation>();
            services.AddScoped<IValidator<BlogCreateDto>, BlogCreateValidation>();

            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateValidation>();
            services.AddScoped<IValidator<CategoryUpdateDto>, CategoryUpdateValidation>();

            services.AddScoped<IValidator<GenderCreateDto>, GenderCreateValidation>();
            services.AddScoped<IValidator<GenderUpdateDto>, GenderUpdateValidation>();

            services.AddScoped<IValidator<ContactDto>, ContactValidation>();

            services.AddScoped<IValidator<BlogCommentDto>, BlogCommentValidation>();
            services.AddScoped<IValidator<ProductCommentDto>, ProductCommentValidation>();

            services.AddScoped<IValidator<UserCreateDto>, UserCreateValidation>();
            services.AddScoped<IValidator<UserLoginDto>, UserLoginValidation>();
        }
    }
}
