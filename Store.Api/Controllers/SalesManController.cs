using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Dtos;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesManController : ControllerBase
    {
        private readonly ISalesManService salesManService;
        private readonly IMapper mapper;
        private readonly IValidator<SalesMan> ValidationHelper;

        public SalesManController(ISalesManService salesManService, IMapper mapper, IValidator<SalesMan> ValidationHelper)
        {
            this.salesManService = salesManService ?? throw new ArgumentNullException(nameof(salesManService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.ValidationHelper = ValidationHelper ?? throw new ArgumentNullException(nameof(ValidationHelper));
        }


        // GET: api/Customer
        /// <summary>
        /// Search for SalesMan.
        /// </summary>
        /// <param name="salesManGet">
        ///     SalesMan base search criteria.
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
        public IActionResult GetSalesMan([FromQuery] SalesManGet salesManGet)
        {
            SalesMan salesMan = mapper.Map<SalesManGet, SalesMan>(salesManGet);

            IEnumerable<SalesMan> saleMans = salesManService.GetSalesManFiltered(salesMan);

            IEnumerable<SalesManGetResult> salesManGetResult =
                         mapper.Map<IEnumerable<SalesManGetResult>>(saleMans);

            return Ok(salesManGetResult);
        }

        /// <summary>
        /// Search for SalesMan by SalesManId.
        /// </summary>
        /// <param name="id">
        ///     Customer base search by SalesManId.
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
        public IActionResult GetSalesManById(Guid id)
        {
            SalesMan salesMan = salesManService.Find(id);

            SalesManGetResult customerGetResult = mapper.Map<SalesManGetResult>(salesMan);

            return Ok(customerGetResult);
        }

        /// <summary>
        /// Update SalesMan.
        /// </summary>
        /// <param name="id">
        ///     Update SalesMan.
        /// </param>
        /// <param name="salesManPost"></param>
        /// <response code="200">if 1 update if 0 dont update.</response>
        /// <response code="400">Incorrect parameters or usage limit exceeded.</response>
        /// <response code="404">
        ///     Not update SalesMan
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpPut("{id}")]
        public IActionResult UpdateSalesMan(Guid id, SalesManPost salesManPost)
        {
            SalesMan isSalesMan = salesManService.Find(id);

            if (isSalesMan == null)
            {
                return BadRequest("salesman not exists");
            }

            SalesMan salesman = mapper.Map<SalesManPost, SalesMan>(salesManPost);

            salesman.SalesManId = id;

            var resultValidate = ValidationHelper.Validate(salesman);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = salesManService.UpdateSalesMan(salesman);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }


        // POST: api/Customer
        /// <summary>
        /// Create one SalesMan
        /// </summary>
        /// <param name="salesManPost">
        ///     Create one SalesMan
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
        public IActionResult CreateSalesMan(SalesManPost salesManPost)
        {
            SalesMan salesMan = mapper.Map<SalesManPost, SalesMan>(salesManPost);

            var resultValidate = ValidationHelper.Validate(salesMan);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = salesManService.CreateSalesMan(salesMan);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }
    }
}
