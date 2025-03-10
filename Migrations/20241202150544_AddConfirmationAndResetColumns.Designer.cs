﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using v1.Data;

#nullable disable

namespace v1.Migrations
{
    [DbContext(typeof(v1DBContext))]
    [Migration("20241202150544_AddConfirmationAndResetColumns")]
    partial class AddConfirmationAndResetColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("v1.Models.tbl_categorium", b =>
                {
                    b.Property<int>("catg_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("catg_id"));

                    b.Property<string>("catg_estado")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("catg_nombre")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("catg_id")
                        .HasName("PK__tbl_cate__DC199F4DC41B276E");

                    b.ToTable("tbl_categoria");
                });

            modelBuilder.Entity("v1.Models.tbl_empresa", b =>
                {
                    b.Property<int>("empr_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("empr_id"));

                    b.Property<string>("empr_RUC")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("empr_correo")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("empr_direccion")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("empr_estado")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("empr_nombre")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("empr_razon_social")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("empr_telefono")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("empr_id")
                        .HasName("PK__tbl_empr__C8617A92A78A3115");

                    b.ToTable("tbl_empresa");
                });

            modelBuilder.Entity("v1.Models.tbl_especialidad", b =>
                {
                    b.Property<int>("espc_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("espc_id"));

                    b.Property<string>("espc_nombre")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("espc_id")
                        .HasName("PK__tbl_espe__CA84D258CB524DF9");

                    b.ToTable("tbl_especialidad");
                });

            modelBuilder.Entity("v1.Models.tbl_estado", b =>
                {
                    b.Property<int>("estd_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("estd_id"));

                    b.Property<string>("estd_estado")
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("estd_nombre")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("estd_id")
                        .HasName("PK__tbl_esta__63A3F55014F64B0D");

                    b.ToTable("tbl_estado");
                });

            modelBuilder.Entity("v1.Models.tbl_rol", b =>
                {
                    b.Property<int>("rol_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("rol_id"));

                    b.Property<string>("rol_estado")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("rol_nombre")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("rol_id")
                        .HasName("PK__tbl_rol__CF32E443EFDD0961");

                    b.ToTable("tbl_rol");
                });

            modelBuilder.Entity("v1.Models.tbl_ticket", b =>
                {
                    b.Property<int>("tick_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tick_id"));

                    b.Property<int>("catg_id")
                        .HasColumnType("int");

                    b.Property<int>("estd_id")
                        .HasColumnType("int");

                    b.Property<int>("tecnico_id")
                        .HasColumnType("int");

                    b.Property<string>("tick_comentario")
                        .HasColumnType("text");

                    b.Property<string>("tick_descripcion")
                        .HasColumnType("text");

                    b.Property<DateTime>("tick_fech_cierre")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("tick_fech_creacion")
                        .HasColumnType("datetime");

                    b.Property<string>("tick_prioridad")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("usuario_id")
                        .HasColumnType("int");

                    b.HasKey("tick_id")
                        .HasName("PK__tbl_tick__26D5005EA36509B1");

                    b.HasIndex("catg_id");

                    b.HasIndex("estd_id");

                    b.HasIndex("tecnico_id");

                    b.HasIndex("usuario_id");

                    b.ToTable("tbl_ticket");
                });

            modelBuilder.Entity("v1.Models.tbl_usuario", b =>
                {
                    b.Property<int>("usur_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("usur_id"));

                    b.Property<string>("ConfirmationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetPasswordTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<int>("espc_id")
                        .HasColumnType("int");

                    b.Property<string>("usur_apellido")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("usur_cedula")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("usur_clave")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("usur_corrreo")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("usur_estado")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("usur_nombre")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.HasKey("usur_id")
                        .HasName("PK__tbl_usua__8DD109A8F416F11B");

                    b.HasIndex("espc_id");

                    b.ToTable("tbl_usuario");
                });

            modelBuilder.Entity("v1.Models.tbl_usuario_empr", b =>
                {
                    b.Property<int>("usur_empr_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("usur_empr_id"));

                    b.Property<int>("empr_id")
                        .HasColumnType("int");

                    b.Property<int>("usur_id")
                        .HasColumnType("int");

                    b.HasKey("usur_empr_id")
                        .HasName("PK__tbl_usua__6F67CCCAE65BBF57");

                    b.HasIndex("empr_id");

                    b.HasIndex("usur_id");

                    b.ToTable("tbl_usuario_empr");
                });

            modelBuilder.Entity("v1.Models.tbl_usuario_rol", b =>
                {
                    b.Property<int>("usur_rol_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("usur_rol_id"));

                    b.Property<int>("rol_id")
                        .HasColumnType("int");

                    b.Property<int>("usur_id")
                        .HasColumnType("int");

                    b.HasKey("usur_rol_id")
                        .HasName("PK__tbl_usua__BA8DE7A6E3FCDED5");

                    b.HasIndex("rol_id");

                    b.HasIndex("usur_id");

                    b.ToTable("tbl_usuario_rol");
                });

            modelBuilder.Entity("v1.Models.tbl_ticket", b =>
                {
                    b.HasOne("v1.Models.tbl_categorium", "catg")
                        .WithMany("tbl_tickets")
                        .HasForeignKey("catg_id")
                        .IsRequired()
                        .HasConstraintName("tbl_tickests_catg_id_fk");

                    b.HasOne("v1.Models.tbl_estado", "estd")
                        .WithMany("tbl_tickets")
                        .HasForeignKey("estd_id")
                        .IsRequired()
                        .HasConstraintName("tbl_estado_estd_id_fk");

                    b.HasOne("v1.Models.tbl_usuario", "tecnico")
                        .WithMany("tbl_tickettecnicos")
                        .HasForeignKey("tecnico_id")
                        .IsRequired()
                        .HasConstraintName("tbl_usuario_usur_tecn_fk");

                    b.HasOne("v1.Models.tbl_usuario", "usuario")
                        .WithMany("tbl_ticketusuarios")
                        .HasForeignKey("usuario_id")
                        .IsRequired()
                        .HasConstraintName("tbl_usuario_tick_id_fk");

                    b.Navigation("catg");

                    b.Navigation("estd");

                    b.Navigation("tecnico");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("v1.Models.tbl_usuario", b =>
                {
                    b.HasOne("v1.Models.tbl_especialidad", "espc")
                        .WithMany("tbl_usuarios")
                        .HasForeignKey("espc_id")
                        .IsRequired()
                        .HasConstraintName("tbl_especialidad_espc_id_fk");

                    b.Navigation("espc");
                });

            modelBuilder.Entity("v1.Models.tbl_usuario_empr", b =>
                {
                    b.HasOne("v1.Models.tbl_empresa", "empr")
                        .WithMany("tbl_usuario_emprs")
                        .HasForeignKey("empr_id")
                        .IsRequired()
                        .HasConstraintName("tbl_empresa_empr_id_fk");

                    b.HasOne("v1.Models.tbl_usuario", "usur")
                        .WithMany("tbl_usuario_emprs")
                        .HasForeignKey("usur_id")
                        .IsRequired()
                        .HasConstraintName("tbl_usuario_empr_id_fk");

                    b.Navigation("empr");

                    b.Navigation("usur");
                });

            modelBuilder.Entity("v1.Models.tbl_usuario_rol", b =>
                {
                    b.HasOne("v1.Models.tbl_rol", "rol")
                        .WithMany("tbl_usuario_rols")
                        .HasForeignKey("rol_id")
                        .IsRequired()
                        .HasConstraintName("tbl_rol_rol_id_fk");

                    b.HasOne("v1.Models.tbl_usuario", "usur")
                        .WithMany("tbl_usuario_rols")
                        .HasForeignKey("usur_id")
                        .IsRequired()
                        .HasConstraintName("tbl_usuario_usur_id_fk");

                    b.Navigation("rol");

                    b.Navigation("usur");
                });

            modelBuilder.Entity("v1.Models.tbl_categorium", b =>
                {
                    b.Navigation("tbl_tickets");
                });

            modelBuilder.Entity("v1.Models.tbl_empresa", b =>
                {
                    b.Navigation("tbl_usuario_emprs");
                });

            modelBuilder.Entity("v1.Models.tbl_especialidad", b =>
                {
                    b.Navigation("tbl_usuarios");
                });

            modelBuilder.Entity("v1.Models.tbl_estado", b =>
                {
                    b.Navigation("tbl_tickets");
                });

            modelBuilder.Entity("v1.Models.tbl_rol", b =>
                {
                    b.Navigation("tbl_usuario_rols");
                });

            modelBuilder.Entity("v1.Models.tbl_usuario", b =>
                {
                    b.Navigation("tbl_tickettecnicos");

                    b.Navigation("tbl_ticketusuarios");

                    b.Navigation("tbl_usuario_emprs");

                    b.Navigation("tbl_usuario_rols");
                });
#pragma warning restore 612, 618
        }
    }
}
