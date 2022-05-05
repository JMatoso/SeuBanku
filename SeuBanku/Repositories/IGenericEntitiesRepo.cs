using SeuBanku.Helpers.Paging;
using SeuBanku.Models;
using System.Linq.Expressions;

namespace SeuBanku.Repositories
{
    public interface IGenericEntitiesRepo<TEntity> where TEntity : class
    {
        Task<Result> DeleteObjectAsync(Guid itemId);
        Task<TEntity?> GetObjectAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> GetObjectsAsync(Expression<Func<TEntity, bool>>? expression = null, IOrderedQueryable<TEntity>? orderBy = null);
        Task<PagedList<TEntity>> GetObjectsAsync(Parameters parameters, Expression<Func<TEntity, bool>>? expression = null, IOrderedQueryable<TEntity>? orderBy = null);
        Task<Result> InsertObjectAsync(TEntity entity, Expression<Func<TEntity, bool>>? predicate = null);
        Task<bool> ObjectExists(Expression<Func<TEntity, bool>> expression);
        Task<Result> UpdateObjectAsync(TEntity entity);
    }
}