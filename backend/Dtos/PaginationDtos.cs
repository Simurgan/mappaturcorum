public class PaginationRequest<T>
{
    public T? Filter {get; set;}
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class PaginationResponse<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}