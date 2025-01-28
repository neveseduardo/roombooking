using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RoomBooking.Models;

namespace RoomBooking.Database;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = optionsBuilder.Options.FindExtension<CoreOptionsExtension>()?.ApplicationServiceProvider?.GetService<IConfiguration>();

            if (configuration == null)
            {
                throw new Exception("Arquivo de configuração não encontrado.");
            }

            optionsBuilder.UseSqlite(_configuration.GetConnectionString("SQLiteConnection"));
        }
    }

    public DbSet<User> Users { get; set; }
}