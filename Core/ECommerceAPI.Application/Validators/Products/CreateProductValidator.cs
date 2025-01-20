using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Please enter the product name.")
                .MinimumLength(2)
                .MaximumLength(150)
                    .WithMessage("Please enter a value between 2 and 150 characters for the product name.");

            RuleFor(p => p.Price)
                .Must(p => p > 0)
                    .WithMessage("The price must be greater than 0.");

            RuleFor(p => p.Stock)
                .Must(p => p >= 0)
                    .WithMessage("The stock cannot be less than 0.");
        }
    }
}
