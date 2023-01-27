namespace Twitter.Core.Domain.DTOs.Requests
{
    public class CreatePostRequest
    {
        public string? Text { get; set; }
        public int CategoryId { get; set; }

    }
}
