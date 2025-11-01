using System.Data;
using Domain.Models;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReaderRepository:IReaderRepository
{
    private readonly LibraryDbContext _libraryDbContext;
    private Random _random = new Random();

    public ReaderRepository(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    public async Task<string> AddReaderAsync(Reader reader, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(reader);
        
        reader.ReaderCardNumber = await GenerateCardNumber(token);
        
        _libraryDbContext.Readers.Add(reader);
        await _libraryDbContext.SaveChangesAsync(token);

        return reader.ReaderCardNumber;
    }

    public async Task<bool> DeleteReaderAsync(string readerCardNumber, CancellationToken token)
    {
        ArgumentException.ThrowIfNullOrEmpty(readerCardNumber);
        
        await GetReaderAsync(readerCardNumber, token);
        
        int count = await _libraryDbContext.Readers
           .Where(r => 
               r.ReaderCardNumber.Equals(readerCardNumber))
           .ExecuteDeleteAsync(token);
       
       return count > 0;
    }

    public async Task<bool> UpdateReaderAsync(Reader reader, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(reader);

        await GetReaderAsync(reader.ReaderCardNumber, token);
            
        int count = await _libraryDbContext.Readers
            .Where(r =>
                r.ReaderCardNumber.Equals(reader.ReaderCardNumber))
            .ExecuteUpdateAsync(r => r
                .SetProperty(x => x.Name, n => reader.Name)
                .SetProperty(x => x.Surname, s => reader.Surname)
                .SetProperty(x => x.Patronymic,p => reader.Patronymic), token);
        
        return count > 0;
    }

    public Task<List<string>> GetAllReadersAsync(CancellationToken token)
    {
        return _libraryDbContext.Readers
            .Select(r =>
                string.Join(" ",new List<string?>(){ r.Surname, r.Name, r.Patronymic, r.ReaderCardNumber}))
            .ToListAsync(token);
    }

    public async Task<Reader> GetReaderAsync(string readerCardNumber, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(readerCardNumber);
        
        var reader = await _libraryDbContext.Readers.Where(r => 
                r.ReaderCardNumber.Equals(readerCardNumber))
            .FirstOrDefaultAsync(token);

        if (reader == null)
            throw new KeyNotFoundException("Читателя с таким номером билета нет");

        return reader;
    }

    private async Task<bool> ReaderExists(string readerCardNumber,CancellationToken token)
    {
        return await _libraryDbContext.Readers.AnyAsync(r =>
            r.ReaderCardNumber.Equals(readerCardNumber), token);
    }

    private async Task<string> GenerateCardNumber(CancellationToken token)
    {
        for(int i = 0; i < 200; i++)
        {
            var number = _random.Next(100000,999999).ToString();
            if (! await ReaderExists(number, token))
                return number;
        }   
        
        throw new IndexOutOfRangeException("За 200 попыток не удалось сгенерировать уникальный номер");
    }
}
