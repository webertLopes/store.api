using FluentValidation;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
