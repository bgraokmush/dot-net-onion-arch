using ETicaretApi.Application.ViewModels.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Validators.Products
{
    public class CreateProductValidatior : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidatior()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150)
                .MinimumLength(5)
                .NotNull();

            RuleFor(x => x.Stock)
                .NotNull()
                .NotEmpty()
                .Must(x => x >= 0);

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .Must(x => x >= 0);


        }
    }
}
