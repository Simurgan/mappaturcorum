public class PaginationRequest<T>
{
    public T? Filter {get; set;}
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    // Name or YearWritten
    public string SortingField {get; set;} = "Name";
    public bool IsDescendingOrder {get; set;} = false;
}

public class PaginationResponse<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}