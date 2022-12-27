using Twitter.Core.Contracts.V1.Request;
using Twitter.Core.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPostsById(int id);
        Task<Post> CreatePost(CreatePostRequest postRequest);
        Task<Post> UpdatePost(UpdatePostRequest postRequest);
        Task<bool> DeletePost(int id);
       
    }
}
