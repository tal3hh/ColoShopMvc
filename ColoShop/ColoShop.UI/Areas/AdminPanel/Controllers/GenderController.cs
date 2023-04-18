using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.Gender;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class GenderController : Controller
    {
        readonly IGenderService _genderservice;
        public GenderController(IGenderService service)
        {
            _genderservice = service;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View((await _genderservice.GetAllAsync(), new GenderCreateDto()));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item2")] GenderCreateDto dto)
        {
            if (!ModelState.IsValid) return View((await _genderservice.GetAllAsync(), dto));

            await _genderservice.CreateAsync(dto);
            return RedirectToAction("Create", "Gender", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {

            return View((await _genderservice.GetAllAsync(), await _genderservice.GetByIdUpdate(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item2")] GenderUpdateDto dto)
        {
            if (!ModelState.IsValid) return View((await _genderservice.GetAllAsync(), dto));

            await _genderservice.Update(dto);
            return RedirectToAction("Create", "Gender", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _genderservice.Remove(id);

            return RedirectToAction("Create", "Gender", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Details(int id)
        {

            return View((await _genderservice.GetAllAsync(), await _genderservice.GetById(id)));
        }
    }
}
