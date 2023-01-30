using FluentResults;
using Twitter.Core.Domain.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface IFastPostService
    {
        Task<Result<FastPost>> CreateFastPost(FastPost request);
        Task<FastPost> ReadFastPost(int id);
        Task<List<FastPost>> ReadAllFastPosts();
        Task<bool> DeleteFastPost (int id);
    }
}
