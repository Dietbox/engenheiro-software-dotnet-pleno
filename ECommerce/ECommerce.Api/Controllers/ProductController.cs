using ECommerce.Domain.Auth;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_COMPANY)]
        public async Task<IActionResult> Create([FromBody] ProductInputModel product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProduct(product, User.Identity.Name);

                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("listcompanyproducts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_COMPANY)]
        public async Task<IActionResult> ListCompanyProducts()
        {
            var products = await _productService.ListProductsByCompany(User.Identity.Name);

            return Ok(products);
        }

        [HttpGet]
        [Route("listallproducts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Roles.ROLE_ADMIN},{Roles.ROLE_API}")]
        public async Task<IActionResult> ListAllProducts()
        {
            var products = await _productService.ListAllProducts();

            return Ok(products);
        }
    }
}
