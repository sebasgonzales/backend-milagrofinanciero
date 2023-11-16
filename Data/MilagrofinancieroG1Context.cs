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

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<ClienteXCuenta> ClienteXCuenta { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Localidad> Localidads { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Sucursal> Sucursal { get; set; }

    public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }

    public virtual DbSet<TipoTransaccion> TipoTransaccions { get; set; }

    public virtual DbSet<Transaccion> Transaccion { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Database=milagrofinanciero-g1;Integrated Security=true;Port=5432;User Id=postgres;Password=123456789");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Banco_pkey");

            entity.ToTable("Banco");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cliente_pkey");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.Calle)
                .HasMaxLength(45)
                .HasColumnName("calle");
            entity.Property(e => e.CuitCuil)
                .HasMaxLength(10)
                .HasColumnName("cuit_cuil");
            entity.Property(e => e.Departamento)
                .HasMaxLength(45)
                .HasColumnName("departamento");
            entity.Property(e => e.LocalidadId).HasColumnName("Localidad_ID");
            entity.Property(e => e.Numero)
                .HasMaxLength(45)
                .HasColumnName("numero");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(45)
                .HasColumnName("razon_social");

            entity.HasOne(d => d.Localidad).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.LocalidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_localidad");
        });

        modelBuilder.Entity<ClienteXCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClienteXCuenta_pkey");

            entity.ToTable("ClienteXCuenta");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.ClienteId).HasColumnName("Cliente_ID");
            entity.Property(e => e.CuentaId).HasColumnName("Cuenta_ID");
            entity.Property(e => e.Rol)
                .HasMaxLength(45)
                .HasColumnName("rol");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ClienteXCuenta)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.ClienteXCuenta)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuenta");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cuenta_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BancoId).HasColumnName("Banco_ID");
            entity.Property(e => e.Cbu).HasColumnName("cbu");
            entity.Property(e => e.NumeroCuenta).HasColumnName("numero_cuenta");
            entity.Property(e => e.TipoCuentaId).HasColumnName("TipoCuenta_ID");

            entity.HasOne(d => d.Banco).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.BancoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_banco");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TipoCuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipocuenta");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Empleado_pkey");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CuitCuil)
                .HasMaxLength(10)
                .HasColumnName("cuit_cuil");
            entity.Property(e => e.Legajo).HasColumnName("legajo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_ID");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("fk_sucursal");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Localidad_pkey");

            entity.ToTable("Localidad");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cp)
                .HasMaxLength(10)
                .HasColumnName("cp");
            entity.Property(e => e.ProvinciaId).HasColumnName("Provincia_ID");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Localidades)
                .HasForeignKey(d => d.ProvinciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provincia");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pais_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Provincia_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.PaisId).HasColumnName("Pais_ID");

            entity.HasOne(d => d.Pais).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pais");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sucursal_pkey");

            entity.ToTable("Sucursal");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Calle)
                .HasMaxLength(45)
                .HasColumnName("calle");
            entity.Property(e => e.Cp)
                .HasMaxLength(10)
                .HasColumnName("cp");
            entity.Property(e => e.CuentaId).HasColumnName("Cuenta_ID");
            entity.Property(e => e.Departamento)
                .HasMaxLength(45)
                .HasColumnName("departamento");
            entity.Property(e => e.LocalidadId).HasColumnName("Localidad_ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasMaxLength(45)
                .HasColumnName("numero");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("fk_cuenta");

            entity.HasOne(d => d.Localidad).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.LocalidadId)
                .HasConstraintName("fk_localidad");
        });

        modelBuilder.Entity<TipoCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoCuenta_pkey");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoTransaccion_pkey");

            entity.ToTable("TipoTransaccion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transaccion_pkey");

            entity.ToTable("Transaccion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acreditacion).HasColumnName("acreditacion");
            entity.Property(e => e.CuentaDestinoId).HasColumnName("CuentaDestino_ID");
            entity.Property(e => e.CuentaOrigenId).HasColumnName("CuentaOrigen_ID");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.Motivo)
                .HasMaxLength(45)
                .HasColumnName("motivo");
            entity.Property(e => e.NumeroOperacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("numero_operacion");
            entity.Property(e => e.Realizacion).HasColumnName("realizacion");
            entity.Property(e => e.Referencia)
                .HasMaxLength(45)
                .HasColumnName("referencia");
            entity.Property(e => e.TipoTransaccionId).HasColumnName("TipoTransaccion_ID");

            entity.HasOne(d => d.CuentaDestino).WithMany(p => p.TransaccionCuentaDestinos)
                .HasForeignKey(d => d.CuentaDestinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuentaDestino");

            entity.HasOne(d => d.CuentaOrigen).WithMany(p => p.TransaccionCuentaOrigens)
                .HasForeignKey(d => d.CuentaOrigenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuentaOrigen");

            entity.HasOne(d => d.TipoTransaccion).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.TipoTransaccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipoTransaccion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
