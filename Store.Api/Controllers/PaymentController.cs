using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Dtos;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;
        private readonly IValidator<Payment> ValidationHelper;


        public PaymentController(IPaymentService paymentService, IMapper mapper, IValidator<Payment> ValidationHelper)
        {
            this.paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.ValidationHelper = ValidationHelper ?? throw new ArgumentNullException(nameof(ValidationHelper));
        }
        
        // GET: api/Customer
        /// <summary>
        /// Search for customers.
        /// </summary>
        /// <param name="paymentGet">
        ///     Customer base search criteria.
        /// </param>
        /// <response code="200">Result List.</response>
        /// <response code="400">
        ///     Incorrect parameters or usage limit exceeded.
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPayment([FromQuery] PaymentGet paymentGet)
        {
            Payment payment = mapper.Map<PaymentGet, Payment>(paymentGet);

            IEnumerable<Payment> payments = await paymentService.GetPaymentFiltered(payment);

            IEnumerable<PaymentGetResult> paymentGetResult =
                         mapper.Map<IEnumerable<PaymentGetResult>>(payments);

            return Ok(paymentGetResult);
        }

        /// <summary>
        /// Search for customers by CustomerId.
        /// </summary>
        /// <param name="id">
        ///     Customer base search by CustomerId.
        /// </param>
        /// <response code="200">Result one Customer.</response>
        /// <response code="400">
        ///     Incorrect parameters or usage limit exceeded.
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            Payment payments = await paymentService.Find(id);

            PaymentGetResult paymentGetResult = mapper.Map<PaymentGetResult>(payments);

            return Ok(paymentGetResult);
        }

        /// <summary>
        /// Update Customer.
        /// </summary>
        /// <param name="id">
        ///     Update Customer.
        /// </param>
        /// <param name="paymentPost"></param>
        /// <response code="200">if 1 update if 0 dont update.</response>
        /// <response code="400">Incorrect parameters or usage limit exceeded.</response>
        /// <response code="404">
        ///     Not update Customer
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, PaymentPost paymentPost)
        {
            Payment isPayment = await paymentService.Find(id);

            if (isPayment == null)
            {
                return BadRequest("payment not exists");
            }

            Payment payment = mapper.Map<PaymentPost, Payment>(paymentPost);

            payment.PaymentId = id;

            var resultValidate = ValidationHelper.Validate(payment);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = await paymentService.UpdatePayment(payment);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }


        // POST: api/Customer
        /// <summary>
        /// Create one Customer
        /// </summary>
        /// <param name="paymentPost">
        ///     Create one Customer
        /// </param>
        /// <response code="200">Result 1 if create and 0 not create.</response>
        /// <response code="400">
        ///     Incorrect parameters or usage limit exceeded.
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreatePayment(PaymentPost paymentPost)
        {
            Payment payment = mapper.Map<PaymentPost, Payment>(paymentPost);

            var resultValidate = ValidationHelper.Validate(payment);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = await paymentService.Create(payment);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }
    }
}
