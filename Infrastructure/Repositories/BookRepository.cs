using Domain.Models;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository:IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddBookAsync(Book book, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(book);
        
        _context.Books.Add(book);
        
        await _context.SaveChangesAsync(token);

        return book.LibraryCode;
    }

    public async Task<bool> DeleteBookAsync(string libraryCode, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(libraryCode);
        
        return await _context.Books.Where(b =>
                b.LibraryCode.Equals(libraryCode))
            .ExecuteDeleteAsync(token) > 0;
    }

    public async Task<bool> UpdateBookAsync(Book book, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(book);
        
        return await _context.Books.Where(b => b.LibraryCode.Equals(book.LibraryCode))
            .ExecuteUpdateAsync(b => b
                .SetProperty(bk => bk.PublisherName, book.PublisherName)
                .SetProperty(bk => bk.PublisherPlace, book.PublisherPlace)
                .SetProperty(bk => bk.PublisherYear, book.PublisherYear)
                .SetProperty(bk => bk.TotalNumberOfCopies, book.TotalNumberOfCopies)
                .SetProperty(bk => bk.AvailableNumberOfCopies, book.AvailableNumberOfCopies), token) > 0;
    }

    public async Task<List<string>> ShowAllBooksAsync(CancellationToken token)
    {
        return await _context.Books.Select(b =>
            string.Join(" ", new string[] 
            {
                $"Библиотечный шифр: {b.LibraryCode}",
                $"Название: {b.Title}",
                $"Авторы: {string.Join(", ", b.Authors)}"
            }))
            .ToListAsync(token);
    }

    public async Task<bool> IsUniqueBookAsync(string libraryCode, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(libraryCode);
        
        return ! await  _context.Books.AnyAsync(b => b.LibraryCode.Equals(libraryCode), token) ;
    }

    public async Task<Book> ShowBookByCodeAsync(string libraryCode, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(libraryCode);
        
        var book = await _context.Books.FindAsync(libraryCode, token);
        
        if(book == null)
            throw new KeyNotFoundException("Книги с таким библиотечным кодом нет");

        return book;
    }
    
    public async Task<Book> ShowBookByNameAsync(string title, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);
        
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Title.ToLower().Equals(title.ToLower()), token);
        
        if(book == null)
            throw new KeyNotFoundException($"Книга с названием '{title}' не найдена");

        return book;
    }

    public async Task<bool> IsStock(string libraryCode, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(libraryCode);
        
        return await _context.Books.AnyAsync(b => 
            b.LibraryCode.Equals(libraryCode) && b.AvailableNumberOfCopies > 0, token);
    }
}