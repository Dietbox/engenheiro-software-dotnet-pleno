using Dietbox.ECommerce.ORM.Entities.Users;
using Dietbox.ECommerce.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Contexts
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IIdentity
    {

        private readonly DbContext _dbContext;

        public Repository(BasicContext context)
        {
            _dbContext = context;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            DbSet<TEntity> context = _dbContext.Set<TEntity>();
            IQueryable<TEntity> result = context.Where(expression);
            return result;
        }

        public Task Insert(TEntity entity, CancellationToken cancellationToken = default)
        {
            Type[] interfaces = entity.GetType().GetInterfaces();
            if (interfaces.Any((Type x) => x.Equals(typeof(ICreatedDate)))) { (entity as ICreatedDate).CreatedDate = DateTime.Now; }
            _dbContext.Add<TEntity>(entity);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Update<TEntity>(entity);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove<TEntity>(entity);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
