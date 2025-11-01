using Applications.Dto.Issue;
using Domain.Models;
using FluentValidation;

namespace Applications.Validators.IssueValidators;

public class IssueValidator:AbstractValidator<Issue>
{
    public IssueValidator()
    {
        RuleFor(i => i.LibraryCode)
            .NotEmpty()
            .WithMessage("Шифр обязательное поле для заполнения")
            .Matches(@"^[A-Z]{2}-\d{3}$")
            .WithMessage("Шифр не соответствует формату \"AA-111\"");

        RuleFor(i => i.ReaderCardNumber)
            .NotEmpty()
            .WithMessage("Номер обязательное поле для заполнения")
            .Matches(@"^\d{6}$")
            .WithMessage("Номер не соответствует формату \"000000\"");

        RuleFor(i => i.ExpectedReturnDate)
            .NotEmpty()
            .WithMessage("Срок сдачи обязательное поле для заполнения");
        
        RuleFor(i => i)
            .Must(i => i.IssueDate < i.ExpectedReturnDate)
            .WithMessage("Дата выдачи не может быть позже даты возврата");
    }
}