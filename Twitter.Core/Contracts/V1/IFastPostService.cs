using FluentResults;
using Twitter.Core.Domain.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface IFastPostService
    {
        Task<Result<FastPost>> CreateFastPost(FastPost request);
        Task<Result<FastPost>> ReadFastPost(int id);
        Task<Result<List<FastPost>>> ReadAllFastPosts();
        Task<Result<bool>> DeleteFastPost (int id);
    }
}
