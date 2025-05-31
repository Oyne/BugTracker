namespace BugTracker.Shared.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; } = default!;
        public string? Error { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
}
