


public class Rating {


  public Guid RatingId {get;set;}

  public string FeedBack{get;set;} = string.Empty;

  public int RatingScore {get;set;}

  public DateTime CreatedAt {get;set;}  = DateTime.UtcNow;


}
