public class QueryParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = "";
    public string SortBy { get; set; } = "Name";
    public string SortOrder { get; set; } = "asc";
}
