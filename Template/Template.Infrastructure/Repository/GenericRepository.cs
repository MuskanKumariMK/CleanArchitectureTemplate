using System.Linq.Expressions;
using BuildingBlock.Pagination;
using Microsoft.EntityFrameworkCore;
using Template.Application.Interface;

namespace Template.Infrastructure.Repository
{
     /// <summary>
     /// Generic repository interface for basic CRUD and pagination operations.
     /// </summary>
     /// <typeparam name="TEntity">The entity type.</typeparam>
     /// <remarks>
     /// Provides a standard interface for working with any EF Core entity.
     /// Supports asynchronous operations and pagination.
     /// </remarks>
     public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
     {
          protected readonly DbContext _context;
          protected readonly DbSet<TEntity> _dbSet;

          public GenericRepository(DbContext context)
          {
               _context = context;
               _dbSet = context.Set<TEntity>();
          }
          /// <summary>
          /// Gets an entity by its primary key.
          /// </summary>
          public async Task<TEntity?> GetByIdAsync(object id)
          {
               return await _dbSet.FindAsync(id);
          }
          /// <summary>
          /// Gets all entities.
          /// </summary>
          public async Task<List<TEntity>> GetAllAsync()
          {
               return await _dbSet.ToListAsync();
          }

          /// <summary>
          /// Core pagination method with search + sorting support.
          /// How Use 
          /// var result = await _unitOfWork.Repository<User>().GetPaginatedWithFiltersAsync(
          ///                new PaginateRequest ( pageIndex: 1, pageSize: 10,  SortColumn: "name", SortOrder: "asc"),
          ///               filter: u => u.IsActive, // only active users
          ///               searchFilter: q => q.Where(u => u.Name.Contains("user") || u.Email.Contains("user")),
          ///                sortableColumns: new ()
          ///              {  { "name", u => u.Name     },       { "email", u => u.Email   }
          ///              );



          /// </summary>
          public async Task<PaginateResult<TEntity>> GetPaginateAsync(
                          PaginateRequest request,
                          Expression<Func<TEntity, bool>>? filter = null,
                          Func<IQueryable<TEntity>, IQueryable<TEntity>>? searchFilter = null,
                          Dictionary<string, Expression<Func<TEntity, object>>>? sortableColumns = null)
          {
               IQueryable<TEntity> query = _dbSet.AsQueryable();

               // Apply extra filter if provided (e.g., restaurantId constraint)
               if (filter != null)
                    query = query.Where(filter);

               // Apply search if delegate provided
               if (searchFilter != null)
                    query = searchFilter(query);

               // Sorting
               if (!string.IsNullOrEmpty(request.SortColumn) && sortableColumns != null &&
                   sortableColumns.TryGetValue(request.SortColumn.ToLower(), out var sortExpr))
               {
                    query = request.SortOrder?.ToLower() == "desc"
                        ? query.OrderByDescending(sortExpr)
                        : query.OrderBy(sortExpr);
               }
               else
               {

                    query = query.OrderBy(e => 0);
               }

               // Use generic paginator
               return await PaginateResult<TEntity>.CreateAsync(query, request.PageIndex, request.PageSize);
          }
          /// <summary>
          /// Add The Entity 
          /// </summary>
          /// <param name="entity"></param>
          /// <returns></returns>
          public async Task AddAsync(TEntity entity)
          {
               await _dbSet.AddAsync(entity);
          }

          public void Update(TEntity entity)
          {
               _dbSet.Update(entity);
          }

          public void Remove(TEntity entity)
          {
               _dbSet.Remove(entity);
          }

          public IQueryable<TEntity> Query()
          {
               return _dbSet.AsQueryable();
          }
     }
}
///<summary>
///Benefits of Generic Repository

///Encapsulation of Data Access Logic: Keeps EF Core queries hidden inside the repository, so application code doesn’t directly deal with DbContext.

///Reusability: Works with all entities (TEntity) without duplicating CRUD logic.

///Consistency: All queries, sorting, pagination, and filtering follow a single standard.

///Testability: Easy to mock repositories in unit tests instead of mocking DbContext directly.
///</summary>