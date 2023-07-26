using FluentValidation;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarWorkshopServiceCommandValidator : AbstractValidator<CreateCarWorkshopServiceCommand>
    {
        public CreateCarWorkshopServiceCommandValidator()
        {
            RuleFor(s => s.Cost).NotNull().NotEmpty();
            RuleFor(s => s.Description).NotNull().NotEmpty();
            RuleFor(s => s.CarWorkshopEncodedName).NotNull().NotEmpty();
        }
    }
}
