namespace Twitter.API.Exceptions
{
    public class ValidationRequestException : Exception
    {
        public ValidationRequestException(string? message) : base(message)
        {
        }
    }
}
