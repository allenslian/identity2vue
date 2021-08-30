
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

        Task<ProductItemDTO> GetProductByIdAsync(Guid id);

        Task<string> AddProductAsync(CreateProductCommand command, string user);

        Task ChangeProductAsync(Guid id, ChangeProductCommand command);

        Task RemoveProductAsync(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly static IList<Product> _products = new List<Product>
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

        public async Task<ProductItemDTO> GetProductByIdAsync(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            return await Task.FromResult(new ProductItemDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SalesPrice = product.SalesPrice
            });
        }

        public Task<string> AddProductAsync(CreateProductCommand command,
          string user)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                SalesPrice = command.SalesPrice,
                DateCreated = DateTime.Now,
                CreatedBy = user
            };
            _products.Add(product);
            return Task.FromResult<string>(product.Id.ToString());
        }

        public Task ChangeProductAsync(Guid id, ChangeProductCommand command)
        {
            var product = _products.FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                throw new ArgumentException("不能找到此产品");
            }
            product.Name = command.Name;
            product.Description = command.Description;
            product.SalesPrice = command.SalesPrice;
            return Task.CompletedTask;
        }

        public Task RemoveProductAsync(Guid id)
        {
            var product = _products.FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                throw new ArgumentException("不能找到此产品");
            }
            _products.Remove(product);
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