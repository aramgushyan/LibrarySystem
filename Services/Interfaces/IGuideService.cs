using Applications.Dto;
using Domain.Models;

namespace Services.Interfaces;

public interface IGuideService
{
    public Task<List<Issue>> ShowIssuesByPeriod(PeriodDto periodDto, CancellationToken token);
    
    public Task<List<Issue>> ShowReturnsByPeriod(PeriodDto periodDto, CancellationToken token);
    
    public Task<List<string>> ShowBooksReader(string readerCardNumber, CancellationToken token);
    
    public Task<bool> IsStock(string libraryCode, CancellationToken token);
}