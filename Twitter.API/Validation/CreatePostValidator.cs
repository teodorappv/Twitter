﻿using FluentValidation;
using Twitter.Core.Contracts.V1.Request;

namespace Twitter.API.Validation
{
    public class CreatePostValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Text).NotEmpty().Length(1, 400).WithMessage("Text must be up to 400 characters!");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("You must add a category!");
        }
    }
}
