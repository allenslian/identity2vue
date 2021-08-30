using System;
using System.Threading.Tasks;
using System.Linq;
using Identity2Vue.WebApi.Models;
using Identity2Vue.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Identity2Vue.WebApi.Controllers
{
  [ApiController]
  [Route("products")]
  [Produces("application/json")]
  public class ProductController : ControllerBase
  {
    private readonly IProductService _service;

    private readonly ILogger<ProductController> _logger;
    
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

    [HttpGet, Route("{id}")]
    [Authorize(Policy = "platform.scopes")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id)
    {
      return Ok(await _service.GetProductByIdAsync(id));
    }

    [HttpPost, Route("")]
    [Authorize(Policy = "platform.scopes")]
    public async Task<ActionResult<string>> AddProductAsync(
      [FromBody]CreateProductCommand command)
    {
      var username = HttpContext.User.Claims.FirstOrDefault(m => m.Type == "name");
      if (username == null || string.IsNullOrEmpty(username.Value))
      {
        return BadRequest("Not found login name!");
      }
      var id = await _service.AddProductAsync(command, username.Value);
      return Ok(id);
    }

    [HttpPut, Route("{id}")]
    [Authorize(Policy = "platform.scopes")]
    public async Task<ActionResult<string>> ChangeProductAsync(Guid id,
      [FromBody]ChangeProductCommand command)
    {
      await _service.ChangeProductAsync(id, command);
      return Ok(id.ToString());
    }

    [HttpDelete, Route("{id}")]
    [Authorize(Policy = "platform.scopes")]
    public async Task<ActionResult<string>> RemoveProductAsync(Guid id)
    {
      await _service.RemoveProductAsync(id);
      return Ok(id.ToString());
    }
  }
}