using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend_milagrofinanciero.Data.BankModels;

namespace backend_milagrofinanciero.Data;

public partial class MilagrofinancieroG1Context : DbContext
{
    public MilagrofinancieroG1Context()
    {
    }

    public MilagrofinancieroG1Context(DbContextOptions<MilagrofinancieroG1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Banco> Bancos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteXCuenta> ClienteXcuenta { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Localidad> Localidads { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }

    public virtual DbSet<TipoTransaccion> TipoTransaccions { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Database=milagrofinanciero-g1;Integrated Security=true;Port=5432;User Id=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Banco_pkey");

            entity.ToTable("Banco");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying(45)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cliente_pkey");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.Calle)
                .HasColumnType("character varying(45)")
                .HasColumnName("calle");
            entity.Property(e => e.CuitCuil)
                .HasColumnType("character varying(10)")
                .HasColumnName("cuit_cuil");
            entity.Property(e => e.Departamento)
                .HasColumnType("character varying(45)")
                .HasColumnName("departamento");
            entity.Property(e => e.Numero)
                .HasColumnType("character varying(5)")
                .HasColumnName("numero");
            entity.Property(e => e.RazonSocial)
                .HasColumnType("character varying(45)")
                .HasColumnName("razon_social");

            entity.HasOne(d => d.Localidad).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Localidad_ID");
        });

        modelBuilder.Entity<ClienteXCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClienteXCuenta_pkey");

            entity.ToTable("ClienteXCuenta");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.Rol)
                .HasColumnType("character varying(45)")
                .HasColumnName("rol");

            entity.HasOne(d => d.Cliente).WithOne(p => p.ClienteXCuenta)
                .HasForeignKey<ClienteXCuenta>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cliente_ID");

            entity.HasOne(d => d.Cuenta).WithOne(p => p.ClienteXCuenta)
                .HasForeignKey<ClienteXCuenta>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cuenta_ID");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cuenta_pkey");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Cbu).HasColumnName("cbu");
            entity.Property(e => e.NumeroCuenta).HasColumnName("numero_cuenta");

            entity.HasOne(d => d.Banco).WithOne(p => p.Cuenta)
                .HasForeignKey<Cuenta>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Banco_ID");

            entity.HasOne(d => d.TipoCuenta).WithOne(p => p.Cuenta)
                .HasForeignKey<Cuenta>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TipoCuenta_ID");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Empleado_pkey");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.CuitCuil).HasColumnName("cuit_cuil");
            entity.Property(e => e.Legajo).HasColumnName("legajo");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying(45)")
                .HasColumnName("nombre");

            entity.HasOne(d => d.Sucursal).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sucursal_ID");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Localidad_pkey");

            entity.ToTable("Localidad");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Cp)
                .HasColumnType("character varying(10)[]")
                .HasColumnName("cp");

            entity.HasOne(d => d.Provincia).WithOne(p => p.Localidad)
                .HasForeignKey<Localidad>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Provincia_ID");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pais_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying(45)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Provincia_pkey");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying(45)")
                .HasColumnName("nombre");

            entity.HasOne(d => d.Pais).WithOne(p => p.Provincia)
                .HasForeignKey<Provincia>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pais_ID");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sucursal_pkey");

            entity.ToTable("Sucursal");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Cp)
                .HasColumnType("character varying(15)")
                .HasColumnName("cp");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying(45)")
                .HasColumnName("nombre");

            entity.HasOne(d => d.Provincia).WithOne(p => p.Sucursal)
                .HasForeignKey<Sucursal>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Provincia_ID");
        });

        modelBuilder.Entity<TipoCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoCuenta_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying(45)[]")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoTransaccion_pkey");

            entity.ToTable("TipoTransaccion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying(45)")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transaccion_pkey");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Acreditacion).HasColumnName("acreditacion");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.Motivo)
                .HasColumnType("character varying(45)")
                .HasColumnName("motivo");
            entity.Property(e => e.NumeroOperacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("numero_operacion");
            entity.Property(e => e.Realizacion).HasColumnName("realizacion");
            entity.Property(e => e.Referencia)
                .HasColumnType("character varying(45)")
                .HasColumnName("referencia");

            entity.HasOne(d => d.Cuenta).WithOne(p => p.Transaccion)
                .HasForeignKey<Transaccion>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CuentaDestino_ID");

            entity.HasOne(d => d.TipoTransaccion).WithOne(p => p.Transaccion)
                .HasForeignKey<Transaccion>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TipoTransaccion_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
