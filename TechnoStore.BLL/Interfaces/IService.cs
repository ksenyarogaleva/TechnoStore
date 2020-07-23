using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TechnoStore.BLL.Interfaces
{
    public interface IService<TEntity,TDto> where TEntity:class
    {
        bool Exists(TEntity entity);
        IEnumerable<TDto> GetAll();
        TDto GetSingle(int id);
        IEnumerable<TDto> Find(Expression<Func<TEntity, bool>> predicate);

    }
}
