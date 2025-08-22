using System.Linq.Expressions;
using BuildingBlock.Pagination;

namespace Template.Application.Interface
{
     /// <summary>
     /// Generic repository interface for basic CRUD and pagination operations.
     /// </summary>
     /// <typeparam name="TEntity">The entity type.</typeparam>
     /// <remarks>
     /// Provides a standard interface for working with any EF Core entity.
     /// Supports asynchronous operations and pagination.
     /// </remarks>
     public interface IGenericRepository<TEntity> where TEntity : class
     {
          /// <summary>
          /// Gets an entity by its primary key.
          /// </summary>
          Task<TEntity?> GetByIdAsync(object id);

          /// <summary>
          /// Gets all entities.
          /// </summary>
          Task<List<TEntity>> GetAllAsync();

          /// <summary>
          /// Returns a paginated list of entities.
          /// </summary>
          Task<PaginateResult<TEntity>> GetPaginateAsync(
                            PaginateRequest request,
                            Expression<Func<TEntity, bool>>? filter = null,
                            Func<IQueryable<TEntity>, IQueryable<TEntity>>? searchFilter = null,
                            Dictionary<string, Expression<Func<TEntity, object>>>? sortableColumns = null);

          /// <summary>
          /// Adds a new entity.
          /// </summary>
          Task AddAsync(TEntity entity);

          /// <summary>
          /// Updates an existing entity.
          /// </summary>
          void Update(TEntity entity);

          /// <summary>
          /// Removes an entity.
          /// </summary>
          void Remove(TEntity entity);

          /// <summary>
          /// Returns an IQueryable for custom queries.
          /// </summary>
          IQueryable<TEntity> Query();
     }
}