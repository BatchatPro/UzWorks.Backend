﻿using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.FAQs;
using UzWorks.Core.Entities.FAQs;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.FAQs;

namespace UzWorks.BL.Services.FAQs;

public class FAQService : IFAQService
{
    private readonly IFAQsRepository _repository;
    private readonly IMappingService _mapping;
    private readonly IEnvironmentAccessor _environment;

    public FAQService(IFAQsRepository repository, IMappingService mapping, IEnvironmentAccessor environment)
    {
        _repository = repository;
        _mapping = mapping;
        _environment = environment;
    }

    public async Task<FAQVM> Create(FAQDto dto)
    {
        if (dto == null)
            throw new UzWorksException($"FAQ Dto can not be null.");

        var faq = _mapping.Map<FAQ, FAQDto>(dto);

        faq.CreateDate = DateTime.Now;
        faq.CreatedBy = Guid.Parse(_environment.GetUserId());

        await _repository.CreateAsync(faq);
        await _repository.SaveChanges();

        return _mapping.Map<FAQVM, FAQ>(faq);
    }

    public async Task<IEnumerable<FAQVM>> GetAllAsync()
    {
        var contacts = await _repository.GetAllAsync();

        return _mapping.Map<IEnumerable<FAQVM>, IEnumerable<FAQ>>(contacts);
    }

    public async Task<FAQVM> GetById(Guid Id)
    {
        var faq = await _repository.GetById(Id) ?? 
            throw new UzWorksException($"Could not find FAQ with {Id}");

        return _mapping.Map<FAQVM, FAQ>(faq);
    }

    public async Task<FAQVM> Update(FAQEM EM)
    {
        var faq = await _repository.GetById(EM.Id) ??
            throw new UzWorksException($"Could not find FAQ with {EM.Id}");
        
        if (!_environment.IsAuthorOrSupervisor(EM.Id))
            throw new UzWorksException("You have not access to change this FAQ data.");

        _mapping.Map(EM, faq);

        faq.UpdateDate = DateTime.Now;
        faq.UpdatedBy = Guid.Parse(_environment.GetUserId());

        _repository.UpdateAsync(faq);
        await _repository.SaveChanges();

        return _mapping.Map<FAQVM, FAQ>(faq);
    }

    public async Task<bool> Delete(Guid Id)
    {
        var faq = await _repository.GetById(Id) ??
            throw new UzWorksException($"Could not find FAQ with {Id}");

        if (!_environment.IsAuthorOrSupervisor(faq.CreatedBy))
            throw new UzWorksException("You have not access to change this FAQ data.");

        _repository.Delete(faq);
        await _repository.SaveChanges();

        return true; 
    }
}
