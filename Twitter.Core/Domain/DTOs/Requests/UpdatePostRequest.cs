namespace Twitter.Core.Domain.DTOs.Requests
{
    public class UpdatePostRequest
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int CategoryId { get; set; }
    }
}
