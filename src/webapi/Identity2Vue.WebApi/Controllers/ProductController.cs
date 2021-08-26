using System.Threading.Tasks;
using Identity2Vue.WebApi.Models;
using Identity2Vue.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Identity2Vue.WebApi.Controllers
{
  [ApiController]
  [Route("products")]
  public class ProductController : ControllerBase
  {
    private IProductService _service;

    private ILogger<ProductController> _logger;
    
    public ProductController(
      IProductService service,
      ILogger<ProductController> logger)
    {
      _service = service;
      _logger = logger;
    }

    [HttpGet, Route("")]
    [Authorize(Policy = "platform.scopes")]
    public async Task<IActionResult> GetProductsAsync()
    {
      return Ok(await _service.GetProductsAsync());
    }

    [HttpPost, Route("")]
    [Authorize(Policy = "platform.scopes")]
    public async Task<IActionResult> AddProductAsync(
      [FromBody]CreateProductCommand command)
    {
      await _service.AddProductAsync(command, "allen");
      return Ok();
    }
  }
}