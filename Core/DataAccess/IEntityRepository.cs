
using System.Collections.Generic;
using Core.Entities.Abstract;


namespace Core.DataAccess
{
    public interface IEntityRepository<T>
    where T: class,IEntity,new()
    {
        T Get(T entity);
        List<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}