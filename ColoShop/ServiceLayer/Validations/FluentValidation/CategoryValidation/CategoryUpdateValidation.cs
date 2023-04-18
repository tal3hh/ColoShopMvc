using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.Category;

namespace ServiceLayer.Validations.FluentValidation.CategoryValidation
{
    public class CategoryUpdateValidation : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Kateqoriyani yazin...")
                .NotEmpty().WithMessage("Kateqoriyani yazin...");

        }
    }
}
