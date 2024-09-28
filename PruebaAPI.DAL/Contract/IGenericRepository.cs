using System.Linq.Expressions;


namespace PruebaAPI.DAL.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> Getall(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<T> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
    }
}
