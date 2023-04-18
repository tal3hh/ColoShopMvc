using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.Blog;

namespace ServiceLayer.Validations.FluentValidation.BlogValidation
{
    public class BlogCreateValidation : AbstractValidator<BlogCreateDto>
    {
        public BlogCreateValidation()
        {
            RuleFor(x => x.ByName).NotNull().WithMessage("Blog yazicisinin adini yazin.")
                .Length(1, 50).WithMessage("1-50 intervalinda simvol daxil edin.");

            RuleFor(x => x.Title).NotNull().WithMessage("Basliq daxil edin.")
                .Length(1,100).WithMessage("1-100 intervalinda simvol daxil edin.");

            RuleFor(x => x.Description).NotNull().WithMessage("Haqqinda melumat daxil edin.")
                .Length(5,700).WithMessage("5-700 intervalinda simvol daxil edin.");

            RuleFor(x => x.Photo).NotNull().WithMessage("Blog ucun sekil secin...").NotEmpty().WithMessage("Blog ucun sekil secin...");
        }
    }
}
