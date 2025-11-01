using Applications.Dto.Reader;
using FluentValidation;

namespace Applications.Validators.ReaderValidators;

public class UpdateReaderValidator:AbstractValidator<UpdateReaderDto>
{
    public UpdateReaderValidator()
    {
        RuleFor(r => r.ReaderCardNumber)
            .Matches(@"^\d{6}$")
            .WithMessage("Номер билета не соответствует формату \"000000\"");
        
        RuleFor(r => r.Name)
            .Matches(@"^[А-Яа-яЁё]+$")
            .WithMessage("Имя должно содержать только русские буквы");
        
        RuleFor(r => r.Surname)
            .Matches(@"^[А-Яа-яЁё]+$")
            .WithMessage("Фамилия должно содержать только русские буквы");
        
        RuleFor(r => r.Patronymic)
            .Matches(@"^[А-Яа-яЁё]+$")
            .WithMessage("Отчество должно содержать только русские буквы");
    }
}