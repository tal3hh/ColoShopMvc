using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.Contact;

namespace ServiceLayer.Validations.FluentValidation.ContactValidation
{
    public class ContactValidation : AbstractValidator<ContactDto>
    {
        public ContactValidation()
        {
            RuleFor(x => x.Name).Length(2,25).WithMessage("Ad 2-25 intervalinda simvol olmalidir.");
            RuleFor(x => x.Surname).Length(2, 45).WithMessage("Soyad 2-45 intervalinda simvol olmalidir.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Sehv e-mail yazmisiniz.Bir daha cehd edin.");
            RuleFor(x => x.Message).Length(2, 600).WithMessage("Mesaj 2-600 simvol olmalidir.");
        }
    }
}
