using FluentValidation;
using Store.Domain.Entities;

namespace Store.Domain.Exceptions
{
    public class ProductCoreException : AbstractValidator<Product> 
    {
        public ProductCoreException()
        {
            RuleFor(x => x.Description)
                .NotNull();
                

            RuleFor(x => x.Code).NotNull();
        }
    }
}
