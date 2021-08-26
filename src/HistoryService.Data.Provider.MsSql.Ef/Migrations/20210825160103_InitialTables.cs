using LT.DigitalOffice.HistoryService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.HistoryService.Data.Provider.MsSql.Ef.Migrations
{
    [DbContext(typeof(HistoryServiceDbContext))]
    [Migration("20210825160103_InitialTables")]
    public class InitialTables : Migration
    {
        protected override void Up(MigrationBuilder builder)
        {
            builder.CreateTable(
                name: DbService.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false, maxLength: 30),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedAtUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            builder.AddUniqueConstraint(
                name: $"UX_Name_unique",
                table: DbService.TableName,
                column: nameof(DbService.Name));

            builder.CreateTable(
                name: DbServicesHistories.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false),
                    Version = table.Column<string>(nullable: false, maxLength: 15),
                    Content = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedAtUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesHistories", x => x.Id);
                });
        }
    }
}
