using Applications.Dto;
using FluentValidation;

namespace Applications.Validators;

public class PeriodValidator:AbstractValidator<PeriodDto>
{
    public PeriodValidator()
    {
        RuleFor(periodDto => periodDto.StartDate)
            .NotEmpty()
            .WithMessage("Начало периода обязательное поле");
        
        RuleFor(periodDto => periodDto.EndDate)
            .NotEmpty()
            .WithMessage("Конец периода обязательное поле");
        
        RuleFor(period => period)
            .Must(p => p.StartDate <= p.EndDate)
            .WithMessage("Начало периода должно быть меньше или равно концу периода");
    }
}