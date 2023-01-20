namespace Twitter.Core.Domain.DTOs.Request
{
    public class UpdatePostRequest
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int CategoryId { get; set; }
    }
}
