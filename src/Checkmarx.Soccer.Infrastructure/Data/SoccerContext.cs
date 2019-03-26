using System;
using Checkmarx.Soccer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkmarx.Soccer.Infrastructure.Data
{
    public class SoccerContext : DbContext
    {
        public SoccerContext(DbContextOptions<SoccerContext> options) : base(options)
        {
        }

        public DbSet<CompetitionArea> Areas { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Standing> Standings { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TableItem> TableItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TableItem>(ConfigureTableItem);
        }

        private void ConfigureTableItem(EntityTypeBuilder<TableItem> builder)
        {
            builder.HasOne(i => i.Team)
                .WithMany()
                .IsRequired();
        }
    }
}
