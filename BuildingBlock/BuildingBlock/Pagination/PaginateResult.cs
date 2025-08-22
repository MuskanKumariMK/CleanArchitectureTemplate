using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Pagination
{
     /// <summary>
     /// Represents a paginated result set.
     /// </summary>
     /// <typeparam name="TEntity">The type of the entity in the paginated list.</typeparam>
     /// <remarks>
     /// This class is used to return paginated data from a query, along with metadata
     /// like total count, page size, and page index. It also provides helper properties
     /// to check for next and previous pages.
     /// </remarks>
     public class PaginateResult<TEntity>
       where TEntity : class
     {
          /// <summary>
          /// Initializes a new instance of the <see cref="PaginateResult{TEntity}"/> class.
          /// </summary>
          /// <param name="pageIndex">The current page index (1-based).</param>
          /// <param name="pageSize">The size of each page.</param>
          /// <param name="count">The total number of items in the full query.</param>
          /// <param name="data">The list of items in the current page.</param>
          public PaginateResult(int pageIndex, int pageSize, long count, List<TEntity> data)
          {
               PageIndex = pageIndex;
               PageSize = pageSize;
               Count = count;
               Data = data;
          }

          /// <summary>
          /// The current page index (1-based).
          /// </summary>
          public int PageIndex { get; }

          /// <summary>
          /// The number of items per page.
          /// </summary>
          public int PageSize { get; }

          /// <summary>
          /// The total number of items available in the query.
          /// </summary>
          public long Count { get; }

          /// <summary>
          /// The list of items in the current page.
          /// </summary>
          public List<TEntity> Data { get; }

          /// <summary>
          /// Indicates whether there is a next page available.
          /// </summary>
          public bool HasNextPage => (PageIndex * PageSize) < Count;

          /// <summary>
          /// Indicates whether there is a previous page available.
          /// </summary>
          public bool HasPreviousPage => PageIndex > 1;

          /// <summary>
          /// Asynchronously creates a paginated result from an <see cref="IQueryable{TEntity}"/>.
          /// </summary>
          /// <param name="query">The query to paginate.</param>
          /// <param name="pageIndex">The page index to retrieve (1-based).</param>
          /// <param name="pageSize">The number of items per page.</param>
          /// <returns>A <see cref="PaginateResult{TEntity}"/> containing the requested page of data.</returns>
          public static async Task<PaginateResult<TEntity>> CreateAsync(IQueryable<TEntity> query, int pageIndex, int pageSize)
          {
               // Count the total number of items in the query.
               int totalCount = await query.CountAsync();

               // Fetch the specific page of data.
               var data = await query
                   .Skip((pageIndex - 1) * pageSize) // Skip items from previous pages
                   .Take(pageSize)                  // Take the items for the current page
                   .ToListAsync();

               // Return a new PaginateResult instance with the fetched data.
               return new(pageIndex, pageSize, totalCount, data);
          }
     }
}