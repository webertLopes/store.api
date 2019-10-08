using FluentValidation;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Exceptions
{
    public class CustomerCoreException : AbstractValidator<Customer>
    {
        public CustomerCoreException()
        {
            RuleFor(x => x.Cpf)
                .Length(11);

            RuleFor(x => x.Name).NotNull();
        }
    }
}
