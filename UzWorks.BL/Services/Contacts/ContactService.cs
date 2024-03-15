using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.Contacts;
using UzWorks.Core.Entities.Contacts;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.Contacts;

namespace UzWorks.BL.Services.Contacts;

public class ContactService : IContactService
{
    public readonly IContactsRepository _contactsRepository;
    public readonly IMappingService _mappingService;
    public readonly IEnvironmentAccessor _environmentAccessor;
    public ContactService(IContactsRepository contactsRepository, IMappingService mappingService, IEnvironmentAccessor environmentAccessor)
    {
        _contactsRepository = contactsRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor;
    }

    public async Task<ContactVM> Create(ContactDto contactDto)
    {
        if (contactDto == null)
            throw new UzWorksException($"Contact Dto can not be null.");
        
        var contact = _mappingService.Map<Contact, ContactDto>( contactDto );

        contact.CreateDate = DateTime.Now;

        await _contactsRepository.CreateAsync(contact);
        await _contactsRepository.SaveChanges();
        
        return _mappingService.Map<ContactVM, Contact>(contact);
    }

    public async Task<bool> Delete(Guid Id)
    {
        var contact = await _contactsRepository.GetById(Id);

        if (contact == null)
            return false;
           
        _contactsRepository.Delete(contact);
        await _contactsRepository.SaveChanges();

        return true; 
    }

    public async Task<IEnumerable<ContactVM>> GetAllContactsAsync(int pageNumber, int pageSize, bool? isComplated)
    {
        var contacts = await _contactsRepository.GetAllContactsAsync(pageNumber, pageSize, isComplated);

        return _mappingService.Map<IEnumerable<ContactVM>, IEnumerable<Contact>>(contacts);
    }

    public async Task<ContactVM> GetById(Guid id)
    {
        if (id == null) 
            throw new UzWorksException("Id can not be null.");

        var contact = await _contactsRepository.GetById(id);

        if (contact == null)
            throw new UzWorksException($"Could not find contact with id: {id}");

        return _mappingService.Map<ContactVM, Contact>(contact);
    }

    public async Task<ContactVM> Update(ContactEM contactEM)
    {
        if (contactEM == null)
            throw new UzWorksException("Contact EM can not be null.");  

        var contact = _mappingService.Map<Contact, ContactEM>(contactEM);

        contact.UpdateDate = DateTime.Now;
        contact.UpdatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        if (!_environmentAccessor.IsAuthorOrSupervisor(contact.CreatedBy))
            throw new UzWorksException("You have not access to change this Contact data.");

        _contactsRepository.UpdateAsync(contact);
        await _contactsRepository.SaveChanges();

        return _mappingService.Map<ContactVM, Contact>(contact);
    }

    public async Task<bool> ChangeStatus(Guid id, bool status)
    {
        var contact = await _contactsRepository.GetById(id);

        if (contact == null)
            throw new UzWorksException($"Could not find contact with id: {id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(contact.CreatedBy))
            throw new UzWorksException("You have not access to change this Contact status.");

        contact.IsComplated = status;
        _contactsRepository.UpdateAsync(contact);
        await _contactsRepository.SaveChanges();
        return true;
    }
}
