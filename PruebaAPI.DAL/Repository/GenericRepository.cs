using Microsoft.EntityFrameworkCore;
using PruebaAPI.DAL.Contract;
using PruebaAPI.DAL.DBContext;
using System.Linq.Expressions;

namespace PruebaAPI.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MyDbContext _dbContext;

        public GenericRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<T>> Getall(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                IQueryable<T> queryModel = filter == null ? _dbContext.Set<T>() : _dbContext.Set<T>().Where(filter);
                return queryModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Create(T model)
        {
            try
            {
                _dbContext.Set<T>().AddAsync(model);
                await _dbContext.SaveChangesAsync();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Update(T model)
        {
            try
            {
                _dbContext.Set<T>().Update(model);
                await _dbContext.SaveChangesAsync();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            try
            {
                T modelFilter = await _dbContext.Set<T>().FirstOrDefaultAsync(filter);
                return modelFilter;
            }
            catch (Exception)
            {

                throw;
            }
        }




        public async Task<bool> Delete(T model)
        {
            try
            {
                _dbContext.Set<T>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
