using System.Text.RegularExpressions;
using Applications.Exceptions;
using FluentValidation.Results;

namespace Services.Helpers;

public class WorkValidation
{
    private static readonly Regex _bookRegex = new Regex(@"^[A-Z]{2}-\d{3}$");
    private static readonly Regex _readerRegex = new Regex(@"^\d{6}$");
    public static void CheckResult(ValidationResult result)
    {
        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
            throw new MyValidationException(errors);
        }
    }

    public static void ValidateBook(string libraryCode)
    {
        if(!_bookRegex.IsMatch(libraryCode))
            throw new ArgumentException("Шифр книги не соответствует формату \"AA-111.\"");
            
    }

    public static void ValidateReadCardNumber(string readerCardNumber)
    {
        if (!_readerRegex.IsMatch(readerCardNumber))
            throw new MyValidationException("Номер билета не соответствует формату \"000000\"");
        
    }

}