using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.Product;

namespace ServiceLayer.Validations.FluentValidation.ProductValidation
{
    public class ProductCreateValidation : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Mehsul'un adini yazin.").NotNull().WithMessage("Mehsul'un adini yazin.")
                .Length(2,100).WithMessage("Mehsul adi 2-100 simvol olmalidir.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Qiymet daxil edin.");

            RuleFor(x => x.Photo).NotNull().WithMessage("Mehsul ucun sekil secin...").NotEmpty().WithMessage("Mehsul ucun sekil secin...");          
        }
    }
}
