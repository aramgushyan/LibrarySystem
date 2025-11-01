using Domain.Models;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class IssueRepository:IIssueRepository
{
    private readonly LibraryDbContext _context;

    public IssueRepository(LibraryDbContext context)
    {
        _context = context;   
    }

    public async Task<long> AddIssueAsync(Issue issue, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(issue);
        
        _context.Issues.Add(issue);
        
        await _context.SaveChangesAsync(token);

        return issue.Id;
    }

    public async Task<Issue> ShowIssueAsync(long id, CancellationToken token)
    {
        var issueEntity = await _context.Issues.FindAsync(id, token);
        
        ArgumentNullException.ThrowIfNull(issueEntity);
        
        return issueEntity;
    }

    public async Task<bool> UpdateIssueAsync(string readerCardNumber, string libraryCode , CancellationToken token)
    {
        return await _context.Issues
            .Where(b => b.ReaderCardNumber.Equals(readerCardNumber) &&  b.LibraryCode.Equals(libraryCode))
            .ExecuteUpdateAsync(i => i
                    .SetProperty(ies => ies.ActualReturnDate, DateOnly.FromDateTime(DateTime.Now)),
                token) > 0;
    }

    public async Task<bool> HasReaderDelay(string readerCardNumber, CancellationToken token)
    {
        return await _context.Issues
            .AnyAsync(b => b.ReaderCardNumber.Equals(readerCardNumber) && 
                                                   b.IssueDate < DateOnly.FromDateTime(DateTime.Now)
                                                   && b.ActualReturnDate == null, token);
    }

    public async Task<List<Issue>> SelectIssuesByPeriodAsync(
        DateOnly startPeriod,
        DateOnly endPeriod,
        CancellationToken token)
    {
        return await _context.Issues
            .Where(b => b.IssueDate >= startPeriod && b.IssueDate <= endPeriod)
            .OrderBy(b => b.IssueDate)
            .ToListAsync(token);
    }
    
    public async Task<List<Issue>> SelectReturnsByPeriodAsync(
        DateOnly startPeriod,
        DateOnly endPeriod,
        CancellationToken token)
    {
        return await _context.Issues
            .Where(b => b.ActualReturnDate != null 
                                                 && b.ActualReturnDate >= startPeriod 
                                                 && b.ActualReturnDate <= endPeriod)
            .OrderBy(b => b.IssueDate)
            .ToListAsync(token);
    }

    public async Task<List<string>> ShowBooksReader(string readerCardNumber, CancellationToken token)
    {
        return await _context.Issues
            .Where(b => b.ReaderCardNumber.Equals(readerCardNumber) && b.ActualReturnDate == null)
            .Include(b => b.Book)
            .OrderBy(b => b.IssueDate)
            .Select(b =>
                    string.Join(" ", new string[] 
                    {
                        $"Библиотечный шифр: {b.LibraryCode}",
                        $"Название: {b.Book.Title}",
                        $"Авторы: {string.Join(", ", b.Book.Authors)}"
                    }))
            .ToListAsync(token);
    }

    public async Task<List<Issue>> ShowAllIssuesAsync(CancellationToken token)
    {
        return await _context.Issues.ToListAsync(token);
    }
}