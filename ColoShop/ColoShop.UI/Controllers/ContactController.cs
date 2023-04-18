using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.Contact;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Controllers
{

    public class ContactController : Controller
    {

        readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View(new ContactDto());
        }

        [HttpPost] 
        public async Task<IActionResult> Index(ContactDto dto)
        {
            //Validation
            if (!ModelState.IsValid) return View(dto);

            await _contactService.CreateAsync(dto);

            return View("Index","Home");
        }
    }
}
