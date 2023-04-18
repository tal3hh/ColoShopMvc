using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.ProductDetails;

namespace ServiceLayer.Validations.FluentValidation.ProductDetailsValidation
{
    public class ProductDetailsCreateValidation : AbstractValidator<ProductDetailsCreateDto>
    {
        public ProductDetailsCreateValidation()
        {
            RuleFor(x => x.Count).NotNull().WithMessage("Mehsul sayini daxil edin.");

            RuleFor(x => x.Count).Must(x => x != 0).WithMessage("Say daxil edin.");

            RuleFor(x => x.ProductID).Must(x => x != 0).WithMessage("Mehsul secin! Yoxdursa yeni bir mehsul elave olunanadek gozleyin.");
        }
    }
}
