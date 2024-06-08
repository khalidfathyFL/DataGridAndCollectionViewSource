namespace DataGridAndCollectionViewSource.Models;

public class Specification
{
  public string Name { get; set; }
  public string Value { get; set; }
  public string Description { get; set; }

  public Specification()
  {
    
  }
  public Specification(string name, string value, string description = "")
  {
    Name = name;
    Value = value;
    Description = description;
  }

}