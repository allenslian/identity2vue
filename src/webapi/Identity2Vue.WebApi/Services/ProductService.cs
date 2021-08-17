
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity2Vue.WebApi.Models;

namespace Identity2Vue.WebApi.Services
{
  public interface IProductService
  {
    Task<IEnumerable<ProductItemDTO>> GetProductsAsync();

    Task AddProductAsync(CreateProductCommand command, string user);
  }

  public class ProductService : IProductService
  {
    private IList<Product> _products = new List<Product>
    {
      new Product{
        Id = new Guid("3ef345e7-bcdf-4b50-9cd6-5e791c89b7e1"),
        Name = "hello",
        Description = "hello is one product!",
        SalesPrice = 2.33M,
        DateCreated = new DateTime(2021, 1, 1),
        CreatedBy = "allen"
      },
      new Product{
        Id = new Guid("3ef345e7-bcdf-4b50-9cd6-5e791c89b7e2"),
        Name = "world",
        Description = "world is one product!",
        SalesPrice = 5.73M,
        DateCreated = new DateTime(2021, 1, 1),
        CreatedBy = "allen"
      }
    };

    public async Task<IEnumerable<ProductItemDTO>> GetProductsAsync()
    {
      return await Task.FromResult(_products.Select(p => new ProductItemDTO
      {
        Id = p.Id,
        Name = p.Name,
        Description = p.Description,
        SalesPrice = p.SalesPrice
      }).ToList());
    }

    public Task AddProductAsync(
      CreateProductCommand command, 
      string user)
    {
      _products.Add(new Product{
        Id = Guid.NewGuid(),
        Name = command.Name,
        Description = command.Description,
        SalesPrice = command.SalesPrice,
        DateCreated = DateTime.Now,
        CreatedBy = user
      });
      return Task.CompletedTask;
    }
  }

  public class Product
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal SalesPrice { get; set; }

    public DateTime DateCreated { get; set; }

    public string CreatedBy { get; set; }
  }
}