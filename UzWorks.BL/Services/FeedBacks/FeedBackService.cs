﻿using UzWorks.Core.Abstract;
using UzWorks.Core.DataTransferObjects.FeedBacks;
using UzWorks.Core.Entities.FAQs;
using UzWorks.Core.Entities.Feedbacks;
using UzWorks.Core.Exceptions;
using UzWorks.Persistence.Repositories.FeedBacks;

namespace UzWorks.BL.Services.FeedBacks;

public class FeedBackService : IFeedBackService
{
    private readonly IFeedBacksRepository _feedBacksRepository;
    private readonly IMappingService _mappingService;
    private readonly IEnvironmentAccessor _environmentAccessor;

    public FeedBackService(IFeedBacksRepository feedBacksRepository, IMappingService mappingService, IEnvironmentAccessor environmentAccessor)
    {
        _feedBacksRepository = feedBacksRepository;
        _mappingService = mappingService;
        _environmentAccessor = environmentAccessor;
    }

    public async Task<FeedBackVM> Create(FeedBackDto dto)
    {
        if (dto == null)
            throw new UzWorksException($"FeedBack Dto can not be null.");

        var feedBack = _mappingService.Map<FeedBack, FeedBackDto>(dto);

        feedBack.CreateDate = DateTime.Now;
        feedBack.CreatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        await _feedBacksRepository.CreateAsync(feedBack);
        await _feedBacksRepository.SaveChanges();

        return _mappingService.Map<FeedBackVM, FeedBack>(feedBack);
    }

    public async Task<IEnumerable<FeedBackVM>> GetAllAsync()
    {
        var feedBacks = await _feedBacksRepository.GetAllAsync();

        return _mappingService.Map<IEnumerable<FeedBackVM>, IEnumerable<FeedBack>>(feedBacks);
    }

    public async Task<FeedBackVM> GetById(Guid Id)
    {
        var feedBack = await _feedBacksRepository.GetById(Id) ?? 
            throw new UzWorksException($"Could not find FeedBack with {Id}");

        return _mappingService.Map<FeedBackVM, FeedBack>(feedBack);
    }

    public async Task<FeedBackVM> Update(FeedBackEM EM)
    {
        var feedBack = await _feedBacksRepository.GetById(EM.Id) ?? 
            throw new UzWorksException($"Could not find FeedBack with {EM.Id}");

        if (!_environmentAccessor.IsAuthorOrSupervisor(feedBack.CreatedBy))
            throw new UzWorksException("You have not access to change this FeedBack data.");

        _mappingService.Map(EM, feedBack);

        feedBack.UpdateDate = DateTime.Now;
        feedBack.UpdatedBy = Guid.Parse(_environmentAccessor.GetUserId());

        _feedBacksRepository.UpdateAsync(feedBack);
        await _feedBacksRepository.SaveChanges();

        return _mappingService.Map<FeedBackVM, FeedBack>(feedBack);
    }

    public async Task<bool> Delete(Guid Id)
    {
        var feedBack = await _feedBacksRepository.GetById(Id)??
            throw new UzWorksException($"Could not find FeedBack with id: {Id}");

        if(!_environmentAccessor.IsAuthorOrSupervisor(feedBack.CreatedBy))
            throw new UzWorksException("You have not access to delete this FeedBack data.");

        _feedBacksRepository.Delete(feedBack);
        await _feedBacksRepository.SaveChanges();

        return true;    
    }
}
