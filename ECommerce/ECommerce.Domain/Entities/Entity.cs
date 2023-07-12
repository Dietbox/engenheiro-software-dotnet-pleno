namespace ECommerce.Domain.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            LastUpdate = DateTime.Now;
        }
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime LastUpdate { get; protected set; }
    }
}
