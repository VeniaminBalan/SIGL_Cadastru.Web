namespace Domain.Models.Entities;

public abstract class Entity : IEntity
{
    public string Id { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime ModifiedUtc { get; set; }

    public Entity(string id) 
    {
        Id = id;
        CreatedUtc = DateTime.UtcNow;
        ModifiedUtc = DateTime.UtcNow;
    }
}
