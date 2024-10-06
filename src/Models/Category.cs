
using System.Text.Json.Serialization;



public class Category{
  public Guid CategoryId {get;set;}
  public string Name {get;set;} = string.Empty;
  public string Slug {get;set;} = string.Empty;
  public DateTime CreatedAt {get;set;}


  [JsonIgnore]
  public ICollection<Product> Products {get;set;}
}
