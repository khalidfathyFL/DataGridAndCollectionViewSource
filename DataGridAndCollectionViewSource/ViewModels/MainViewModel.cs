using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataGridAndCollectionViewSource.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DataGridAndCollectionViewSource.ViewModels;

internal partial class MainViewModel : ObservableObject
{
  private ICollectionView collectionView;

  #region Properties

  // Observable collection of orders
  [ObservableProperty] private ObservableCollection<Order> _orders;

  // Search text for filtering orders
  [ObservableProperty] private string _searchText;

  // Refresh the collection view when the search text changes
  partial void OnSearchTextChanged(string value)
  {
    collectionView.Refresh();
  }

  // Constructor to initialize orders and collection view
  public MainViewModel()
  {
   
    Orders = new ObservableCollection<Order>(GetOrders());
    collectionView = CollectionViewSource.GetDefaultView(Orders);
    collectionView.Filter = FilterOrders;
  }

  #endregion


  #region Data Initialization

  /// <summary>
  /// Simulating Database
  /// </summary>
  /// <returns>List of orders</returns>
  private IEnumerable<Order> GetOrders()
  {
    return new List<Order>
    {
      new Order(1, new DateTime(2023, 5, 1), "John Doe", "123 Main St, Anytown, USA", "Shipped", 1049.98m)
      {
        Products = new List<Product>
        {
          new Product("Laptop", 999.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Dell", "High performance laptop"),
              new Specification("Model", "XPS 13", "13-inch display")
            }
          },
          new Product("Mouse", 49.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Logitech", "Wireless mouse"),
              new Specification("Type", "Wireless", "2.4 GHz connection")
            }
          }
        }
      },
      new Order(2, new DateTime(2023, 5, 2), "Jane Smith", "456 Oak St, Othertown, USA", "Processing", 929.98m)
      {
        Products = new List<Product>
        {
          new Product("Smartphone", 799.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Apple", "Latest model"),
              new Specification("Model", "iPhone 13", "6.1-inch display")
            }
          },
          new Product("Earbuds", 129.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Samsung", "Noise-cancelling"),
              new Specification("Type", "Bluetooth", "Wireless connectivity")
            }
          }
        }
      },
      new Order(3, new DateTime(2023, 5, 3), "Alice Johnson", "789 Pine St, Smalltown, USA", "Delivered", 389.98m)
      {
        Products = new List<Product>
        {
          new Product("Monitor", 299.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "LG", "4K resolution"),
              new Specification("Size", "27 inches", "Large display")
            }
          },
          new Product("Keyboard", 89.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Corsair", "RGB backlit"),
              new Specification("Type", "Mechanical", "High durability")
            }
          }
        }
      },
      new Order(4, new DateTime(2023, 5, 4), "Bob Brown", "101 Maple St, Village, USA", "Cancelled", 699.98m)
      {
        Products = new List<Product>
        {
          new Product("Tablet", 599.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Microsoft", "Versatile use"),
              new Specification("Model", "Surface Pro 7", "2-in-1 tablet and laptop")
            }
          },
          new Product("Stylus", 99.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Microsoft", "Precision stylus"),
              new Specification("Type", "Surface Pen", "Compatible with Surface Pro")
            }
          }
        }
      },
      new Order(5, new DateTime(2023, 5, 5), "Charlie Davis", "202 Birch St, Hamlet, USA", "Pending", 229.98m)
      {
        Products = new List<Product>
        {
          new Product("Headphones", 199.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Sony", "High-quality sound"),
              new Specification("Type", "Over-Ear", "Comfortable fit")
            }
          },
          new Product("Charger", 29.99m)
          {
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Anker", "Fast charging"),
              new Specification("Type", "Fast Charging", "Quick charge technology")
            }
          }
        }
      },
      new Order
      {
        Id = 6,
        OrderDate = new DateTime(2023, 5, 6),
        CustomerName = "Emma Wilson",
        ShippingAddress = "303 Cedar St, Smalltown, USA",
        OrderStatus = "Shipped",
        TotalAmount = 559.98m,
        Products = new List<Product>
        {
          new Product
          {
            Name = "Smartwatch",
            Price = 299.99m,
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Garmin", "High-end smartwatch"),
              new Specification("Model", "Fenix 6", "Advanced features")
            }
          },
          new Product
          {
            Name = "Fitness Tracker",
            Price = 259.99m,
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Fitbit", "Accurate fitness tracking"),
              new Specification("Model", "Charge 4", "Includes GPS")
            }
          }
        }
      },
      new Order
      {
        Id = 7,
        OrderDate = new DateTime(2023, 5, 7),
        CustomerName = "Liam Moore",
        ShippingAddress = "404 Spruce St, Anytown, USA",
        OrderStatus = "Delivered",
        TotalAmount = 1299.98m,
        Products = new List<Product>
        {
          new Product
          {
            Name = "Gaming Laptop",
            Price = 1299.99m,
            Specifications = new List<Specification>
            {
              new Specification("Brand", "Alienware", "Top-tier gaming performance"),
              new Specification("Model", "M15 R3", "15-inch display")
            }
          }
        }
      }
    };
  }

  #endregion

  #region Collection View Filter, Sort, and Grouping

  // Filter orders based on search text
  private bool FilterOrders(object item)
  {
    if (item is Order order)
    {
      if (string.IsNullOrEmpty(SearchText))
      {
        return true;
      }

      return order.Id.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
             order.OrderDate.ToString("d").Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
             order.CustomerName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
             order.ShippingAddress.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
             order.OrderStatus.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
             order.TotalAmount.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase);
    }

    return false;
  }

  #endregion

  #region Commands

  // Command to move selection down
  [RelayCommand]
  private void Down(object? obj)
  {
    if (collectionView.CurrentPosition < Orders.Count - 1)
    {
      collectionView.MoveCurrentToNext();
    }
  }

  // Command to move selection up
  [RelayCommand]
  private void Up(object? obj)
  {
    if (collectionView.CurrentPosition > 0)
    {
      collectionView.MoveCurrentToPrevious();
    }
  }

  // Command to apply sorting to the collection view
  [RelayCommand]
  private void Sort(object? obj)
  {
    collectionView.SortDescriptions.Clear();
    collectionView.SortDescriptions.Add(new SortDescription(nameof(Order.OrderDate), ListSortDirection.Ascending));
    collectionView.SortDescriptions.Add(new SortDescription(nameof(Order.CustomerName), ListSortDirection.Ascending));
    collectionView.SortDescriptions.Add(new SortDescription(nameof(Order.TotalAmount), ListSortDirection.Descending));
  }

  // Command to apply grouping to the collection view
  [RelayCommand]
  private void Group(object? obj)
  {
    collectionView.GroupDescriptions.Clear();

    collectionView.GroupDescriptions.Add(new PropertyGroupDescription("OrderStatus"));

  }

  #endregion
}