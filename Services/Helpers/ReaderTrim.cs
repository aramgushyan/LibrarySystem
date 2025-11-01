using Applications.Dto.Reader;

namespace Services.Helpers;

public static class ReaderTrim
{
    public static void TrimAddReaderDto(AddReaderDto addReaderDto)
    {
        addReaderDto.Name = addReaderDto.Name.Trim();
        addReaderDto.Surname = addReaderDto.Surname.Trim();
        addReaderDto.Patronymic = addReaderDto.Patronymic?.Trim();
    }

    public static void TrimUpdateReaderDto(UpdateReaderDto updateReaderDto)
    {
        updateReaderDto.ReaderCardNumber = updateReaderDto.ReaderCardNumber.Trim();
        updateReaderDto.Name = updateReaderDto.Name?.Trim();
        updateReaderDto.Surname = updateReaderDto.Surname?.Trim();
        updateReaderDto.Patronymic = updateReaderDto.Patronymic?.Trim();
    }

}