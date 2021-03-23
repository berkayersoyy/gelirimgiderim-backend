using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess.Abstract;


namespace Core.DataAccess
{
    public interface IEntityRepository<T>
    where T: class,IEntity,new()
    {
        T Get(T entity);
        List<T> GetAll(Expression<Func<bool, T>> expression = null);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}