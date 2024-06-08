using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridAndCollectionViewSource.Models
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } 
    public string CustomerName { get; set; }
    public string ShippingAddress { get; set; }
    public string OrderStatus { get; set; }  
    public decimal TotalAmount { get; set; }
    public List<Product> Products { get; set; }

    public Order()
    {
      
    }
    public Order(int id, DateTime orderDate, string customerName, string shippingAddress, string orderStatus, decimal totalAmount)
    {
      Id = id;
      OrderDate = orderDate;
      CustomerName = customerName;
      ShippingAddress = shippingAddress;
      OrderStatus = orderStatus;
      TotalAmount = totalAmount;
      Products = new List<Product>();
    }

  }

}
