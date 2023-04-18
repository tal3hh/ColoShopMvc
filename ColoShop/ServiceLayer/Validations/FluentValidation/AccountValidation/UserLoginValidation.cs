using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.Account;

namespace ServiceLayer.Validations.FluentValidation.AccountValidation
{
    public class UserLoginValidation : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidation()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Istifadeci adinizi yazin...").Length(3, 50).WithMessage("3-50 intervalinda simvol yazin.");

            RuleFor(x => x.Password).NotNull().WithMessage("Password daxil edin...").Length(3, 80).WithMessage("3-80 intervalinda simvol yazin.");
        }
    }
}
