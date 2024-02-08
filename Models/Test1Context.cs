using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntrevistaTecnica.Models;

public partial class Test1Context : DbContext
{
    public Test1Context()
    {
    }

    public Test1Context(DbContextOptions<Test1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Jugador> Jugadors { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Partidum> Partida { get; set; }

    public virtual DbSet<Rondum> Ronda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-SLMKS60\\MSSQLSERVER01; Database=test1; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Jugador__3213E83F7A4001B1");

            entity.ToTable("Jugador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3213E83F70F96B33");

            entity.ToTable("Movimiento");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Partidum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Partida__3213E83F9BA692A7");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ganador).HasColumnName("ganador");
            entity.Property(e => e.IdJugador1).HasColumnName("id_jugador_1");
            entity.Property(e => e.IdJugador2).HasColumnName("id_jugador_2");

            entity.HasOne(d => d.GanadorNavigation).WithMany(p => p.PartidumGanadorNavigations)
                .HasForeignKey(d => d.Ganador)
                .HasConstraintName("FK__Partida__ganador__76969D2E");

            entity.HasOne(d => d.IdJugador1Navigation).WithMany(p => p.PartidumIdJugador1Navigations)
                .HasForeignKey(d => d.IdJugador1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partida__id_juga__74AE54BC");

            entity.HasOne(d => d.IdJugador2Navigation).WithMany(p => p.PartidumIdJugador2Navigations)
                .HasForeignKey(d => d.IdJugador2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partida__id_juga__75A278F5");
        });

        modelBuilder.Entity<Rondum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ronda__3213E83F76E5F1D3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ganador).HasColumnName("ganador");
            entity.Property(e => e.IdJugador1).HasColumnName("id_jugador_1");
            entity.Property(e => e.IdJugador2).HasColumnName("id_jugador_2");
            entity.Property(e => e.MovimientoJugador1).HasColumnName("movimiento_jugador_1");
            entity.Property(e => e.MovimientoJugador2).HasColumnName("movimiento_jugador_2");
            entity.Property(e => e.Partida).HasColumnName("partida");

            entity.HasOne(d => d.IdJugador1Navigation).WithMany(p => p.RondumIdJugador1Navigations)
                .HasForeignKey(d => d.IdJugador1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ronda__id_jugado__797309D9");

            entity.HasOne(d => d.IdJugador2Navigation).WithMany(p => p.RondumIdJugador2Navigations)
                .HasForeignKey(d => d.IdJugador2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ronda__id_jugado__7A672E12");

            entity.HasOne(d => d.MovimientoJugador1Navigation).WithMany(p => p.RondumMovimientoJugador1Navigations)
                .HasForeignKey(d => d.MovimientoJugador1)
                .HasConstraintName("FK__Ronda__movimient__7B5B524B");

            entity.HasOne(d => d.MovimientoJugador2Navigation).WithMany(p => p.RondumMovimientoJugador2Navigations)
                .HasForeignKey(d => d.MovimientoJugador2)
                .HasConstraintName("FK__Ronda__movimient__7C4F7684");

            entity.HasOne(d => d.PartidaNavigation).WithMany(p => p.RondumPartidaNavigations)
                .HasForeignKey(d => d.Partida)
                .HasConstraintName("FK__Ronda__partida__7D439ABD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
