using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Extension
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCommentService, ProductCommentService>();
            services.AddScoped<IBlogCommentService, BlogCommentService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IProductDetailsService, ProductDetailsService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMessageSend, MessageSend>();
        }
    }
}
