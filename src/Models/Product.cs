using System.Text.Json.Serialization;


public class Product{



  public Guid ProductId {get;set;}
  public required string Name {get;set;}
  public decimal Price {get;set;}
  public int Quantity {get;set;}
  public string? Description {get;set;}
  public string? ImageIDs {get;set;}
  public int? ReturnByDaysAfterOrder {get;set;} =2;
  public bool IsOnSale = false;
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;

    public Guid CategoryId {get;set;}



  [JsonIgnore]
  public Category? Category {get;set;}


    [JsonIgnore]
      public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

  // List<Rating>

      [JsonIgnore]

    public ICollection<Rating?> Ratings {get;set;} = new List<Rating>();







}
