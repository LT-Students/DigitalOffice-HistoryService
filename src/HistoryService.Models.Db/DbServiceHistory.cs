using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.HistoryService.Models.Db
{
    public class DbServiceHistory
    {
        public const string TableName = "ServiceHistories";

        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public string Version { get; set; }
        public string Content { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }

        public DbService Service { get; set; }
    }
    public class DbServiceHistoryConfiguration : IEntityTypeConfiguration<DbServiceHistory>
    {
        public void Configure(EntityTypeBuilder<DbServiceHistory> builder)
        {
            builder.
                ToTable(DbServiceHistory.TableName);

            builder.
                HasKey(p => p.Id);

            builder
                .Property(p => p.Version)
                .IsRequired();

            builder
                .Property(p => p.Content)
                .IsRequired();

            builder
                .HasOne(sh => sh.Service)
                .WithMany(s => s.ServiceHistories);
        }
    }
}
