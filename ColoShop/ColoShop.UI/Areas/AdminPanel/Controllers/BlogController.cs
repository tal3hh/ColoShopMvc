using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.Blog;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        readonly IBlogService _blogservice;
        public BlogController(IBlogService blogservice)
        {
            _blogservice = blogservice;
        }

        #region List
        public async Task<IActionResult> BlogList(int page = 1, int take = 15)
        {

            return View(await _blogservice.GetAllPaginateAsync(page,take));
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            return View((new BlogCreateDto(), await _blogservice.GetAllAsync()));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] BlogCreateDto dto)
        {
            if (!ModelState.IsValid) return View((dto, await _blogservice.GetAllAsync()));

            await _blogservice.CreateAsync(dto);
            return RedirectToAction("Create", "Blog", new { area = "AdminPanel" });
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                id = (int)TempData["BlogId"];
            }
            return View((await _blogservice.GetByUpdateId(id), await _blogservice.GetById(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix ="Item1")] BlogUpdateDto dto)
        {
            if (!ModelState.IsValid) return View((dto, await _blogservice.GetById(dto.Id)));

            TempData["BlogId"] = dto.Id;
            await _blogservice.Update(dto);
            return RedirectToAction("Update", "Blog", new { area = "AdminPanel" });
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _blogservice.Remove(id);
            return RedirectToAction("BlogList", "Blog", new { area = "AdminPanel" });
        }
        #endregion
    }
}
