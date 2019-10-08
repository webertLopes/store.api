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


        // POST: api/Customer
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
