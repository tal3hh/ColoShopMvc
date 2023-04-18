using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.ProductDetails;

namespace ServiceLayer.Validations.FluentValidation.ProductDetailsValidation
{
    public class ProductDetailsUpdateValidation : AbstractValidator<ProductDetailsUpdateDto>
    {
        public ProductDetailsUpdateValidation()
        {
            RuleFor(x => x.Count).NotNull().WithMessage("Say daxil edin.")
                .Must(x=> x !> 0 && x !< 500).WithMessage("Say daxil edin.(1-500 intervalinda!)");
            RuleFor(x => x.Size).NotNull().WithMessage("Mehsul ucun size secin.");
        }
    }
}
