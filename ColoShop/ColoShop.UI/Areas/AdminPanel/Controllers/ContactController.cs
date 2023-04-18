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
    public class ContactController : Controller
    {
        readonly IContactService _contactservice;
        public ContactController(IContactService contactservice)
        {
            _contactservice = contactservice;
        }

        #region List
        public async Task<IActionResult> ContactList()
        {
            return View(await _contactservice.GetAllAsync());
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _contactservice.Remove(id);
            return RedirectToAction("ContactList", "Contact", new { area = "AdminPanel" });
        }
        #endregion
    }
}
