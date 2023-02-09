using Twitter.Core.Domain.Entities;

namespace Twitter.Core.Domain.DTOs.Responses
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public List<Post> Posts { get; set; }
    }
}
