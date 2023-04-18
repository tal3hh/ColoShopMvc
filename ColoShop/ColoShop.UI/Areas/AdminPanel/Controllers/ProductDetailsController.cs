using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.ProductDetails;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class ProductDetailsController : Controller
    {
        readonly IProductDetailsService _ProductDetailsservice;
        readonly IProductService _productService;
        public ProductDetailsController(IProductDetailsService service, IProductService productService)
        {
            _ProductDetailsservice = service;
            _productService = productService;
        }

        public async Task<IActionResult> ProductDetailsList(int page = 1, int take = 20)
        {

            return View(await _ProductDetailsservice.GetPaginateIncludeAsync(page, take));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productlist = await _ProductDetailsservice.GetProductAllAsync();

            var productdetailslist = await _ProductDetailsservice.GetIncludeAllAsync();

            return View((new ProductDetailsCreateDto(),productlist,productdetailslist));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] ProductDetailsCreateDto dto)
        {
            var productlist = await _ProductDetailsservice.GetProductAllAsync();
            var productdetailslist = await _ProductDetailsservice.GetIncludeAllAsync();

            if (!ModelState.IsValid) return View((dto, productlist, productdetailslist));

            await _ProductDetailsservice.CreateAsync(dto);
            return RedirectToAction("Create", "Product", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                id = (int)TempData["ProId"];
            }
            var productlist = await _productService.GetAllAsync();
            var productDetailsDto = await _ProductDetailsservice.GetById(id);
            var updatedto = await _ProductDetailsservice.GetUpdateById(id);

            return View((productDetailsDto,updatedto, productlist));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item2")] ProductDetailsUpdateDto dto)
        {
            TempData["ProId"] = dto.Id;
            var productlist = await _productService.GetAllAsync();
            var productDetailsDto = await _ProductDetailsservice.GetById(dto.Id);

            if (!ModelState.IsValid) return View((productDetailsDto,dto, productlist));

            await _ProductDetailsservice.Update(dto);
            return RedirectToAction("Update", "ProductDetails", new { area = "AdminPanel"});
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _ProductDetailsservice.Remove(id);

            return RedirectToAction("ProductDetailsList", "ProductDetails", new { area = "AdminPanel" });
        }
    }
}
