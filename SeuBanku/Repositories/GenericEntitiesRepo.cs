using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SeuBanku.Data;
using SeuBanku.Helpers.Paging;
using SeuBanku.Models;
using System.Linq.Expressions;

namespace SeuBanku.Repositories
{
    public class GenericEntitiesRepo<TEntity> : IGenericEntitiesRepo<TEntity> 
        where TEntity : class
    {
        private readonly DbSet<TEntity> _db;
        private readonly IMemoryCache _memoryCache;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GenericEntitiesRepo<TEntity>> _logger;

        public GenericEntitiesRepo(
            IMemoryCache memoryCache,
            ApplicationDbContext dbContext,
            ILogger<GenericEntitiesRepo<TEntity>> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            _memoryCache = memoryCache;
            _db = _dbContext.Set<TEntity>();
        }

        public async Task<Result> DeleteObjectAsync(Guid itemId)
        {
            var entity = await _db.FindAsync(itemId);
            if (entity is null)
            {
                return ResultProvider.Set("Not found.", false, new Dictionary<object, object>()
                {
                    {
                        nameof(itemId),
                        "Item not found."
                    }
                });
            }

            _db.Remove(entity);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Removed: {id}", itemId);
            return ResultProvider.Set(string.Empty, true);
        }

        public async Task<bool> ObjectExists(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = _db;

            return await query.AnyAsync(expression);
        }

        public async Task<List<TEntity>> GetObjectsAsync(
            Expression<Func<TEntity, bool>>? expression = null,
            IOrderedQueryable<TEntity>? orderBy = null)
        {
            IQueryable<TEntity> query = _db;

            object value = expression is null ? "default" : expression;

            if (!_memoryCache.TryGetValue(value, out List<TEntity> data))
            {
                if (data is null)
                {
                    if (expression != null) query = query.Where(expression);

                    data = await query.AsNoTracking().ToListAsync();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(5));

                    _memoryCache.Set(value, data, cacheOptions);
                }
            }

            return data;
        }

        public async Task<PagedList<TEntity>> GetObjectsAsync(
            Parameters parameters,
            Expression<Func<TEntity, bool>>? expression = null,
            IOrderedQueryable<TEntity>? orderBy = null)
        {
            IQueryable<TEntity> query = _db;

            return expression != null
                    ? PagedList<TEntity>
                        .ToPagedList(await query
                            .Where(expression)
                            .ToListAsync(), parameters.PageNumber, parameters.PageSize)
                    : PagedList<TEntity>
                        .ToPagedList(await query
                            .AsNoTracking()
                            .ToListAsync(), parameters.PageNumber, parameters.PageSize);
        }

        public async Task<TEntity?> GetObjectAsync(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = _db;

            var result = await query.AsNoTracking().SingleOrDefaultAsync(expression);
            return result ?? null;
        }

        public async Task<Result> InsertObjectAsync(
            TEntity entity, Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> query = _db;

            if (predicate != null)
            {
                if (await query.AnyAsync(predicate))
                {
                    return ResultProvider.Set("Data Conflict.", false, new Dictionary<object, object>()
                    {
                        {
                            nameof(predicate.Name),
                            "Item in use."
                        }
                    });
                }
            }

            await _db.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return ResultProvider.Set();
        }

        public async Task<Result> UpdateObjectAsync(TEntity entity)
        {
            _db.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return ResultProvider.Set();
        }
    }
}
