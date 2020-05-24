using App.Core;
using App.Core.DbTrackers;
using App.Entity.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using App.Data.Extensions;

namespace App.Data
{
    public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly InMemoryContext _context;

        private DbSet<TEntity> _entities;

        #endregion

        #region Ctor

        public EfRepository(InMemoryContext context)
        {
            if (_context == null)
                _context = context;
        }

        #endregion

        #region Properties

        public virtual IQueryable<TEntity> Table => Entities;

        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        #endregion



        #region Methods

        public virtual async Task<TEntity> GetById(object id)
        {
            return await Entities.FindAsync(id);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null,
            bool asNoTracking = false,
            params Expression<Func<TEntity, object>>[] relations)
        {
            var query = _context.Set<TEntity>() as IQueryable<TEntity>;

            if (asNoTracking)
                query = query.AsNoTracking();

            try
            {
                if (relations.Length > 0)
                {
                    query = relations.Aggregate(query, (current, property) => current.Include(property));
                    //for (int i = 0; i < relations.Length; i++)
                    //    query = query.Include(relations[i]);
                }

                return query.Where(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IQueryable<TEntity> Pagining(Expression<Func<TEntity, bool>> filter,
                                                   int skip,
                                                   int take,
                                                   string columnName,
                                                   OrderDirection direction,
                                                   bool asNoTracking = true,
                                                   params Expression<Func<TEntity, object>>[] relations)
        {
            var query = _context.Set<TEntity>() as IQueryable<TEntity>;
            if (asNoTracking)
                query = query.AsNoTracking();
            if (relations.Length > 0)
                query = relations.Aggregate(query, (current, property) => current.Include(property));
            query = query.Where(filter);
            query = query.OrderBy(columnName, direction);
            return query.Skip(skip).Take(take);
        }

        public virtual async Task Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await Entities.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual async Task Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                await Entities.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.UpdateRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        #endregion


        #region Utilities

        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }



        #endregion

    }
}
