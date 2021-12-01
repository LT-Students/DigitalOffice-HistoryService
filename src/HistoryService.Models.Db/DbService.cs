using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Models.Db
{
  public class DbService
  {
    public const string TableName = "Services";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }

    public ICollection<DbServiceHistory> ServicesHistories { get; set; }

    public DbService()
    {
      ServicesHistories = new HashSet<DbServiceHistory>();
    }
  }

  public class DbServicesConfiguration : IEntityTypeConfiguration<DbService>
  {
    public void Configure(EntityTypeBuilder<DbService> builder)
    {
      builder.
        ToTable(DbService.TableName);

      builder.
        HasKey(p => p.Id);

      builder
        .Property(p => p.Name)
        .IsRequired();

      builder
        .HasMany(s => s.ServicesHistories)
        .WithOne(sh => sh.Service);
    }
  }
}
