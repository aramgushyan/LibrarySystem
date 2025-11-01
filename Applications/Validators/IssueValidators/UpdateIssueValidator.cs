using Applications.Dto.Issue;
using FluentValidation;

namespace Applications.Validators.IssueValidators;

public class UpdateIssueValidator:AbstractValidator<UpdateIssueDto>
{
    public UpdateIssueValidator()
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
    }
}