using LT.DigitalOffice.HistoryService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LT.DigitalOffice.HistoryService.Data.Provider.MsSql.Ef
{
    public class HistoryServiceDbContext : DbContext, IDataProvider
    {
        public DbSet<DbService> Services { get; set; }
        public DbSet<DbServicesHistories> ServiceHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.Load("LT.DigitalOffice.HistoryService.Models.Db"));
        }

        public HistoryServiceDbContext(DbContextOptions<HistoryServiceDbContext> options)
            : base(options)
        {
        }

        public void Save()
        {
            SaveChanges();
        }

        public object MakeEntityDetached(object obj)
        {
            Entry(obj).State = EntityState.Detached;

            return Entry(obj).State;
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
