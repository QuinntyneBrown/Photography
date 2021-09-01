using Photography.Api.Models;
using Photography.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Photography.Api.Data
{
    public class PhotographyDbContext: DbContext, IPhotographyDbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<Photo> Photos { get; private set; }
        public PhotographyDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhotographyDbContext).Assembly);
        }
        
    }
}
