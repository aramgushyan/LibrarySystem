using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IIssueRepository
{
    public Task<long> AddIssueAsync(Issue issue, CancellationToken token);
    
    public Task<Issue> ShowIssueAsync(long id, CancellationToken token);

    public Task<List<Issue>> ShowAllIssuesAsync(CancellationToken token);

    public Task<bool> UpdateIssueAsync(string readerCardNumber, string libraryCode, CancellationToken token);

    public Task<bool> HasReaderDelay(string readerCardNumber, CancellationToken token);

    public Task<List<string>> ShowBooksReader(string readerCardNumber, CancellationToken token);

    public Task<List<Issue>> SelectReturnsByPeriodAsync(DateOnly startPeriod, DateOnly endPeriod,
        CancellationToken token);

    public Task<List<Issue>> SelectIssuesByPeriodAsync(DateOnly startPeriod, DateOnly endPeriod,
        CancellationToken token);

}