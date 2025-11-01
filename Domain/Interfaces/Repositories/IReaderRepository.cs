using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IReaderRepository
{
    public Task<string> AddReaderAsync(Reader reader, CancellationToken token);
    public Task<bool> DeleteReaderAsync(string readerCardNumber, CancellationToken token);
    public Task<bool> UpdateReaderAsync(Reader reader, CancellationToken token);
    public Task<List<string>> GetAllReadersAsync(CancellationToken token);
    public Task<Reader> GetReaderAsync(string readerCardNumber, CancellationToken token);
    
}