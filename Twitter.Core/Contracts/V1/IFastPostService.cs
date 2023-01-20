using Twitter.Core.Domain.Entities;

namespace Twitter.Core.Contracts.V1
{
    public interface IFastPostService
    {
        Task<FastPost> CreateFastPost(FastPost request);
    }
}
