namespace Applications.Dto.Book;

public class UpdateBookDto
{
    public string LibraryCode { get; set; }
    
    public int? PublisherYear { get; set; }
    
    public string? PublisherName { get; set; }
    
    public string? PublisherPlace{ get; set; }
    
    public int? TotalNumberOfCopies  { get; set; }
}