using Applications.Dto.Book;
using FluentValidation;

namespace Applications.Validators.BookValidators;

public class UpdateBookValidator:AbstractValidator<UpdateBookDto>
{
    public UpdateBookValidator()
    {
        RuleFor(b => b.LibraryCode)
            .NotEmpty()
            .WithMessage("Шифр обязательное поле для заполнения")
            .Matches(@"^[A-Z]{2}-\d{3}$")
            .WithMessage("Шифр не соответствует формату \"AA-111\"");
        
        RuleFor(b => b.PublisherYear)
            .Must(b => b >= 0 && b <= DateTime.Now.Year)
            .WithMessage("Дата издания не может быть меньше 0 и быть больше текущего года");

        RuleFor(b => b.TotalNumberOfCopies)
            .Must(b => b >= 0)
            .WithMessage("Количество экземпляров не может быть меньше 0");
    }
}