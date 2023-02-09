using FluentResults;
using Twitter.Core.Domain.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<List<Post>> ReadAllPosts(PostParameters postParameters);
        Task<int> NumberOfAvailablePosts(int? categoryId);
        Task<Post> GetPostById(int id);
        Task<Post> CreatePost(Post postRequest);
        Task<Result<Post>> UpdatePost(Post postRequest);
        Task<bool> DeletePost(int id);
        Task<bool> IsOwner(int postId, string userId);
        Task<List<Post>> UserNonArchivedPosts(string userId);
    }
}
