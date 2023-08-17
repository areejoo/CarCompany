using FluentValidation;

namespace web.api.Dtos.Incomming
{
    public class UpdateDtoValidator: AbstractValidator<UpdateCarDto>
    {
        public UpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

        }
    }
}
