using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.ProductComment;
using ServiceLayer.Services.Interfaces;

namespace ColoShop.UI.Controllers
{
    public class DetailsController : Controller
    {
        readonly IProductService _productservice;
        readonly IProductCommentService _commentservice;

        public DetailsController(IProductService product, IProductCommentService commentservice)
        {
            _productservice = product;
            _commentservice = commentservice;
        }

        public async Task<IActionResult> Index(int productID)
        {

            var product = await _productservice.GetById(productID);
            var commentlist = await _commentservice.GetByIdAllAsync(productID);

            return View((product, new ProductCommentDto(), commentlist));
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind(Prefix = "Item2")] ProductCommentDto commentDto,int productid)
        {
            
            var product = await _productservice.GetById(productid);
            var commentlist = await _commentservice.GetByIdAllAsync(commentDto.ProductID);

            //Validation
            if (!ModelState.IsValid) return View((product, commentDto ,commentlist));

            //Create Comment
            commentDto.ProductID = productid;
            await _commentservice.CreateAsync(commentDto);
            var NEWcommentlist = await _commentservice.GetByIdAllAsync(commentDto.ProductID);

            return View((product, new ProductCommentDto(), NEWcommentlist));
        }
    }
}
