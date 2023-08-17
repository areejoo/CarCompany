using FluentValidation;

namespace web.api.Dtos.Incomming
{
    public class CreateDtoValidator: AbstractValidator<CreateCarDto>
    {
        public CreateDtoValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty();
                // RuleFor(x => x.EngineCapacity)
                // .NotNull();

        }
    }
}
