namespace Applications.Dto.Reader;

public class UpdateReaderDto
{
    public string ReaderCardNumber  { get; set; }
    
    public string? Name { get; set; }
    
    public string? Surname { get; set; }
    
    public string? Patronymic { get; set; }
}