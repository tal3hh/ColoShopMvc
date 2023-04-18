using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.Product;
using ServiceLayer.DTOs.ProductDetails;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        readonly IProductService _productservice;
        readonly ICategoryService _categoryservice;
        readonly IGenderService _genderservice;
        readonly IProductDetailsService _ProductDetailsservice;
        public ProductController(IProductService service, ICategoryService categoryservice, IGenderService genderservice, IProductDetailsService productDetailsservice)
        {
            _productservice = service;
            _categoryservice = categoryservice;
            _genderservice = genderservice;
            _ProductDetailsservice = productDetailsservice;
        }



        
        #region ProductList
        public async Task<IActionResult> ProductList(string sortOrder,int Page = 1, int Take = 20)
        {
            ViewBag.Price = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.Count = sortOrder == "count_asc" ? "count_desc" : "count_asc";

            //View send
            TempData["sortOrder"] = sortOrder;

            return View(await _productservice.GetAllDashboardFilterAsync(sortOrder,Page,Take));
        }
        #endregion


        #region Search
        public async Task<IActionResult> ProductSearch(string productSearch, int page = 1, int take = 20)
        {
            var products = await _productservice.GetSearchDashboardAsync(productSearch, page, take);

            return View("ProductList", products);
        }
        #endregion


        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categorylist = await _categoryservice.GetAllAsync();
            var genderlist = await _genderservice.GetAllAsync();
            var productlist = await _productservice.GetIncludeAllAsync();
            var productcreateDto = new ProductCreateDto();

            var productdetailslist = await _ProductDetailsservice.GetIncludeAllAsync();
            var productDetailscreateDto = new ProductDetailsCreateDto();
            var ProListDto = await _ProductDetailsservice.GetProductAllAsync();

            return View((productDetailscreateDto, productdetailslist, productcreateDto, productlist,ProListDto, categorylist, genderlist));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item3")] ProductCreateDto dto)
        {
            var categorylist = await _categoryservice.GetAllAsync();
            var genderlist = await _genderservice.GetAllAsync();
            var productlist = await _productservice.GetIncludeAllAsync();
            

            var productdetailslist = await _ProductDetailsservice.GetIncludeAllAsync();
            var productDetailscreateDto = new ProductDetailsCreateDto();
            var ProListDto = await _ProductDetailsservice.GetProductAllAsync();

            if (!ModelState.IsValid) return View((productDetailscreateDto, productdetailslist, dto, productlist, ProListDto, categorylist, genderlist));

            await _productservice.CreateAsync(dto);
            return RedirectToAction("Create", "Product", new { area = "AdminPanel" });
        }
        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                id = (int)TempData["ProId"];
            }

            var CategList = await _categoryservice.GetAllAsync();
            var GenderList = await _genderservice.GetAllAsync();
            var ProUpdate = await _productservice.GetByIdUpdate(id);
            var ProView = await _productservice.GetById(id);

            //ProDetails is null?
            if (ProView.ProductID == 0) return RedirectToAction("Create", "ProductDetails", new { area = "AdminPanel" });

            var ProList = await _productservice.GetAllAsync();
            var ProDetaView = await _ProductDetailsservice.GetById(ProView.ProductID);
            var ProDetaUpdate = await _ProductDetailsservice.GetUpdateById(ProView.ProductID);

            return View((ProUpdate,ProDetaUpdate,ProView,ProDetaView,ProList,GenderList,CategList));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] ProductUpdateDto dto)
        {
            TempData["ProId"] = dto.Id;
            var CategList = await _categoryservice.GetAllAsync();
            var GenderList = await _genderservice.GetAllAsync();
            var ProView = await _productservice.GetById(dto.Id);

            var ProList = await _productservice.GetAllAsync();
            var ProDetaView = await _ProductDetailsservice.GetById(ProView.ProductID);
            var ProDetaUpdate = await _ProductDetailsservice.GetUpdateById(ProView.ProductID);

            if (!ModelState.IsValid) return View((dto, ProDetaUpdate, ProView, ProDetaView, ProList, GenderList, CategList));

            await _productservice.Update(dto);
            return RedirectToAction("Update", "Product", new { area = "AdminPanel" });
        }
        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _productservice.Remove(id);

            return RedirectToAction("ProductList", "Product", new { area = "AdminPanel" });
        }
        #endregion


        #region Deatils
        public async Task<IActionResult> Details(int id)
        {

            return View(await _productservice.GetById(id));
        }
        #endregion

    }
}
