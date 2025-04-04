using Data.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Infra.Context
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<DataModel> DataModel { get; set; }
    }
}