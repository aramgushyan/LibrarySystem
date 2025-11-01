namespace Domain.Models;

public class Reader
{
    public string ReaderCardNumber { get; set; }
    
    public string Name { get; set; }
    
    public string Surname  { get; set; }
    
    public string? Patronymic { get; set; }
    
    public DateOnly IssueDateReaderCard { get; set; }

    public DateOnly ExpirationDateReaderCard { get; set; }
}