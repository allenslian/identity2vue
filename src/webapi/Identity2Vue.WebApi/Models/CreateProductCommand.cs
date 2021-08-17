

namespace Identity2Vue.WebApi.Models
{
  public class CreateProductCommand
  {
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal SalesPrice { get; set; }
  }
}