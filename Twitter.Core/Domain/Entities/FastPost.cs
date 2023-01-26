namespace Twitter.Core.Domain.Entities
{
    public class FastPost
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime Created { get; set; }
        public int CategoryId { get; set; }
        public string CreatedById { get; set; }
    }
}
