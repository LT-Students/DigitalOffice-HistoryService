using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.Kernel.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Data.Provider.MsSql.Ef
{
  public class HistoryServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbService> Services { get; set; }
    public DbSet<DbServiceHistory> ServicesHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(
        Assembly.Load("LT.DigitalOffice.HistoryService.Models.Db"));
    }

    public HistoryServiceDbContext(DbContextOptions<HistoryServiceDbContext> options)
      : base(options)
    {
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;
      return Entry(obj).State;
    }

    void IBaseDataProvider.Save()
    {
      SaveChanges();
    }

    async Task IBaseDataProvider.SaveAsync()
    {
      await SaveChangesAsync();
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }
  }
}
