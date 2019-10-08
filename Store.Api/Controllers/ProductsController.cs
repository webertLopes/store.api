using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Dtos;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Exceptions;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IValidator<Product> ValidationHelper;

        public ProductsController(IProductService productService, IMapper mapper, IValidator<Product> ValidationHelper)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.ValidationHelper = ValidationHelper ?? throw new ArgumentNullException(nameof(ValidationHelper));
        }

        // GET: api/Products
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetImageProduct(IFormFile uploadedFile)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = productService.ImageToBase64(uploadedFile);

            return Ok(result);
        }


        // POST: api/Products
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateProduct(ProductPost productPost)
        {
            Product product = mapper.Map<ProductPost, Product>(productPost);

            var resultValidate = ValidationHelper.Validate(product);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = productService.CreateProduct(product);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProducts([FromQuery] ProductsGet productsGet)
        {
            Product product = mapper.Map<ProductsGet, Product>(productsGet);

            IEnumerable<Product> products = productService.GetProductsFiltered(product);

            IEnumerable<ProductGetResult> productGetResult = 
                         mapper.Map<IEnumerable<ProductGetResult>>(products);

            return Ok(productGetResult);
        }
        
    }
}
