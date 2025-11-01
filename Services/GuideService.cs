using Applications.Dto;
using Domain.Interfaces.Repositories;
using Domain.Models;
using FluentValidation;
using Services.Helpers;
using Services.Interfaces;

namespace Services;

public class GuideService:IGuideService
{
    private readonly IReaderRepository  _readerRepository;
    
    private readonly IBookRepository _bookRepository;
    
    private readonly IIssueRepository _issueRepository;
    
    private readonly IValidator<PeriodDto>  _periodDtoValidator;

    public GuideService(
        IReaderRepository readerRepository,
        IBookRepository bookRepository,
        IIssueRepository issueRepository,
        IValidator<PeriodDto> periodDtoValidator)
    {
        _readerRepository = readerRepository;
        _bookRepository = bookRepository;
        _issueRepository = issueRepository;
        _periodDtoValidator = periodDtoValidator;
    }

    public async Task<List<Issue>> ShowIssuesByPeriod(PeriodDto periodDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(periodDto);

        WorkValidation.CheckResult(await _periodDtoValidator.ValidateAsync(periodDto, token));

        return await _issueRepository.SelectIssuesByPeriodAsync(periodDto.StartDate, periodDto.EndDate, token);
    }

    public async Task<List<Issue>> ShowReturnsByPeriod(PeriodDto periodDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(periodDto);

        WorkValidation.CheckResult(await _periodDtoValidator.ValidateAsync(periodDto, token));

        return await _issueRepository.SelectReturnsByPeriodAsync(periodDto.StartDate, periodDto.EndDate, token);
    }

    public async Task<List<string>> ShowBooksReader(string readerCardNumber, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(readerCardNumber);
        
        readerCardNumber = readerCardNumber.Trim();
        
        WorkValidation.ValidateReadCardNumber(readerCardNumber);
        
        return await _issueRepository.ShowBooksReader(readerCardNumber, token);
    }

    public async Task<bool> IsStock(string libraryCode, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(libraryCode);
        
        libraryCode = libraryCode.Trim();
        
        WorkValidation.ValidateBook(libraryCode);
        
        return await _bookRepository.IsStock(libraryCode, token);
    }
    
}