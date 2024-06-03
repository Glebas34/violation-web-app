﻿namespace ViolationWebApplication.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IList<T>> GetAll();
        Task Add(T entity);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
        Task ExplicitLoading(T entity,string property);
        Task ExplicitLoadingCollection(T entity,string property);
        Task ExplicitLoadingRange(IEnumerable<T> entities, string property);
    }
}
