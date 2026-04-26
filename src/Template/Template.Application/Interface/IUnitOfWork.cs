namespace Template.Application.Interface
{
     /// <summary>
     /// Unit of Work interface for managing multiple repositories and committing transactions.
     /// </summary>
     /// <remarks>
     /// Helps coordinate changes across multiple repositories and ensures atomic commits.
     /// </remarks>
     public interface IUnitOfWork : IDisposable
     {
          IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
          Task<int> SaveChangeAsync();
     }
}
