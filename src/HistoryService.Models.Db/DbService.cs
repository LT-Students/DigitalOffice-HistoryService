using LT.DigitalOffice.Kernel.Attributes.ParseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [IgnoreParse]
        public ICollection<DbServiceHistory> ServiceHistories { get; set; }

        public DbService()
        {
            ServiceHistories = new HashSet<DbServiceHistory>();
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
                .HasMany(s => s.ServiceHistories)
                .WithOne(sh => sh.Service);
        }
    }
}
