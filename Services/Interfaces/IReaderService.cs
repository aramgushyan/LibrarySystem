using Domain.Models;
using Applications.Dto.Reader;

namespace Services.Interfaces;

public interface IReaderService
{
    public Task<string> AddReaderAsync(AddReaderDto readerDto, CancellationToken token);
    
    public Task<bool> RemoveReaderAsync(string readerCardNumber, CancellationToken token);
    
    public Task<bool> UpdateReaderAsync(UpdateReaderDto readerDto, CancellationToken token);
    
    public Task<Reader> GetReaderAsync(string readerCardNumber, CancellationToken token);
    
    public Task<List<string>> GetReadersAsync(CancellationToken token);
}