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
    public class SaleController : ControllerBase
    {
        private readonly ISaleService saleService;
        private readonly IMapper mapper;
        private readonly IValidator<Sale> ValidationHelper;


        public SaleController(ISaleService saleService, IMapper mapper, IValidator<Sale> ValidationHelper)
        {
            this.saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.ValidationHelper = ValidationHelper ?? throw new ArgumentNullException(nameof(ValidationHelper));
        }

        // GET: api/Customer
        /// <summary>
        /// Search for customers.
        /// </summary>
        /// <param name="saleGet">
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
        public async Task<IActionResult> GetSale([FromQuery] SaleGet saleGet)
        {
            Sale sale = mapper.Map<SaleGet, Sale>(saleGet);

            IEnumerable<Sale> sales = await saleService.GetSaleFiltered(sale);

            IEnumerable<SaleGetResult> saleGetResult =
                         mapper.Map<IEnumerable<SaleGetResult>>(sales);

            return Ok(saleGetResult);
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
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            Sale sale = await saleService.Find(id);

            SaleGetResult saleGetResult = mapper.Map<SaleGetResult>(sale);

            return Ok(saleGetResult);
        }

        /// <summary>
        /// Update Customer.
        /// </summary>
        /// <param name="id">
        ///     Update Customer.
        /// </param>
        /// <param name="salePost"></param>
        /// <response code="200">if 1 update if 0 dont update.</response>
        /// <response code="400">Incorrect parameters or usage limit exceeded.</response>
        /// <response code="404">
        ///     Not update Customer
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(Guid id, SalePost salePost)
        {
            Sale isSale = await saleService.Find(id);

            if (isSale == null)
            {
                return BadRequest("sale not exists");
            }

            Sale sale = mapper.Map<SalePost, Sale>(salePost);

            sale.SaleId = id;

            var resultValidate = ValidationHelper.Validate(sale);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = await saleService.UpdateSale(sale);

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
        /// <param name="salePost">
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
        public async Task<IActionResult> CreatePayment(SalePost salePost)
        {
            Sale sale = mapper.Map<SalePost, Sale>(salePost);

            var resultValidate = ValidationHelper.Validate(sale);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = await saleService.Create(sale);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }
    }
}
