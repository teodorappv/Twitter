
namespace Twitter.Core.Contracts.V1.Request
{
    public class UpdatePostRequest
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int CategoryId { get; set; }
    }
}
