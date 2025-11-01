using Applications.Dto.Issue;

namespace Services.Interfaces;

public interface IIssueService
{
    public Task<long> AddIssueAsync(AddIssueDto issue, CancellationToken token);
    
    public Task<ShowIssueDto> ShowIssueAsync(int id, CancellationToken token);

    public Task<List<ShowIssueDto>> ShowAllIssuesAsync(CancellationToken token);
    
    public Task<bool> UpdateIssueAsync(UpdateIssueDto issueDto, CancellationToken token);
}