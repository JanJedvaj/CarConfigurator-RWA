using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarConfigurator.Models;

public partial class CarConfiguratorContext : DbContext
{
    public CarConfiguratorContext()
    {
    }

    public CarConfiguratorContext(DbContextOptions<CarConfiguratorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarComponent> CarComponents { get; set; }

    public virtual DbSet<CarComponentCompatibility> CarComponentCompatibilities { get; set; }

    public virtual DbSet<ComponentType> ComponentTypes { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<ConfigurationCarComponent> ConfigurationCarComponents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-N9V5141;Database=CarConfigurator; User Id=sa; Password=sql; Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarCompo__3214EC0791021DAD");

            entity.ToTable("CarComponent");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImagePath).HasMaxLength(512);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.ComponentType).WithMany(p => p.CarComponents)
                .HasForeignKey(d => d.ComponentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarCompon__Compo__4BAC3F29");
        });

        modelBuilder.Entity<CarComponentCompatibility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarCompo__3214EC07B452536B");

            entity.ToTable("CarComponentCompatibility");

            entity.HasOne(d => d.CarComponentId1Navigation).WithMany(p => p.CarComponentCompatibilityCarComponentId1Navigations)
                .HasForeignKey(d => d.CarComponentId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarCompon__CarCo__4E88ABD4");

            entity.HasOne(d => d.CarComponentId2Navigation).WithMany(p => p.CarComponentCompatibilityCarComponentId2Navigations)
                .HasForeignKey(d => d.CarComponentId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarCompon__CarCo__4F7CD00D");
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Componen__3214EC075FD2664E");

            entity.ToTable("ComponentType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Configur__3214EC0740A93511");

            entity.ToTable("Configuration");

            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Configurations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Configura__UserI__5535A963");
        });

        modelBuilder.Entity<ConfigurationCarComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Configur__3214EC0716AFE93D");

            entity.ToTable("ConfigurationCarComponent");

            entity.HasOne(d => d.CarComponent).WithMany(p => p.ConfigurationCarComponents)
                .HasForeignKey(d => d.CarComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Configura__CarCo__59063A47");

            entity.HasOne(d => d.Configuration).WithMany(p => p.ConfigurationCarComponents)
                .HasForeignKey(d => d.ConfigurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Configura__Confi__5812160E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0764513688");

            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(256);
            entity.Property(e => e.PwdHash).HasMaxLength(256);
            entity.Property(e => e.PwdSalt).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
