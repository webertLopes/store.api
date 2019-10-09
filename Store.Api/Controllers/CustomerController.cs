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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        private readonly IValidator<Customer> ValidationHelper;

        public CustomerController(ICustomerService customerService, IMapper mapper, IValidator<Customer> ValidationHelper)
        {
            this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.ValidationHelper = ValidationHelper ?? throw new ArgumentNullException(nameof(ValidationHelper));
        }


        // GET: api/Customer
        /// <summary>
        /// Search for customers.
        /// </summary>
        /// <param name="customerGet">
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
        public IActionResult GetCustomer([FromQuery] CustomerGet customerGet)
        {
            Customer customer = mapper.Map<CustomerGet, Customer>(customerGet);

            IEnumerable<Customer> customers = customerService.GetCustomerFiltered(customer);

            IEnumerable<CustomerGetResult> customerGetResult =
                         mapper.Map<IEnumerable<CustomerGetResult>>(customers);

            return Ok(customerGetResult);
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
        public IActionResult GetCustomerById(Guid id)
        {
            Customer customers = customerService.Find(id);

            CustomerGetResult customerGetResult = mapper.Map<CustomerGetResult>(customers);

            return Ok(customerGetResult);
        }

        /// <summary>
        /// Update Customer.
        /// </summary>
        /// <param name="id">
        ///     Update Customer.
        /// </param>
        /// <param name="customerPost"></param>
        /// <response code="200">if 1 update if 0 dont update.</response>
        /// <response code="400">Incorrect parameters or usage limit exceeded.</response>
        /// <response code="404">
        ///     Not update Customer
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(Guid id, CustomerPost customerPost)
        {
            Customer isCustomer = customerService.Find(id);

            if (isCustomer == null)
            {
                return BadRequest("customer not exists");
            }

            Customer customer = mapper.Map<CustomerPost, Customer>(customerPost);

            customer.CustomerId = id;

            var resultValidate = ValidationHelper.Validate(customer);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = customerService.UpdateCustomer(customer);

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
        /// <param name="customerPost">
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
        public IActionResult CreateCustomer(CustomerPost customerPost)
        {
            Customer customer = mapper.Map<CustomerPost, Customer>(customerPost);

            var resultValidate = ValidationHelper.Validate(customer);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = customerService.Create(customer);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }

       
    }
}
