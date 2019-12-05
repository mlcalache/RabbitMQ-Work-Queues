using RabbitMQ_Work_Queues.Domain.Entities;
using RabbitMQ_Work_Queues.Domain.Interfaces.Repositories;

namespace RabbitMQ_Work_Queues.Data.SqlServer.EF.Repositories
{
    public class Log_Portal_GenericRepository : BaseRepository<Log_Portal_Generic, int>, ILog_Portal_GenericRepository
    {
    }
}