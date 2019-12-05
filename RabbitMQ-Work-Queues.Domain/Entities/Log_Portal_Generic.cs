using System;

namespace RabbitMQ_Work_Queues.Domain.Entities
{
    public class Log_Portal_Generic : BaseEntity<int>
    {
        public Guid ThreadId { get; set; }

        public string Message { get; set; }

        public string Sender { get; set; }

        public int? Type { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public string ClientIP { get; set; }

        public string Exception { get; set; }

        public int? UserId { get; set; }
    }
}