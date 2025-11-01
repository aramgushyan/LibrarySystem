using Applications.Dto.Reader;
using Domain.Models;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Services.Helpers;
using Services.Interfaces;

namespace Services;

public class ReaderService:IReaderService
{
    private readonly IReaderRepository _readerRepository;
    
    private readonly IValidator<AddReaderDto> _addReaderValidator;
    
    private readonly IValidator<UpdateReaderDto> _updateReaderValidator;

    public ReaderService(
        IReaderRepository readerRepository,
        IValidator<AddReaderDto> addReaderValidator,
        IValidator<UpdateReaderDto> updateReaderValidator)
    {
        _readerRepository = readerRepository;
        _addReaderValidator = addReaderValidator;
        _updateReaderValidator = updateReaderValidator;
    }

    public async Task<string> AddReaderAsync(AddReaderDto readerDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(readerDto);
        
        ReaderTrim.TrimAddReaderDto(readerDto);
        
        WorkValidation.CheckResult(await _addReaderValidator.ValidateAsync(readerDto, token));
        
        return await _readerRepository.AddReaderAsync(new Reader()
        {
            Name = readerDto.Name,
            Surname = readerDto.Surname,
            Patronymic = readerDto.Patronymic,
            IssueDateReaderCard = DateOnly.FromDateTime(DateTime.Now),
            ExpirationDateReaderCard = DateOnly.FromDateTime(DateTime.Now.AddYears(2))
        }, token);
    }

    public async Task<bool> RemoveReaderAsync(string readerCardNumber, CancellationToken token)
    {
        readerCardNumber = readerCardNumber.Trim();
        
        WorkValidation.ValidateReadCardNumber(readerCardNumber);
        
        return await _readerRepository.DeleteReaderAsync(readerCardNumber, token);
    }

    public async Task<bool> UpdateReaderAsync(UpdateReaderDto readerDto, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(readerDto);
        
        ReaderTrim.TrimUpdateReaderDto(readerDto);
        
        WorkValidation.CheckResult(await _updateReaderValidator.ValidateAsync(readerDto, token));
        
        var reader = await _readerRepository.GetReaderAsync(readerDto.ReaderCardNumber, token);
        
        if(!string.IsNullOrEmpty(readerDto.Name))
            reader.Name = readerDto.Name;
        
        if(!string.IsNullOrEmpty(readerDto.Surname))
            reader.Surname = readerDto.Surname;
        
        if(!string.IsNullOrEmpty(readerDto.Patronymic))
            reader.Patronymic = readerDto.Patronymic;

        return await  _readerRepository.UpdateReaderAsync(reader, token);
    }
    
    public async Task<Reader> GetReaderAsync(string readerCardNumber, CancellationToken token)
    {
        readerCardNumber = readerCardNumber.Trim();
        
        WorkValidation.ValidateReadCardNumber(readerCardNumber);
        
        return await _readerRepository.GetReaderAsync(readerCardNumber, token);
    }

    public async Task<List<string>> GetReadersAsync(CancellationToken token)
    {
        return await _readerRepository.GetAllReadersAsync(token);
    }
    

}