using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ServiceLayer.DTOs.ProductComment;

namespace ServiceLayer.Validations.FluentValidation.ProductCommentValidation
{
    public class ProductCommentValidation : AbstractValidator<ProductCommentDto>
    {
        public ProductCommentValidation()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad ve Soyadinizi yazin.").Length(3, 50).WithMessage("3-50 intervalinda simvol yazin.");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj yazin.").Length(3, 600).WithMessage("3-600 intervalinda simvol yazin.");
        }
    }
}
