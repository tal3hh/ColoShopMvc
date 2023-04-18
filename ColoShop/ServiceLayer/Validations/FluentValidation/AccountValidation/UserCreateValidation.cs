using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.Account;

namespace ServiceLayer.Validations.FluentValidation.AccountValidation
{
    public class UserCreateValidation : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidation()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Istifadeci adinizi yazin...").Length(3, 50).WithMessage("3-50 intervalinda simvol yazin.");

            RuleFor(x => x.Email).NotNull().WithMessage("Email yazin...").Length(3, 100).WithMessage("3-100 intervalinda simvol yazin.")
                .EmailAddress().WithMessage("Email formatinda('@') yazi daxil edin.");

            RuleFor(x => x.Password).NotNull().WithMessage("Password daxil edin...").Length(3, 80).WithMessage("3-80 intervalinda simvol yazin.");

            RuleFor(x => x.ConfrimPassword).NotNull().WithMessage("ConfrimPassword daxil edin...")
                .Equal(x => x.Password).WithMessage("Tekrar sifre yalnisdir.");
        }
    }
}
