using Microsoft.EntityFrameworkCore;

namespace BeerManagement.Repository.DatabaseContext
{
    public partial class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Bars> Bars { get; set; }
        public virtual DbSet<Beers> Beers { get; set; }
        public virtual DbSet<Brewery> Brewery { get; set; }
        public virtual DbSet<LinkBarWithBeer> LinkBarWithBeer { get; set; }
        public virtual DbSet<LinkBreweryWithBeer> LinkBreweryWithBeer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bars>(entity =>
            {
                entity.HasKey(e => e.BarId)
                    .HasName("PK__Bar__E08249958669D15D");

                entity.HasIndex(e => e.BarName)
                    .HasName("UK_Key_BarName")
                    .IsUnique();

                entity.Property(e => e.BarAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BarName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Beers>(entity =>
            {
                entity.HasKey(e => e.BeerId)
                    .HasName("PK__Beers__293C94BF27384B5F");

                entity.HasIndex(e => e.BeerName)
                    .HasName("UK_Key_BeerName")
                    .IsUnique();

                entity.Property(e => e.BeerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PercentageAlcoholByVolume).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Brewery>(entity =>
            {
                entity.HasIndex(e => e.BreweryName)
                    .HasName("UK_Key_BreweryName")
                    .IsUnique();

                entity.Property(e => e.BreweryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LinkBarWithBeer>(entity =>
            {
                entity.HasOne(d => d.Bar)
                    .WithMany(p => p.LinkBarWithBeer)
                    .HasForeignKey(d => d.BarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkBarWithBeer_BarId");

                entity.HasOne(d => d.Beer)
                    .WithMany(p => p.LinkBarWithBeer)
                    .HasForeignKey(d => d.BeerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkBarWithBeer_BeerId");
            });

            modelBuilder.Entity<LinkBreweryWithBeer>(entity =>
            {
                entity.HasOne(d => d.Beer)
                    .WithMany(p => p.LinkBreweryWithBeer)
                    .HasForeignKey(d => d.BeerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkBreweryWithBeer_BeerId");

                entity.HasOne(d => d.Brewery)
                    .WithMany(p => p.LinkBreweryWithBeer)
                    .HasForeignKey(d => d.BreweryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LinkBreweryWithBeer_BreweryId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}