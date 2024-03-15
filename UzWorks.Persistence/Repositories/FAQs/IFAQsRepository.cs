using UzWorks.Core.Entities.FAQs;

namespace UzWorks.Persistence.Repositories.FAQs;

public interface IFAQsRepository : IGenericRepository<FAQ>
{
    Task<FAQ[]> GetAllFAQsAsync();
}
