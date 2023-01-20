namespace Twitter.Core.Domain.DTOs.Request
{
    public class CreatePostRequest
    {
        public string? Text { get; set; }
        public int CategoryId { get; set; }
        public string CreatedById { get; set; }

    }
}
