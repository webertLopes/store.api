using FluentValidation;
using Store.Domain.Entities;

namespace Store.Domain.Exceptions
{
    public class ProductCoreException : AbstractValidator<Product> 
    {
        public ProductCoreException()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .Length(3, 5);
                

            RuleFor(x => x.Code).NotNull();
        }
    }
}
