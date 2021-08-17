
using System;

namespace Identity2Vue.WebApi.Models
{
  public class ProductItemDTO
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal SalesPrice { get; set; }
  }
}