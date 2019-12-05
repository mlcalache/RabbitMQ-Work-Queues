using RabbitMQ_Work_Queues.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace RabbitMQ_Work_Queues.Data.SqlServer.EF.Contexts
{
    public class QueueContext : DbContext
    {
        public QueueContext()
            : base("name=QueueContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<QueueContext>(null);

#if DEBUG
            Database.Log = s => System.Diagnostics.Trace.WriteLine(s);
#endif
        }

        public virtual DbSet<Log_Portal_Generic> Log_Portal_Generic { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Types().Configure(delegate (ConventionTypeConfiguration p)
            {
                if (p.ClrType.GetProperty("ValidationResult") != null)
                {
                    p.Ignore("ValidationResult");
                }
            });

            var typesMapping = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var mapping in typesMapping)
            {
                dynamic configurationInstance = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public void IgnoreChanges(DbEntityEntry entry, string[] properties)
        {
            foreach (string prop in properties)
            {
                entry.Property(prop).IsModified = false;
                entry.Property(prop).CurrentValue = entry.GetDatabaseValues().GetValue<object>(prop);
            }
        }
    }
}