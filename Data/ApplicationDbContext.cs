using Microsoft.EntityFrameworkCore;
using TesteTecnico_.NET.Models;

namespace TesteTecnico_.NET.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Main> Temps { get; set; }
  }
}
