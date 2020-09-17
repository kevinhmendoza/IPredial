using Core.Entities.Base;
using Core.UseCase.Base;
using Infrastructure.Audit;
using Infrastructure.Audit.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Data.Base
{
    public abstract class GenericRepositoryAuditLog<T> : IGenericRepositoryQuery<T>
        where T : AuditLog
    {
        protected IDbContextAudit _db;
        protected readonly DbSet<T> _dbset;
        

        protected GenericRepositoryAuditLog(IDbContextAudit context)
        {
            _db = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.AsEnumerable<T>();
        }

        public virtual T Find(object id)
        {
            return _dbset.Find(id);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        protected IQueryable<T> FindByAsQueryable(Expression<Func<T, bool>> predicate)
        {
            return  _dbset.Where(predicate);
        }

        protected IQueryable<T> AsQueryable()
        {
            return _dbset.AsQueryable();
        }

        public virtual IEnumerable<T> FindBy(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<T> query = _dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T FindFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            T query = _dbset.Where(predicate).FirstOrDefault();
            return query;
        }

    }
}