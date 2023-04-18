using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.Category;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _Categoryservice;
        public CategoryController(ICategoryService service)
        {
            _Categoryservice = service;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View((await _Categoryservice.GetAllAsync(), new CategoryCreateDto()));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item2")] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid) return View((await _Categoryservice.GetAllAsync(), dto));

            await _Categoryservice.CreateAsync(dto);
            return RedirectToAction("Create", "Category", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {

            return View((await _Categoryservice.GetAllAsync(), await _Categoryservice.GetByIdUpdate(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item2")] CategoryUpdateDto dto)
        {
            if (!ModelState.IsValid) return View((await _Categoryservice.GetAllAsync(), dto));

            await _Categoryservice.Update(dto);
            return RedirectToAction("Create", "Category", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _Categoryservice.Remove(id);

            return RedirectToAction("Create", "Category", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Details(int id)
        {

            return View((await _Categoryservice.GetAllAsync(), await _Categoryservice.GetById(id)));
        }
    }
}
