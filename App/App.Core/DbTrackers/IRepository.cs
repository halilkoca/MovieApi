using App.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Core.DbTrackers
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        Task<TEntity> GetById(object id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null,
            bool asNoTracking = false,
            params Expression<Func<TEntity, object>>[] relations);
        IQueryable<TEntity> Pagining(Expression<Func<TEntity, bool>> filter,
                                                   int skip,
                                                   int take,
                                                   string columnName,
                                                   OrderDirection direction,
                                                   bool asNoTracking = true,
                                                   params Expression<Func<TEntity, object>>[] relations);
        Task Insert(TEntity entity);
        Task Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);





    }
}
