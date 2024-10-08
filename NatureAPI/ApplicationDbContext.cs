using Microsoft.EntityFrameworkCore;
using NatureAPI.Entities;

namespace NatureAPI;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        
    }
    
    public DbSet<Nature> Nature { get; set; }
}