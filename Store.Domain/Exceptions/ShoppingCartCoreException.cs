using FluentValidation;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Exceptions
{
    public class ShoppingCartCoreException : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartCoreException()
        {

        }
    }
}
