using UzWorks.Core.Entities.SMS;
using UzWorks.Persistence.Data;

namespace UzWorks.Persistence.Repositories.SMSTokens;

public class SmsTokensRepository : GenericRepository<SmsToken>, ISmsTokensRepository
{
    protected SmsTokensRepository(UzWorksDbContext context) : base(context)
    {
    }
}
