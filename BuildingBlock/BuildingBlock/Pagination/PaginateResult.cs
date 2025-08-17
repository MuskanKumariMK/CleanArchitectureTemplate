namespace BuildingBlock.Pagination
{
  public class PaginateResult<TEntity>(int pageIndex , int pageSize , long count , IEnumerable<TEntity> data)
        where TEntity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public IEnumerable<TEntity> Data { get; } = data;
        public long Count { get; } = count;

    }
  
  
}
