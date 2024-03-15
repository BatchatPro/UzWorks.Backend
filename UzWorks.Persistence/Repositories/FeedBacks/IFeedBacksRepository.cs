using UzWorks.Core.Entities.Feedbacks;

namespace UzWorks.Persistence.Repositories.FeedBacks;

public interface IFeedBacksRepository : IGenericRepository<FeedBack>
{
    Task<FeedBack[]> GetAllAsync();
}
