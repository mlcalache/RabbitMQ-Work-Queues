using System.Data.Entity;

namespace RabbitMQ_Work_Queues.Data.SqlServer.EF.Contexts
{
    public static class DbContextExtension
    {
        public static void ChangeConnection(this DbContext context, string connectionString)
        {
            context.Database.Connection.ConnectionString = connectionString;
        }

        public static void ChangeDatabase(this DbContext context, string database)
        {
            context.Database.Connection.Open();
            context.Database.Connection.ChangeDatabase(database);
        }
    }
}