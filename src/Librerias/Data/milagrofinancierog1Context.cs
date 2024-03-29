﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class milagrofinancierog1Context : DbContext
{
    public milagrofinancierog1Context(DbContextOptions<milagrofinancierog1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Banco> Banco { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<ClienteCuenta> ClienteCuenta { get; set; }

    public virtual DbSet<Contacto> Contacto { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Empleado> Empleado { get; set; }

    public virtual DbSet<Localidad> Localidad { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Sucursal> Sucursal { get; set; }

    public virtual DbSet<TipoCuenta> TipoCuenta { get; set; }

    public virtual DbSet<TipoMotivo> TipoMotivo { get; set; }

    public virtual DbSet<TipoTransaccion> TipoTransaccion { get; set; }

    public virtual DbSet<Transaccion> Transaccion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Banco_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Banco_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cliente_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Cliente_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.Calle)
                .HasMaxLength(45)
                .HasColumnName("calle");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.CuitCuil)
                .IsRequired()
                .HasMaxLength(13)
                .HasColumnName("cuitCuil");
            entity.Property(e => e.Departamento)
                .HasMaxLength(45)
                .HasColumnName("departamento");
            entity.Property(e => e.IdLocalidad).HasColumnName("idLocalidad");
            entity.Property(e => e.AlturaCalle)
                .HasMaxLength(45)
                .HasColumnName("alturaCalle");
            entity.Property(e => e.Nombre)
               .IsRequired()
               .HasMaxLength(30)
               .HasColumnName("nombre");
            entity.Property(e => e.Apellido)
                            .IsRequired()
                            .HasMaxLength(30)
                            .HasColumnName("apellido");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("username");

            entity.HasOne(d => d.Localidad).WithMany(p => p.Cliente)
                .HasForeignKey(d => d.IdLocalidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_localidad");
        });

        modelBuilder.Entity<ClienteCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClienteCuenta_pkey");

            entity.ToTable("ClienteCuenta");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"ClienteCuenta_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Alta).HasColumnName("alta");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");
            entity.Property(e => e.Titular)
            .HasDefaultValue(true)
            .HasColumnName("titular");


            entity.HasOne(d => d.Cliente).WithMany(p => p.ClienteCuenta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.ClienteCuenta)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuenta");
        });

        modelBuilder.Entity<Contacto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Contacto_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cbu)
                .IsRequired()
                .HasMaxLength(22)
                .HasColumnName("cbu");
            entity.Property(e => e.IdBanco).HasColumnName("idBanco");
            entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.HasOne(d => d.Banco).WithMany(p => p.Contacto)
                .HasForeignKey(d => d.IdBanco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_banco");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Contacto)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuenta");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cuenta_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Cuenta_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Cbu)
                .IsRequired()
                .HasMaxLength(22)
                .HasColumnName("cbu");
            entity.Property(e => e.IdBanco).HasColumnName("idBanco");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.IdTipoCuenta).HasColumnName("idTipoCuenta");
            entity.Property(e => e.Numero).HasColumnName("numero");

            entity.HasOne(d => d.Banco).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdBanco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_banco");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sucursal");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdTipoCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipocuenta");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Empleado_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Empleado_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.CuitCuil)
                .IsRequired()
                .HasMaxLength(13)
                .HasColumnName("cuitCuil");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Legajo).HasColumnName("legajo");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Empleado)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("fk_sucursal");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Localidad_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Localidad_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.CodigoPostal)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("codigoPostal");
            entity.Property(e => e.IdProvincia).HasColumnName("idProvincia");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Localidad)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provincia");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pais_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Pais_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Provincia_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Provincia_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.IdPais).HasColumnName("idPais");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Pais).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pais");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sucursal_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Sucursal_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Calle)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("calle");
            entity.Property(e => e.Departamento)
                .HasMaxLength(45)
                .HasColumnName("departamento");
            entity.Property(e => e.IdLocalidad).HasColumnName("idLocalidad");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("numero");

            entity.HasOne(d => d.Localidad).WithMany(p => p.Sucursal)
                .HasForeignKey(d => d.IdLocalidad)
                .HasConstraintName("fk_localidad");
        });

        modelBuilder.Entity<TipoCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoCuenta_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"TipoCuenta_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoMotivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoMotivo_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoTransaccion_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"TipoTransaccion_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Transaccion_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Transaccion_ID_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.IdCuentaDestino).HasColumnName("idCuentaDestino");
            entity.Property(e => e.IdCuentaOrigen).HasColumnName("idCuentaOrigen");
            entity.Property(e => e.IdTipoMotivo).HasColumnName("idTipoMotivo");
            entity.Property(e => e.IdTipoTransaccion).HasColumnName("idTipoTransaccion");
            entity.Property(e => e.Monto).HasColumnName("monto");
            entity.Property(e => e.Numero)
                .HasDefaultValueSql("nextval('\"Transaccion_numero_operacion_seq\"'::regclass)")
                .HasColumnName("numero");
            entity.Property(e => e.Realizacion).HasColumnName("realizacion");
            entity.Property(e => e.Referencia)
                .HasMaxLength(45)
                .HasColumnName("referencia");

            entity.HasOne(d => d.CuentaDestino).WithMany(p => p.TransaccionCuentaDestinos)
                .HasForeignKey(d => d.IdCuentaDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuentaDestino");

            entity.HasOne(d => d.CuentaOrigen).WithMany(p => p.TransaccionCuentaOrigenes)
                .HasForeignKey(d => d.IdCuentaOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cuentaOrigen");

            entity.HasOne(d => d.TipoMotivo).WithMany(p => p.Transaccion)
                .HasForeignKey(d => d.IdTipoMotivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipoMotivo");

            entity.HasOne(d => d.TipoTransaccion).WithMany(p => p.Transaccion)
                .HasForeignKey(d => d.IdTipoTransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipoTransaccion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}