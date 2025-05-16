using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WsApiexamen.Models;

public partial class ExamenContextoDb : DbContext
{
    public ExamenContextoDb()
    {
    }

    public ExamenContextoDb(DbContextOptions<ExamenContextoDb> options)
        : base(options)
    {
    }

    public virtual DbSet<TblExaman> TblExamen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ExamenConnString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblExaman>(entity =>
        {
            entity.HasKey(e => e.IdExamen).HasName("PK__tblExame__0E8DC9BE06E7A6A2");

            entity.ToTable("tblExamen");

            entity.Property(e => e.IdExamen).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
