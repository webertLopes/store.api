using FluentValidation;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Exceptions
{
    public class SalesManCoreException : AbstractValidator<SalesMan>
    {
        public SalesManCoreException()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
