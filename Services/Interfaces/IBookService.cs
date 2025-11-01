using Applications.Dto.Book;
using Applications.Dto.Reader;
using Domain.Models;


namespace Services.Interfaces;

public interface IBookService
{
    public Task<string> AddBookAsync(AddBookDto addBookDto, CancellationToken token);
    
    public Task<bool> RemoveBookAsync(string libraryCode, CancellationToken token);
    
    public Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto, CancellationToken token);
    
    public Task<Book> GetBookByCodeAsync(string libraryCode, CancellationToken token);

    public Task<Book> GetBookByNameAsync(string title, CancellationToken token);
    
    public Task<List<string>> GetBooksAsync(CancellationToken token);
}