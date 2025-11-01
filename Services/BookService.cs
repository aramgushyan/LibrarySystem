using System.Data;
using System.Text.RegularExpressions;
using Applications.Dto.Book;
using Applications.Exceptions;
using Domain.Models;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Services.Helpers;
using Services.Interfaces;

namespace Services;

public class BookService:IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<AddBookDto> _addBookDtoValidator;
    private readonly IValidator<UpdateBookDto> _updateBookDtoValidator;
    
    public BookService(
            IBookRepository bookRepository, 
            IValidator<AddBookDto> addBookDtoValidator,
            IValidator<UpdateBookDto> updateBookDtoValidator)
    {
        _bookRepository = bookRepository;
        _addBookDtoValidator = addBookDtoValidator;
        _updateBookDtoValidator = updateBookDtoValidator;
    }

    public async Task<string> AddBookAsync(AddBookDto addBookDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(addBookDto);

        BookTrim.TrimAddBookDto(addBookDto);
        
        if (!await _bookRepository.IsUniqueBookAsync(addBookDto.LibraryCode, token))
            throw new DuplicateNameException("Книга с таким шифром уже есть");
        
        WorkValidation.CheckResult(await _addBookDtoValidator.ValidateAsync(addBookDto, token));

        return await _bookRepository.AddBookAsync(new Book()
        {
            LibraryCode = addBookDto.LibraryCode,
            Authors = addBookDto.Authors,
            AvailableNumberOfCopies = addBookDto.TotalNumberOfCopies,
            PublisherYear = addBookDto.PublisherYear,
            PublisherName = addBookDto.PublisherName,
            PublisherPlace = addBookDto.PublisherPlace,
            TotalNumberOfCopies = addBookDto.TotalNumberOfCopies,
            Title = addBookDto.Title
        }, token);
    }

    public async Task<bool> RemoveBookAsync(string libraryCode, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(libraryCode);
        
        libraryCode = libraryCode.Trim();
        
        await _bookRepository.ShowBookByCodeAsync(libraryCode, token);
        
        WorkValidation.ValidateBook(libraryCode);
        
        return await _bookRepository.DeleteBookAsync(libraryCode, token);
    }

    public async Task<bool> UpdateBookAsync(UpdateBookDto updateBookDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(updateBookDto);
        
        BookTrim.TrimUpdateBookDto(updateBookDto);
        
        await _bookRepository.ShowBookByCodeAsync(updateBookDto.LibraryCode, token);
        
        WorkValidation.CheckResult(await _updateBookDtoValidator.ValidateAsync(updateBookDto, token));
        
        var book = await _bookRepository.ShowBookByCodeAsync(updateBookDto.LibraryCode, token);
        
        if(!string.IsNullOrEmpty(updateBookDto.PublisherName))
            book.PublisherName = updateBookDto.PublisherName;
        
        if(!string.IsNullOrEmpty(updateBookDto.PublisherPlace))
            book.PublisherPlace = updateBookDto.PublisherPlace;
        
        WorkCopies(book, updateBookDto);
        
        return await _bookRepository.UpdateBookAsync(book, token);
    }

    public async Task<Book> GetBookByCodeAsync(string libraryCode, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(libraryCode);
        
        libraryCode = libraryCode.Trim();
        
        WorkValidation.ValidateBook(libraryCode);
        
        return await _bookRepository.ShowBookByCodeAsync(libraryCode,token);
    }

    public async Task<Book> GetBookByNameAsync(string title, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(title);
        
        title = title.Trim();

        return await _bookRepository.ShowBookByNameAsync(title, token);
    }

    public async Task<List<string>> GetBooksAsync(CancellationToken token)
    {
        return await _bookRepository.ShowAllBooksAsync(token);
    }

    private void WorkCopies(Book book, UpdateBookDto updateBookDto)
    {
        if(updateBookDto.PublisherYear != null)
            book.PublisherYear = updateBookDto.PublisherYear.Value;

        int issuedCopies = book.TotalNumberOfCopies - book.AvailableNumberOfCopies;
        
        if (updateBookDto.TotalNumberOfCopies.HasValue)
        {
            int newTotal = updateBookDto.TotalNumberOfCopies.Value;

            if (newTotal < issuedCopies)
                throw new MyValidationException(
                    $"Нельзя уменьшить общее количество экземпляров меньше, чем уже выдано ({issuedCopies})");

            book.TotalNumberOfCopies = newTotal;
            
            book.AvailableNumberOfCopies = Math.Max(0, newTotal - issuedCopies);
        }
    }
}