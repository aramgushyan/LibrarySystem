using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IBookRepository
{
    public Task<string> AddBookAsync(Book book, CancellationToken token);
    
    public Task<bool> DeleteBookAsync(string libraryCode, CancellationToken token);
    
    public Task<bool> UpdateBookAsync(Book book, CancellationToken token);
    
    public Task<List<string>> ShowAllBooksAsync(CancellationToken token);
    
    public Task<bool> IsUniqueBookAsync(string libraryCode, CancellationToken token);
    
    public Task<Book> ShowBookByCodeAsync(string libraryCode, CancellationToken token);
    
    
    public Task<Book> ShowBookByNameAsync(string title, CancellationToken token);

    public Task<bool> IsStock(string libraryCode, CancellationToken token);
}