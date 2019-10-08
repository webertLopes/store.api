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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProduct(ProductPost productPost)
        {
            Product product = mapper.Map<ProductPost, Product>(productPost);

            var resultValidate = ValidationHelper.Validate(product);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate.Errors);
            }

            var rows = productService.CreateProduct(product);

            if (rows == 0)
            {
                return NoContent();
            }

            return Ok(rows);
        }



        // GET: api/Products/5
        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "value";
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
