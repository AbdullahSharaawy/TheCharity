
namespace TheCharityBLL.DTOs
{
    public class ServiceResponce<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
