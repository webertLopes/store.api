using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {      

        public PaymentController()
        {
           
        }

        [HttpGet]
        public IActionResult GetInfo()
        {           
            return Ok();
        }

        

        
    }
}
