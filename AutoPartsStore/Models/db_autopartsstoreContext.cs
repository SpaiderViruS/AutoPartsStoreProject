using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class db_autopartsstoreContext : DbContext
    {
        public db_autopartsstoreContext()
        {
        }
        public static db_autopartsstoreContext DbContext { get; private set; }

        static db_autopartsstoreContext() => DbContext = new db_autopartsstoreContext();

        public db_autopartsstoreContext(DbContextOptions<db_autopartsstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autopart> Autopart { get; set; }
        public virtual DbSet<Busket> Busket { get; set; }
        public virtual DbSet<Busketautopart> Busketautopart { get; set; }
        public virtual DbSet<Characteristik> Characteristik { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Manufracturer> Manufracturer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;user=root;database=db_autopartsstore;password=a678e321r", x => x.ServerVersion("8.0.30-mysql")).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autopart>(entity =>
            {
                entity.HasKey(e => e.IdAutoPart)
                    .HasName("PRIMARY");

                entity.ToTable("autopart");

                entity.HasIndex(e => e.IdCharacteristik)
                    .HasName("FK_Autopart_Characteristik_idx");

                entity.HasIndex(e => e.IdManufracturer)
                    .HasName("FK_Autopart_Manufracturer_idx");

                entity.HasIndex(e => e.IdStatusAutoPart)
                    .HasName("FK_Autopart_Status_idx");

                entity.Property(e => e.IdAutoPart).HasColumnName("idAutoPart");

                entity.Property(e => e.AutoPartImage)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AutoPartName)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdCharacteristik).HasColumnName("idCharacteristik");

                entity.Property(e => e.IdManufracturer).HasColumnName("idManufracturer");

                entity.Property(e => e.IdStatusAutoPart).HasColumnName("idStatusAutoPart");

                entity.HasOne(d => d.IdCharacteristikNavigation)
                    .WithMany(p => p.Autopart)
                    .HasForeignKey(d => d.IdCharacteristik)
                    .HasConstraintName("FK_Autopart_Characteristik");

                entity.HasOne(d => d.IdManufracturerNavigation)
                    .WithMany(p => p.Autopart)
                    .HasForeignKey(d => d.IdManufracturer)
                    .HasConstraintName("FK_Autopart_Manufracturer");

                entity.HasOne(d => d.IdStatusAutoPartNavigation)
                    .WithMany(p => p.Autopart)
                    .HasForeignKey(d => d.IdStatusAutoPart)
                    .HasConstraintName("FK_Autopart_Status");
            });

            modelBuilder.Entity<Busket>(entity =>
            {
                entity.HasKey(e => e.IdBusket)
                    .HasName("PRIMARY");

                entity.ToTable("busket");

                entity.HasIndex(e => e.IdUser)
                    .HasName("FK_Busket_User_idx");

                entity.Property(e => e.IdBusket).HasColumnName("idBusket");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasColumnName("orderStatus")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Busket)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Busket_User");
            });

            modelBuilder.Entity<Busketautopart>(entity =>
            {
                entity.HasKey(e => new { e.IdBusketAutopart, e.IdBusket, e.IdAutopart })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("busketautopart");

                entity.HasIndex(e => e.IdAutopart)
                    .HasName("FK_Busketautopart_Autopart_idx");

                entity.HasIndex(e => e.IdBusket)
                    .HasName("FK_Busketautopart_Busket_idx");

                entity.Property(e => e.IdBusketAutopart)
                    .HasColumnName("idBusketAutopart")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdBusket).HasColumnName("idBusket");

                entity.Property(e => e.IdAutopart).HasColumnName("idAutopart");

                entity.HasOne(d => d.IdAutopartNavigation)
                    .WithMany(p => p.Busketautopart)
                    .HasForeignKey(d => d.IdAutopart)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Busketautopart_Autopart");

                entity.HasOne(d => d.IdBusketNavigation)
                    .WithMany(p => p.Busketautopart)
                    .HasForeignKey(d => d.IdBusket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Busketautopart_Busket");
            });

            modelBuilder.Entity<Characteristik>(entity =>
            {
                entity.HasKey(e => e.IdCharacteristik)
                    .HasName("PRIMARY");

                entity.ToTable("characteristik");

                entity.HasIndex(e => e.Idmanufracturer)
                    .HasName("FK_Characteristik_Manufracturer_idx");

                entity.Property(e => e.IdCharacteristik).HasColumnName("idCharacteristik");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Idmanufracturer).HasColumnName("IDManufracturer");

                entity.HasOne(d => d.IdmanufracturerNavigation)
                    .WithMany(p => p.Characteristik)
                    .HasForeignKey(d => d.Idmanufracturer)
                    .HasConstraintName("FK_Characteristik_Manufracturer");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry)
                    .HasName("PRIMARY");

                entity.ToTable("country");

                entity.Property(e => e.IdCountry).HasColumnName("idCountry");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Manufracturer>(entity =>
            {
                entity.HasKey(e => e.IdManufracturer)
                    .HasName("PRIMARY");

                entity.ToTable("manufracturer");

                entity.HasIndex(e => e.IdCountry)
                    .HasName("FK_Manufracturer_Country_idx");

                entity.Property(e => e.IdManufracturer).HasColumnName("idManufracturer");

                entity.Property(e => e.IdCountry).HasColumnName("idCountry");

                entity.Property(e => e.ManufracturerName)
                    .IsRequired()
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Manufracturer)
                    .HasForeignKey(d => d.IdCountry)
                    .HasConstraintName("FK_Manufracturer_Country");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.HasIndex(e => e.IdBusket)
                    .HasName("FK_Order_Busket_idx");

                entity.Property(e => e.IdOrder).HasColumnName("idOrder");

                entity.Property(e => e.DateOrder).HasColumnType("date");

                entity.Property(e => e.IdBusket).HasColumnName("idBusket");

                entity.HasOne(d => d.IdBusketNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.IdBusket)
                    .HasConstraintName("FK_Order_Busket");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("PRIMARY");

                entity.ToTable("review");

                entity.HasIndex(e => e.IdAutoPart)
                    .HasName("FK_Review_Autopart_idx");

                entity.HasIndex(e => e.IdUser)
                    .HasName("FK_Review_User_idx");

                entity.Property(e => e.IdReview).HasColumnName("idReview");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdAutoPart).HasColumnName("idAutoPart");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdAutoPartNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdAutoPart)
                    .HasConstraintName("FK_Review_Autopart");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Review_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PRIMARY");

                entity.ToTable("role");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.RoleName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PRIMARY");

                entity.ToTable("status");

                entity.Property(e => e.IdStatus).HasColumnName("idStatus");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.HasIndex(e => e.IdRole)
                    .HasName("FK_User_Role_idx");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Image)
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Patronomyc)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Surname)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
