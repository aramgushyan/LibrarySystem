using Applications.Dto.Book;

namespace Services.Helpers;

public static class BookTrim
{
    public static void TrimAddBookDto(AddBookDto dto)
    {
        dto.LibraryCode = dto.LibraryCode.Trim();
        dto.Title = dto.Title.Trim();
        dto.Authors = dto.Authors.Select(s => s.Trim()).ToList();
        dto.PublisherName = dto.PublisherName.Trim();
        dto.PublisherPlace = dto.PublisherPlace.Trim();
    }
    
    public static void TrimUpdateBookDto(UpdateBookDto dto)
    {
        dto.LibraryCode = dto.LibraryCode.Trim();
        dto.PublisherName = dto.PublisherName.Trim();
        dto.PublisherPlace = dto.PublisherPlace.Trim();
    }
}