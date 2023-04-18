using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        readonly IGenderService _genderservice;
        readonly ICategoryService _categoryservice;
        readonly IProductService _productservice;
        readonly IBlogService _blogservice;
        public DashboardController(IGenderService genderservice, 
            ICategoryService categoryservice, 
            IProductService productservice, 
            IBlogService blogservice)
        {
            _genderservice = genderservice;
            _categoryservice = categoryservice;
            _productservice = productservice;
            _blogservice = blogservice;
        }


        public async Task<IActionResult> Index()
        {
            var genderlist = await _genderservice.GetAllAsync();
            var categorylist = await _categoryservice.GetAllAsync();

            var productlist = await _productservice.GetIncludeAllAsync();
            var bloglist = await _blogservice.GetAllAsync();

            return View((categorylist,genderlist,productlist,bloglist));
        }
    }
}
