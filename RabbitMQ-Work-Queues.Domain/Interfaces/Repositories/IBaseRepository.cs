using RabbitMQ_Work_Queues.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RabbitMQ_Work_Queues.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TKey> : IDisposable where TEntity : BaseEntity<TKey>
    {
        void ChangeConnection(string connectionString);

        void ChangeDatabase(string database);

        void Create(TEntity entity);

        void CreateRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true);

        TEntity Get(TKey id, bool @readonly = true);

        IQueryable<TEntity> GetAll(bool @readonly = true);

        void Remove(TKey id);

        void RemoveRange(IEnumerable<TEntity> entities);

        int SaveChanges();

        void SetConnetionTimeout(int timeout);

        void Update(TEntity entity, params string[] ignoreProperties);
    }

    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        void ChangeConnection(string connectionString);

        void ChangeDatabase(string database);

        void Create(TEntity entity);

        void CreateRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true);

        IQueryable<TEntity> GetAll(bool @readonly = true);

        void RemoveRange(IEnumerable<TEntity> entities);

        //void Remove(TKey id);
        int SaveChanges();

        void SetConnetionTimeout(int timeout);

        //TEntity Get(TKey id, bool @readonly = true);
        void Update(TEntity entity, params string[] ignoreProperties);
    }
}