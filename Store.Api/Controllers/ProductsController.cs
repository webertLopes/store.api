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
        /// <summary>
        /// Get image and transform in base64
        /// </summary>
        /// <param name="uploadedFile">
        ///     Image transform.
        /// </param>
        /// <response code="200">Result imageBase64.</response>
        /// <response code="500">Internal Error</response>
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



        /// <summary>
        /// Update Product.
        /// </summary>
        /// <param name="id">
        ///     Update Product.
        /// </param>
        /// <param name="productPost"></param>
        /// <response code="200">if 1 update if 0 dont update.</response>
        /// <response code="404">
        ///     Not update product
        /// </response>
        /// <response code="400">
        ///     Incorrect parameters or usage limit exceeded.
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, ProductPost productPost)
        {
            Product isProduct = productService.Find(id);

            if (isProduct == null)
            {
                return BadRequest("product not exists");
            }

            Product product = mapper.Map<ProductPost, Product>(productPost);

            product.ProductId = id;

            var resultValidate = ValidationHelper.Validate(product);

            if (resultValidate.Errors.Count > 0)
            {
                return BadRequest(resultValidate);
            }

            var rows = productService.UpdateProduct(product);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(rows);
        }

        /// <summary>
        /// Search for Product by ProductId.
        /// </summary>
        /// <param name="id">
        ///     Product base search by ProductId.
        /// </param>
        /// <response code="200">Result one Product.</response>
        /// <response code="400">
        ///     Incorrect parameters or usage limit exceeded.
        /// </response>
        /// <response code="500">Internal Error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResult), 400)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProductById(Guid id)
        {
            Product product = productService.Find(id);

            ProductGetResult productGetResult = mapper.Map<ProductGetResult>(product);            

            return Ok(productGetResult);
        }


        // POST: api/Products
        /// <summary>
        /// Create one Product
        /// </summary>
        /// <param name="productPost">
        ///     Product base search.
        /// </param>
        /// <response code="200">Create one Product.</response>
        /// <response code="404">if 1 create if 0 no create products.</response>
        /// <response code="400">
        ///     Incorrect parameters or usage limit exceeded.
        /// </response>
        /// <response code="500">Internal Error</response>
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


        // GET: api/Products
        /// <summary>
        /// Search for Product.
        /// </summary>
        /// <param name="productsGet">
        ///     Product base search.
        /// </param>
        /// <response code="200">Result list Products.</response>
        /// <response code="400">
        ///     Incorrect parameters or usage limit exceeded.
        /// </response>
        /// <response code="500">Internal Error</response>
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
