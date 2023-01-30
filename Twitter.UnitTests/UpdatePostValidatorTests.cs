using FluentValidation.TestHelper;
using Twitter.API.Validation;
using Twitter.Core.Domain.DTOs.Requests;

namespace Twitter.UnitTests
{
    public class UpdatePostValidatorTests
    {
        private readonly UpdatePostValidator _validator;

        public UpdatePostValidatorTests()
        {
            _validator = new UpdatePostValidator();
        }

        [Fact]
        public void Given_UpdatePost_When_TextIsEmpty_Then_UpdatePost_Fails()
        {
            UpdatePostRequest updatePostRequest = new UpdatePostRequest()
            {
                Id = 1,
                Text = "",
                CategoryId = 1
            };

            var response = _validator.TestValidate(updatePostRequest);

            response.ShouldHaveValidationErrorFor(x => x.Text).Only();
            response.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void Given_UpdatePost_When_TextIsNull_Then_UpdatePost_Fails()
        {
            UpdatePostRequest updatePostRequest = new UpdatePostRequest()
            {
                Id = 1,
                Text = null,
                CategoryId = 1
            };

            var response = _validator.TestValidate(updatePostRequest);

            response.ShouldHaveValidationErrorFor(x => x.Text).Only();
            response.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void Given_UpdatePost_When_CategoryIdIsNull_Then_UpdatePost_Fails()
        {
            UpdatePostRequest updatePostRequest = new UpdatePostRequest()
            {
                Id = 1,
                Text = "lorem ipsum",
                CategoryId = 0
            };

            var response = _validator.TestValidate(updatePostRequest);

            response.ShouldNotHaveValidationErrorFor(x => x.Text);
            response.ShouldHaveValidationErrorFor(x => x.CategoryId).Only();
        }

        [Fact]
        public void Given_UpdatePost_When_ParametersAreValid_Then_UpdatePost_Succeeds()
        {
            UpdatePostRequest updatePostRequest = new UpdatePostRequest()
            {
                Id = 1,
                Text = "lorem ipsum",
                CategoryId = 1
            };

            var response = _validator.TestValidate(updatePostRequest);

            response.ShouldNotHaveValidationErrorFor(x => x.Text);
            response.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
        }
    }
}
