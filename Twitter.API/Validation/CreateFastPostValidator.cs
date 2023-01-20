using FluentValidation;
using Twitter.API.Commands;

namespace Twitter.API.Validation
{
    public class CreateFastPostValidator : AbstractValidator<CreateFastPostCommand>
    {
        public CreateFastPostValidator()
        {
            RuleFor(x => x.Text).NotEmpty().Length(1, 200).WithMessage("Text must be up to 200 characters.");      
        }
    }
}
