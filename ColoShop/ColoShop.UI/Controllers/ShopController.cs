using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.EnumSize;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Controllers
{
    public class ShopController : Controller
    {
        readonly IProductService _productservice;
        readonly ICategoryService _categoryservice;
        readonly IGenderService _genderservice;

        public ShopController(IProductService productservice, ICategoryService categoryservice, IGenderService genderservice)
        {
            _productservice = productservice;
            _categoryservice = categoryservice;
            _genderservice = genderservice;
        }

        public async Task<IActionResult> Index(int categoryid, string sortOrder, int genderid, int page = 1, int take = 8)
        {
            ViewBag.Reyting = String.IsNullOrEmpty(sortOrder) ? "reyting_desc" : "reyting_asc";
            ViewBag.Price = (sortOrder == "price_desc") ? "price_asc" : "price_desc";

            //View send
            TempData["CategoryId"] = categoryid;
            TempData["sortOrder"] = sortOrder;
            TempData["GenderId"] = genderid;

            var categories = await _categoryservice.GetAllAsync();
            var genders = await _genderservice.GetAllAsync();
            var products = await _productservice.GetAllContrllerFilterAsync(sortOrder,page, take);

            //Size Enum
            var Size = _productservice.GetSizeEnum();


            //Gender wilt Products
            if (genderid != 0) products = await _productservice.GetAllGenderAsync(genderid, sortOrder, page, take);

            //Category with Products
            if (categoryid != 0) products = await _productservice.GetAllCategoryAsync(categoryid, sortOrder,page, take);

            //Gender AND Category with Products
            if (categoryid != 0 && genderid != 0) products = await _productservice.GetAllGenderANDCategoryAsync(genderid, categoryid, sortOrder, page, take);

          

            return View((products,categories,Size,genders));
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind(Prefix = "Item3")] SizeViewModel sizeFilter,string sortOrder,int page = 1, int take = 8)
        {

            var products = await _productservice.GetAllSizeAsync(sizeFilter.CheckBoxItems, sortOrder, page, take);

            var genders = await _genderservice.GetAllAsync();
            var categories = await _categoryservice.GetAllAsync();

            var Size = _productservice.GetSizeEnum();

            return View((products, categories, Size, genders));
        }


    }
}
