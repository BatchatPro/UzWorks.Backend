using UzWorks.Core.DataTransferObjects.FAQs;

namespace UzWorks.BL.Services.FAQs;

public interface IFAQService
{
    Task<FAQVM> Create(FAQDto dto);
    Task<IEnumerable<FAQVM>> GetAllAsync();
    Task<FAQVM> GetById(Guid Id);
    Task<FAQVM> Update(FAQEM EM);
    Task<bool> Delete(Guid Id);
}
