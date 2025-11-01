namespace Applications.Dto.Issue;

public class AddIssueDto
{
    public string LibraryCode { get; set; }
    
    public string ReaderCardNumber  { get; set; }
    
    public DateOnly ExpectedReturnDate { get; set; }
    
}