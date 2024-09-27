using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
