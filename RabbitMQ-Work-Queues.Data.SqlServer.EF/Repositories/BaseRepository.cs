using RabbitMQ_Work_Queues.Data.SqlServer.EF.Contexts;
using RabbitMQ_Work_Queues.Domain.Entities;
using RabbitMQ_Work_Queues.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace RabbitMQ_Work_Queues.Data.SqlServer.EF.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : BaseRepository, IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>, new()
    {
        #region Protected vars

        protected readonly DbSet<TEntity> DbSet;

        #endregion Protected vars

        #region Ctors

        protected BaseRepository()
        {
            DbSet = _context.Set<TEntity>();
        }

        #endregion Ctors

        #region Public CRUD methods

        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            return @readonly
                ? DbSet.AsNoTracking()
                    .Where(predicate)
                : DbSet
                    .Where(predicate);
        }

        public virtual TEntity Get(TKey id, bool @readonly = true)
        {
            return @readonly
                ? DbSet.AsNoTracking()
                    .SingleOrDefault(p => p.Id.ToString() == id.ToString())
                : DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll(bool @readonly = true)
        {
            return @readonly
                ? DbSet.AsNoTracking()
                : DbSet;
        }

        public virtual void Remove(TKey id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity, params string[] ignoreProperties)
        {
            var entry = _context.Entry(entity);

            entry.State = EntityState.Modified;

            _context.IgnoreChanges(entry, ignoreProperties);
        }

        #endregion Public CRUD methods
    }

    public abstract class BaseRepository<TEntity> : BaseRepository, IBaseRepository<TEntity>
       where TEntity : BaseEntity, new()
    {
        #region Protected vars

        protected readonly DbSet<TEntity> DbSet;

        #endregion Protected vars

        #region Ctors

        protected BaseRepository()
        {
            DbSet = _context.Set<TEntity>();
        }

        #endregion Ctors

        #region Public CRUD methods

        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = true)
        {
            return @readonly
                ? DbSet.AsNoTracking()
                    .Where(predicate)
                : DbSet
                    .Where(predicate);
        }

        public virtual IQueryable<TEntity> GetAll(bool @readonly = true)
        {
            return @readonly
                ? DbSet.AsNoTracking()
                : DbSet;
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity, params string[] ignoreProperties)
        {
            var entry = _context.Entry(entity);

            entry.State = EntityState.Modified;

            _context.IgnoreChanges(entry, ignoreProperties);
        }

        #endregion Public CRUD methods
    }

    public abstract class BaseRepository
    {
        #region Protected vars

        protected QueueContext _context;
        protected bool _disposed;

        #endregion Protected vars

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }

        #endregion Dispose

        #region Ctors

        protected BaseRepository()
        {
            _context = new Contexts.QueueContext();
        }

        #endregion Ctors

        #region Public methods

        public void ChangeConnection(string database)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(_context.Database.Connection.ConnectionString);

            if (connectionStringBuilder.InitialCatalog != database)
            {
                connectionStringBuilder.InitialCatalog = database;
                _context.ChangeConnection(connectionStringBuilder.ToString());
            }
        }

        public void ChangeDatabase(string database)
        {
            _context.ChangeDatabase(database);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void SetConnetionTimeout(int timeout)
        {
            ((IObjectContextAdapter)_context).ObjectContext.CommandTimeout = timeout;
        }

        #endregion Public methods
    }
}