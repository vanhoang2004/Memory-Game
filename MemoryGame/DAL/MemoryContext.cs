using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;

namespace MemoryGame.DAL
{
    public class MemoryGameContext : DbContext
    {
        private static MemoryGameContext _instance;
        public static MemoryGameContext Instance()
        {
            if (_instance == null)
                _instance = new MemoryGameContext();
            return _instance;
        }
        public MemoryGameContext()
        {
        }

        public MemoryGameContext(DbContextOptions<MemoryGameContext> options)
            : base(options)
        {
        }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Stage> Stages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-2IO3BS1L;Database=memorygame;User Id=sa;Password=123;Encrypt=False;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stage>(entity =>
            {
                entity.HasKey(e => e.StageId);
                entity.ToTable("Stage");

                entity.Property(e => e.StageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("StageId");

                entity.Property(e => e.StageTitle)
                    .HasColumnName("StageTitle");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(e => e.LevelId);
                entity.ToTable("Level");

                entity.Property(e => e.LevelId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LevelId");

                entity.Property(e => e.LevelTitle)
                    .HasColumnName("LevelTitle");

                entity.Property(e => e.NumberOfSquare)
                    .HasColumnName("NumberOfSquare");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.ToTable("Login");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UserId");

                entity.Property(e => e.Username)
                    .HasColumnName("Username")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("Password")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StageId)
                    .HasColumnName("StageId");

                entity.HasOne(e => e.Stage)
                    .WithMany(s => s.Logins)
                    .HasForeignKey(e => e.StageId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
