using FluentValidation.TestHelper;
using Twitter.API.Validation;
using Twitter.Core.Contracts.V1.Request;

namespace Twitter.UnitTests
{
    public class CreatePostValidatorTests
    {
        private readonly CreatePostValidator _validator;

        public CreatePostValidatorTests()
        {
            _validator = new CreatePostValidator();
        }

        [Fact]
        public void Given_CreatePost_When_Text_IsEmpty_Then_CreatePost_Fails()
        {
            CreatePostRequest createPostRequest = new CreatePostRequest()
            {
                Text = "",
                CategoryId = 1
            };

            var response = _validator.TestValidate(createPostRequest);

            response.ShouldHaveValidationErrorFor(x => x.Text).Only();
            response.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void Given_CreatePost_When_Text_IsNull_Then_CreatePost_Fails()
        {
            CreatePostRequest createPostRequest = new CreatePostRequest()
            {
                Text = null,
                CategoryId = 1
            };

            var response = _validator.TestValidate(createPostRequest);

            response.ShouldHaveValidationErrorFor(x => x.Text).Only();
            response.ShouldNotHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void Given_CreatePost_When_CategoryId_IsNull_Then_CreatePost_Fails()
        {
            CreatePostRequest createPostRequest = new CreatePostRequest()
            {
                Text = "lorem ipsum",
                CategoryId = 0
            };

            var response = _validator.TestValidate(createPostRequest);

            response.ShouldNotHaveValidationErrorFor(x => x.Text);
            response.ShouldHaveValidationErrorFor(x => x.CategoryId).Only();
        }
    }
}