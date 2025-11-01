namespace Applications.Dto.Issue;

public class ShowIssueDto
{
    public long Id { get; set; }

    public string LibraryCode { get; set; }
    
    public string ReaderCardNumber  { get; set; }
    
    public DateOnly IssueDate { get; set; }
    
    public DateOnly ExpectedReturnDate { get; set; }
    
    public DateOnly? ActualReturnDate { get; set; }
}