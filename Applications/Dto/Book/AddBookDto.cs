namespace Applications.Dto.Book;

public class AddBookDto
{
    public string LibraryCode { get; set; }
    
    public string Title { get; set; }
    
    public List<string> Authors { get; set; }
    
    public int PublisherYear { get; set; }
    
    public string PublisherName { get; set; }
    
    public string PublisherPlace{ get; set; }
    
    public int TotalNumberOfCopies  { get; set; }
}