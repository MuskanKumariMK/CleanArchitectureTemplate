using Microsoft.EntityFrameworkCore;
using Template.Application.Interface;
using Template.Infrastructure.Repository;

namespace Template.Infrastructure.Services
{
     /// <summary>
     /// Unit of Work implementation for EF Core.
     /// </summary>
     public class UnitOfWork : IUnitOfWork
     {
          private readonly DbContext _context;
          private readonly Dictionary<Type, object> _repositories = new();

          public UnitOfWork(DbContext context)
          {
               _context = context;
          }

          public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
          {
               var type = typeof(TEntity);
               if (!_repositories.ContainsKey(type))
               {
                    var repositoryInstance = new GenericRepository<TEntity>(_context);
                    _repositories[type] = repositoryInstance;
               }
               return (IGenericRepository<TEntity>)_repositories[type];
          }

          public async Task<int> SaveChangeAsync()
          {
               return await _context.SaveChangesAsync();
          }

          public void Dispose()
          {
               _context.Dispose();
          }
     }
}

///<summary>
///Benefits of Unit of Work

///Transaction Management: Ensures all database operations across repositories are committed together.

///Single Save Point: Only SaveChangeAsync() is called once per unit of work, improving performance and consistency.

///Repository Coordination: Multiple repositories can be used in a single business transaction.

///Lifetime Management: Keeps track of repositories, ensures one DbContext per request.
///</summary>
