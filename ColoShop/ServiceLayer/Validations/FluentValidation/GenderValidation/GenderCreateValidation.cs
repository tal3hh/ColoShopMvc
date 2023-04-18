using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.Gender;

namespace ServiceLayer.Validations.FluentValidation.GenderValidation
{
    public class GenderCreateValidation : AbstractValidator<GenderCreateDto>
    {
        public GenderCreateValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Cinsiyyet'i yazin...")
                .NotEmpty().WithMessage("Cinsiyyet'i yazin...");
           
        }
    }
}
