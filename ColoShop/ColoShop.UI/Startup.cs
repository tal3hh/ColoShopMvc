using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.Mapping;
using ServiceLayer.Extension;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Utilities.CustomDescriber;

namespace ColoShop.UI
{
    public class Startup
    {
        readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            //Extension
            services.AddValidations();
            services.AddServices();


            #region Context
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(_configuration["ConnectionStrings:Mssql"]);
            });
            #endregion


            services.AddControllersWithViews();

            services.AddFluentValidationAutoValidation();

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;  //Simvollardan biri olmalidir(@,/,$) 
                opt.Password.RequireLowercase = true;       //Mutleq Kicik herf
                opt.Password.RequireUppercase = true;       //Mutleq Boyuk herf 
                opt.Password.RequiredLength = 4;            //Min. simvol sayi
                opt.Password.RequireDigit = false;

                opt.User.RequireUniqueEmail = true;

                opt.SignIn.RequireConfirmedEmail = true;
                opt.SignIn.RequireConfirmedAccount = false;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //Sifreni 5defe sehv girdikde hesab 3dk baglanir.
                opt.Lockout.MaxFailedAccessAttempts = 5;                      //Sifreni max. 5defe sehv girmek olar.

            }).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.Cookie.Name = "AshionIdentity";
                opt.LoginPath = new PathString("/Account/Login");
                opt.AccessDeniedPath = new PathString("/Account/AccessDenied");

            });


            #region AutoMapper
            var configuration = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            #endregion


            services.AddScoped<IUow, Uow>();

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default2",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
