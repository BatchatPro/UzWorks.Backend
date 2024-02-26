using UzWorks.Core.DataTransferObjects.Contacts;

namespace UzWorks.BL.Services.Contacts;

public interface IContactService
{
    Task<ContactVM> Create(ContactDto contactDto);
    Task<IEnumerable<ContactVM>> GetAllContactsAsync(int pageNumber, int pageSize, bool? isComplated);
    Task<ContactVM> GetById(Guid id);
    Task<ContactVM> Update(ContactEM contactEM);
    Task<bool> ChangeStatus(Guid id, bool status);
    Task<bool> Delete(Guid Id);
}
