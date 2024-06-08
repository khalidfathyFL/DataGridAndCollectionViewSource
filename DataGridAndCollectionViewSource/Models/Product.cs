namespace DataGridAndCollectionViewSource.Models;

public class Product
{
  public string Name { get; set; }
  public decimal Price { get; set; } 
  public List<Specification> Specifications { get; set; }

  public Product()
  {
    
  }
  public Product(string name, decimal price)
  {
    Name = name;
    Price = price;
    Specifications = new List<Specification>();
  }


}