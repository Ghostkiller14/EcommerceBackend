



public class PagedResult<T>{

  public int PageNumber {get;set;} = 1;
  public int PageSize{get;set;} = 4;
  public int TotalPages {get;set;}
  public int TotalItems {get;set;}
  public IEnumerable<T> Items {get;set;}



}
