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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //todo: only for dev purpose
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Competition>(ConfigureCompetition);
            builder.Entity<TableItem>(ConfigureTableItem);
        }

        private void ConfigureCompetition(EntityTypeBuilder<Competition> builder)
        {
            builder.HasMany(i => i.Standings)
                .WithOne(s => s.Competition);
        }

        private void ConfigureTableItem(EntityTypeBuilder<TableItem> builder)
        {
            builder.HasOne(i => i.Team)
                .WithMany()
                .IsRequired();
        }
    }
}
