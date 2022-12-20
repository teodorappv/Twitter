using Twitter.Core.Interfaces;

namespace Twitter.Core.Entities;

public partial class Category: IEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }
}
