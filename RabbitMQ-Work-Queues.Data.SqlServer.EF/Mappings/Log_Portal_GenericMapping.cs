using RabbitMQ_Work_Queues.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RabbitMQ_Work_Queues.Data.SqlServer.EF.Mappings
{
    public class Log_Portal_GenericMapping : EntityTypeConfiguration<Log_Portal_Generic>
    {
        public Log_Portal_GenericMapping()
        {
            ToTable(nameof(Log_Portal_Generic), "api");

            HasKey(c => c.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName(nameof(Log_Portal_Generic.Id));

            Property(p => p.ThreadId)
                .HasColumnName(nameof(Log_Portal_Generic.ThreadId));

            Property(p => p.Message)
                .HasColumnName(nameof(Log_Portal_Generic.Message));

            Property(p => p.Message)
                .HasMaxLength(200)
                .HasColumnName(nameof(Log_Portal_Generic.Message));

            Property(p => p.Type)
                .HasColumnName(nameof(Log_Portal_Generic.Type));

            Property(p => p.RegistrationDateTime)
                .HasColumnName(nameof(Log_Portal_Generic.RegistrationDateTime));

            Property(p => p.ClientIP)
                .HasMaxLength(30)
                .HasColumnName(nameof(Log_Portal_Generic.ClientIP));

            Property(p => p.Exception)
                .HasColumnName(nameof(Log_Portal_Generic.Exception));

            Property(p => p.UserId)
                .HasColumnName(nameof(Log_Portal_Generic.UserId));
        }
    }
}