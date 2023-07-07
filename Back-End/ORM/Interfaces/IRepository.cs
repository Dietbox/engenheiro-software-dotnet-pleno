using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.ORM.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IIdentity
    {  

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

        Task Insert(TEntity entity, CancellationToken cancellationToken = default);

        Task Update(TEntity entity, CancellationToken cancellationToken = default);

        Task Delete(TEntity entity, CancellationToken cancellationToken = default);

    }
}
