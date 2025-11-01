using System.Text.RegularExpressions;
using FluentValidation;
using Applications.Dto.Reader;

namespace Applications.Validators.ReaderValidators;

public class ReaderValidator:AbstractValidator<AddReaderDto>
{
    public ReaderValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Имя обязательно поле")
            .Matches(@"^[А-Яа-яЁё]+$")
            .WithMessage("Имя должно содержать только русские буквы");
        
        RuleFor(r => r.Surname)
            .NotEmpty()
            .WithMessage("Фамилия обязательно поле")
            .Matches(@"^[А-Яа-яЁё]+$")
            .WithMessage("Фамилия должно содержать только русские буквы");
        
        RuleFor(r => r.Patronymic)
            .Matches(@"^[А-Яа-яЁё]+$")
            .WithMessage("Отчество должно содержать только русские буквы");
    }
}