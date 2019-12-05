namespace RabbitMQ_Work_Queues.Domain.Entities
{
    public abstract class BaseEntity
    {
    }

    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}