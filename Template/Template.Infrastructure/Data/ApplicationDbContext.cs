using Microsoft.EntityFrameworkCore;
using Security.Infrastructure.Data.Seed;

namespace Template.Infrastructure.Data
{
     public class ApplicationDbContext : DbContext
     {
          /// <summary>
          /// ApplicationDbContext constructor
          /// </summary>
          /// <param name="option"></param>
          public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
          {

          }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               modelBuilder.Seed();
          }
     }
}
