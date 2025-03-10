using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace v1.Migrations
{
    /// <inheritdoc />
    public partial class AddConfirmationAndResetColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "tbl_usuario",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "tbl_usuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "tbl_usuario",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTokenExpiry",
                table: "tbl_usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "tbl_usuario");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "tbl_usuario");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "tbl_usuario");

            migrationBuilder.DropColumn(
                name: "ResetPasswordTokenExpiry",
                table: "tbl_usuario");
        }
        /*
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_categoria",
                columns: table => new
                {
                    catg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catg_nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    catg_estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_cate__DC199F4DC41B276E", x => x.catg_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_empresa",
                columns: table => new
                {
                    empr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empr_nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    empr_RUC = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    empr_telefono = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    empr_correo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    empr_direccion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    empr_razon_social = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    empr_estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_empr__C8617A92A78A3115", x => x.empr_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_especialidad",
                columns: table => new
                {
                    espc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    espc_nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_espe__CA84D258CB524DF9", x => x.espc_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_estado",
                columns: table => new
                {
                    estd_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estd_nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    estd_estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_esta__63A3F55014F64B0D", x => x.estd_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_rol",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol_nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    rol_estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_rol__CF32E443EFDD0961", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_usuario",
                columns: table => new
                {
                    usur_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usur_cedula = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    usur_corrreo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    usur_clave = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    usur_nombre = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    usur_apellido = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    usur_estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    espc_id = table.Column<int>(type: "int", nullable: false),
                    ConfirmationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_usua__8DD109A8F416F11B", x => x.usur_id);
                    table.ForeignKey(
                        name: "tbl_especialidad_espc_id_fk",
                        column: x => x.espc_id,
                        principalTable: "tbl_especialidad",
                        principalColumn: "espc_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_ticket",
                columns: table => new
                {
                    tick_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tick_descripcion = table.Column<string>(type: "text", nullable: true),
                    tick_prioridad = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    tick_fech_creacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    tick_fech_cierre = table.Column<DateTime>(type: "datetime", nullable: false),
                    tick_comentario = table.Column<string>(type: "text", nullable: true),
                    catg_id = table.Column<int>(type: "int", nullable: false),
                    estd_id = table.Column<int>(type: "int", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    tecnico_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_tick__26D5005EA36509B1", x => x.tick_id);
                    table.ForeignKey(
                        name: "tbl_estado_estd_id_fk",
                        column: x => x.estd_id,
                        principalTable: "tbl_estado",
                        principalColumn: "estd_id");
                    table.ForeignKey(
                        name: "tbl_tickests_catg_id_fk",
                        column: x => x.catg_id,
                        principalTable: "tbl_categoria",
                        principalColumn: "catg_id");
                    table.ForeignKey(
                        name: "tbl_usuario_tick_id_fk",
                        column: x => x.usuario_id,
                        principalTable: "tbl_usuario",
                        principalColumn: "usur_id");
                    table.ForeignKey(
                        name: "tbl_usuario_usur_tecn_fk",
                        column: x => x.tecnico_id,
                        principalTable: "tbl_usuario",
                        principalColumn: "usur_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_usuario_empr",
                columns: table => new
                {
                    usur_empr_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usur_id = table.Column<int>(type: "int", nullable: false),
                    empr_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_usua__6F67CCCAE65BBF57", x => x.usur_empr_id);
                    table.ForeignKey(
                        name: "tbl_empresa_empr_id_fk",
                        column: x => x.empr_id,
                        principalTable: "tbl_empresa",
                        principalColumn: "empr_id");
                    table.ForeignKey(
                        name: "tbl_usuario_empr_id_fk",
                        column: x => x.usur_id,
                        principalTable: "tbl_usuario",
                        principalColumn: "usur_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_usuario_rol",
                columns: table => new
                {
                    usur_rol_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usur_id = table.Column<int>(type: "int", nullable: false),
                    rol_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_usua__BA8DE7A6E3FCDED5", x => x.usur_rol_id);
                    table.ForeignKey(
                        name: "tbl_rol_rol_id_fk",
                        column: x => x.rol_id,
                        principalTable: "tbl_rol",
                        principalColumn: "rol_id");
                    table.ForeignKey(
                        name: "tbl_usuario_usur_id_fk",
                        column: x => x.usur_id,
                        principalTable: "tbl_usuario",
                        principalColumn: "usur_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ticket_catg_id",
                table: "tbl_ticket",
                column: "catg_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ticket_estd_id",
                table: "tbl_ticket",
                column: "estd_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ticket_tecnico_id",
                table: "tbl_ticket",
                column: "tecnico_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ticket_usuario_id",
                table: "tbl_ticket",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_usuario_espc_id",
                table: "tbl_usuario",
                column: "espc_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_usuario_empr_empr_id",
                table: "tbl_usuario_empr",
                column: "empr_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_usuario_empr_usur_id",
                table: "tbl_usuario_empr",
                column: "usur_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_usuario_rol_rol_id",
                table: "tbl_usuario_rol",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_usuario_rol_usur_id",
                table: "tbl_usuario_rol",
                column: "usur_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ticket");

            migrationBuilder.DropTable(
                name: "tbl_usuario_empr");

            migrationBuilder.DropTable(
                name: "tbl_usuario_rol");

            migrationBuilder.DropTable(
                name: "tbl_estado");

            migrationBuilder.DropTable(
                name: "tbl_categoria");

            migrationBuilder.DropTable(
                name: "tbl_empresa");

            migrationBuilder.DropTable(
                name: "tbl_rol");

            migrationBuilder.DropTable(
                name: "tbl_usuario");

            migrationBuilder.DropTable(
                name: "tbl_especialidad");
        }
        */
    }
}
