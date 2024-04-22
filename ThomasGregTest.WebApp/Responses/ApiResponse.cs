namespace ThomasGregTest.WebApp;

public class ApiResponse<T>
{
    public ApiResponse(bool success, T data, int totalRows, int id)
    {
        Success = success;
        Data = data;
        TotalRows = totalRows;
        Id = id;
    }

    public bool Success { get; }
    public T Data { get; }
    public int TotalRows { get; }
    public int Id { get; }
}
