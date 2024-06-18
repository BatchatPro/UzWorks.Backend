using UzWorks.Core.DataTransferObjects.FeedBacks;
using UzWorks.Core.Entities.Feedbacks;

namespace UzWorks.BL.Services.FeedBacks;

public interface IFeedBackService
{
    Task<FeedBackVM> Create(FeedBackDto dto);
    Task<IEnumerable<FeedBackVM>> GetAllAsync();
    Task<FeedBackVM> GetById(Guid Id);
    Task<FeedBackVM> Update(FeedBackEM EM);
    Task<bool> Delete(Guid Id);
}
