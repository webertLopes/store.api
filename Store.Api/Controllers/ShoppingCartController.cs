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
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IMapper mapper;
        private readonly IValidator<ShoppingCart> ValidationHelper;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapper mapper, IValidator<ShoppingCart> ValidationHelper)
        {
            this.shoppingCartService = shoppingCartService ?? throw new ArgumentNullException(nameof(shoppingCartService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.ValidationHelper = ValidationHelper ?? throw new ArgumentNullException(nameof(ValidationHelper));
        }

        // GET: api/Customer
        /// <summary>
        /// Search for customers.
        /// </summary>
        /// <param name="shoppingCartGet">
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
        public async Task<IActionResult> GetShoppingCart([FromQuery] ShoppingCartGet shoppingCartGet)
        {
            ShoppingCart shoppingCart = mapper.Map<ShoppingCartGet, ShoppingCart>(shoppingCartGet);

            IEnumerable<ShoppingCart> shoppingCarts = await shoppingCartService.GetShoppingCartFiltered(shoppingCart);

            IEnumerable<ShoppingCartGetResult> shoppingCartGetResult =
                         mapper.Map<IEnumerable<ShoppingCartGetResult>>(shoppingCarts);

            return Ok(shoppingCartGetResult);
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
        public async Task<IActionResult> GetShoppingCartById(Guid id)
        {
            ShoppingCart shoppingCart = await shoppingCartService.Find(id);

            ShoppingCartGetResult shoppingCartGetResult = mapper.Map<ShoppingCartGetResult>(shoppingCart);

            return Ok(shoppingCartGetResult);
        }

        /// <summary>
        /// Update Customer.
        /// </summary>
        /// <param name="id">
        ///     Update Customer.
        /// </param>
        /// <param name="shoppingCartPost"></param>
        /// <response code="200">if 1 update if 0 dont update.</response>
        /// <response code="400">Incorrect parameters or usage limit exceeded.</response>
        /// <response code="404">
        ///     Not update Customer
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingCart(Guid id, ShoppingCartPost shoppingCartPost)
        {
            ShoppingCart isShoppingCart = await shoppingCartService.Find(id);

            if (isShoppingCart == null)
            {
                return BadRequest("shoppingCart not exists");
            }

            ShoppingCart shoppingCart = mapper.Map<ShoppingCartPost, ShoppingCart>(shoppingCartPost);

            shoppingCart.ShoppingCartId = id;

            var resultValidate = ValidationHelper.Validate(shoppingCart);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = await shoppingCartService.UpdateShoppingCart(shoppingCart);

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
        /// <param name="shoppingCartPost">
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
        public async Task<IActionResult> CreateShoppingCart(ShoppingCartPost shoppingCartPost)
        {
            ShoppingCart shoppingCart = mapper.Map<ShoppingCartPost, ShoppingCart>(shoppingCartPost);

            var resultValidate = ValidationHelper.Validate(shoppingCart);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = await shoppingCartService.Create(shoppingCart);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }
    }
}
