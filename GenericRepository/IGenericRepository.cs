using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositoryDemo.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T t);
        void Remove(T t);
        void Update(T t);
        void Delete (T t);
        List<T> GetAll();
        List<T> GetBy(Expression<Func<T,bool>> predicate);
        T GetSingle(Expression<Func<T,bool>> predicate);
        bool CheckIfExist (Expression<Func<T,bool>> predicate);
    }
}