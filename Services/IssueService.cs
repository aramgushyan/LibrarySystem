using Applications.Dto.Issue;
using Applications.Exceptions;
using Applications.Validators.IssueValidators;
using Domain.Models;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Services.Helpers;
using Services.Interfaces;

namespace Services;

public class IssueService:IIssueService
{
    private readonly IIssueRepository _issueRepository;
    
    private readonly IReaderRepository _readerRepository;
    
    private readonly IBookRepository _bookRepository;
    
    private readonly IValidator<Issue>  _issueValidator;
    
    private readonly IValidator<UpdateIssueDto>  _updateIssueValidator;

    public IssueService(
        IIssueRepository issueRepository,
        IValidator<Issue> issueValidator,
        IReaderRepository readerRepository,
        IBookRepository bookRepository,
        IValidator<UpdateIssueDto> updateIssueValidator)
    {
        _issueRepository = issueRepository;
        _issueValidator = issueValidator;
        _readerRepository = readerRepository;
        _bookRepository = bookRepository;
        _updateIssueValidator = updateIssueValidator;
    }

    public async Task<long> AddIssueAsync(AddIssueDto issueDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(issueDto);
        
        IssueTrim.TrimAddIssueDto(issueDto);
        
        await _bookRepository.ShowBookByCodeAsync(issueDto.LibraryCode, token);
        await _readerRepository.GetReaderAsync(issueDto.ReaderCardNumber, token);
        
        Issue issue = new Issue()
        {
            LibraryCode = issueDto.LibraryCode,
            ReaderCardNumber = issueDto.ReaderCardNumber,
            ExpectedReturnDate = issueDto.ExpectedReturnDate,
            IssueDate = DateOnly.FromDateTime(DateTime.Now)
        };
        
        WorkValidation.CheckResult(await _issueValidator.ValidateAsync(issue, token));
        
        return await _issueRepository.AddIssueAsync(issue, token);
    }

    public async Task<ShowIssueDto> ShowIssueAsync(int id, CancellationToken token)
    {
        var issue = await _issueRepository.ShowIssueAsync(id, token);

        return new ShowIssueDto()
        {
          IssueDate  = issue.IssueDate,
          ActualReturnDate = issue.ActualReturnDate,
          ExpectedReturnDate = issue.ExpectedReturnDate,
          LibraryCode = issue.LibraryCode,
          ReaderCardNumber = issue.ReaderCardNumber,
          Id = issue.Id
        };
    }

    public async Task<List<ShowIssueDto>> ShowAllIssuesAsync(CancellationToken token)
    {
        var issuesList = await _issueRepository.ShowAllIssuesAsync(token);

        return issuesList.Select(i => new ShowIssueDto()
        {
            IssueDate  = i.IssueDate,
            ActualReturnDate = i.ActualReturnDate,
            ExpectedReturnDate = i.ExpectedReturnDate,
            LibraryCode = i.LibraryCode,
            ReaderCardNumber = i.ReaderCardNumber,
            Id = i.Id 
        }).ToList();
    }

    public async Task<bool> UpdateIssueAsync(UpdateIssueDto issue, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(issue);
        
        IssueTrim.TrimUpdateIssueDto(issue);
        
        WorkValidation.CheckResult(await _updateIssueValidator.ValidateAsync(issue, token));
        
        if (await _issueRepository.HasReaderDelay(issue.ReaderCardNumber, token))
            throw new MyValidationException("Читатель не вернул книгу");

        return await _issueRepository.UpdateIssueAsync(issue.ReaderCardNumber, issue.LibraryCode, token);
    }
}   