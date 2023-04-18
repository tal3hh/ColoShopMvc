using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Controllers
{
    public class HomeController : Controller
    {
        readonly IProductService _productservice;
        readonly IGenderService _genderservice;
        readonly IBlogService _blogservice;

        public HomeController(IProductService productservice, IGenderService genderservice, IBlogService blogservice)
        {
            _productservice = productservice;
            _genderservice = genderservice;
            _blogservice = blogservice;
        }

        public async Task<IActionResult> Index()
        {
            var genderlist = await _genderservice.GetAllAsync();
            var bloglist = await _blogservice.GetAllAsync();
            var productlist = await _productservice.GetIncludeAllAsync();
            return View((productlist, genderlist,bloglist));
        }


        public IActionResult Error()
        {
            var expHandFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var logFolderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Logs");

            var logFileName = DateTime.Now.ToString();

            //Replase
            logFileName = logFileName.Replace(" ", "_");
            logFileName = logFileName.Replace("/", "-");
            logFileName += ".txt";

            var logFilePath = Path.Combine(logFolderpath, logFileName);

            var directoryInfo = new DirectoryInfo(logFolderpath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var fileInfo = new FileInfo(logFilePath);

            var writer = fileInfo.CreateText();
            writer.WriteLine("Xetanin oldugu yer :" + expHandFeature.Path);
            writer.WriteLine("Xeta mesaji :" + expHandFeature.Error.Message);
            writer.Close();

            return View();
        }

        public void Xeta()
        {
            throw new System.Exception("xetaaaaasasdasdasda");
        }


    }
}
