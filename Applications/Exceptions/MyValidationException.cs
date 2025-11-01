namespace Applications.Exceptions;

public class MyValidationException:Exception
{
    public MyValidationException(string message) : base(message)
    {
        
    }
}