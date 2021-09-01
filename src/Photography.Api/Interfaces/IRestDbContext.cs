using Photography.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Photography.Api.Interfaces
{
    public interface IPhotographyDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Photo> Photos { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
