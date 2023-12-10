using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend_milagrofinanciero.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banco",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Banco_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pais_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoCuenta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    alta = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TipoCuenta_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoTransaccion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TipoTransaccion_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Pais_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Provincia_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "fk_pais",
                        column: x => x.Pais_ID,
                        principalTable: "Pais",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero_cuenta = table.Column<long>(type: "bigint", nullable: false),
                    cbu = table.Column<long>(type: "bigint", nullable: false),
                    TipoCuenta_ID = table.Column<int>(type: "integer", nullable: false),
                    Banco_ID = table.Column<int>(type: "integer", nullable: false),
                    Sucursal_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Cuenta_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "fk_banco",
                        column: x => x.Banco_ID,
                        principalTable: "Banco",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_tipocuenta",
                        column: x => x.TipoCuenta_ID,
                        principalTable: "TipoCuenta",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_sucursal",
                        column: x => x.Sucursal_ID,
                        principalTable: "Sucursal",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Localidad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cp = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Provincia_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Localidad_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "fk_provincia",
                        column: x => x.Provincia_ID,
                        principalTable: "Provincia",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    monto = table.Column<float>(type: "real", nullable: false),
                    numero_operacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    acreditacion = table.Column<DateOnly>(type: "date", nullable: true),
                    realizacion = table.Column<DateOnly>(type: "date", nullable: true),
                    motivo = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    referencia = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    CuentaOrigen_ID = table.Column<int>(type: "integer", nullable: false),
                    CuentaDestino_ID = table.Column<int>(type: "integer", nullable: false),
                    TipoTransaccion_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Transaccion_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "fk_cuentaDestino",
                        column: x => x.CuentaDestino_ID,
                        principalTable: "Cuenta",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_cuentaOrigen",
                        column: x => x.CuentaOrigen_ID,
                        principalTable: "Cuenta",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_tipoTransaccion",
                        column: x => x.TipoTransaccion_ID,
                        principalTable: "TipoTransaccion",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    razon_social = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    cuit_cuil = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    alta = table.Column<DateOnly>(type: "date", nullable: true),
                    calle = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    departamento = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    numero = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Localidad_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Cliente_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "fk_localidad",
                        column: x => x.Localidad_ID,
                        principalTable: "Localidad",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    cp = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    calle = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    departamento = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    numero = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Localidad_ID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_localidad",
                        column: x => x.Localidad_ID,
                        principalTable: "Localidad",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ClienteXCuenta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    alta = table.Column<DateOnly>(type: "date", nullable: true),
                    Cliente_ID = table.Column<int>(type: "integer", nullable: false),
                    Cuenta_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ClienteXCuenta_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "fk_cliente",
                        column: x => x.Cliente_ID,
                        principalTable: "Cliente",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_cuenta",
                        column: x => x.Cuenta_ID,
                        principalTable: "Cuenta",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    cuit_cuil = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    legajo = table.Column<int>(type: "integer", nullable: false),
                    Sucursal_ID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Empleado_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "fk_sucursal",
                        column: x => x.Sucursal_ID,
                        principalTable: "Sucursal",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Localidad_ID",
                table: "Cliente",
                column: "Localidad_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteXCuenta_Cliente_ID",
                table: "ClienteXCuenta",
                column: "Cliente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteXCuenta_Cuenta_ID",
                table: "ClienteXCuenta",
                column: "Cuenta_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_Banco_ID",
                table: "Cuenta",
                column: "Banco_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_TipoCuenta_ID",
                table: "Cuenta",
                column: "TipoCuenta_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Sucursal_ID",
                table: "Empleado",
                column: "Sucursal_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_Provincia_ID",
                table: "Localidad",
                column: "Provincia_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Provincia_Pais_ID",
                table: "Provincia",
                column: "Pais_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_Sucursal_ID",
                table: "Cuenta",
                column: "Sucursal_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_Localidad_ID",
                table: "Sucursal",
                column: "Localidad_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_CuentaDestino_ID",
                table: "Transaccion",
                column: "CuentaDestino_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_CuentaOrigen_ID",
                table: "Transaccion",
                column: "CuentaOrigen_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_TipoTransaccion_ID",
                table: "Transaccion",
                column: "TipoTransaccion_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteXCuenta");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Transaccion");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "TipoTransaccion");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Localidad");

            migrationBuilder.DropTable(
                name: "Banco");

            migrationBuilder.DropTable(
                name: "TipoCuenta");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
