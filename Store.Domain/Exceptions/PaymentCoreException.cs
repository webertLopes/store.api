using FluentValidation;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Exceptions
{
    public class PaymentCoreException : AbstractValidator<Payment>
    {
        public PaymentCoreException()
        {

        }
    }
}
