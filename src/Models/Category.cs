public class Category{
  public Guid CategoryId {get;set;}
  public string Name {get;set;} = string.Empty;
  public string Slug {get;set;} = string.Empty;
  public DateTime CreatedAt {get;set;}

  public List<Product> Products {get;set;}
}