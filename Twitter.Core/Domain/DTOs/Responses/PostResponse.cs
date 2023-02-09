namespace Twitter.Core.Domain.DTOs.Responses
{
    public class PostResponse<T>
    {
        PostResponse() { }

        public PostResponse(List<T> response) {
            Posts = response;
        }

        public List<T> Posts { get; set; }
    }
}
