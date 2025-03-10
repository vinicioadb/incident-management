using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using v1.Models;

namespace v1.Data;

public partial class v1DBContext : DbContext
{
    private readonly IConfiguration _configuration;
    public v1DBContext()
    {
    }

    public v1DBContext(DbContextOptions<v1DBContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<tbl_categorium> tbl_categoria { get; set; }

    public virtual DbSet<tbl_empresa> tbl_empresas { get; set; }

    public virtual DbSet<tbl_especialidad> tbl_especialidads { get; set; }

    public virtual DbSet<tbl_estado> tbl_estados { get; set; }

    public virtual DbSet<tbl_rol> tbl_rols { get; set; }

    public virtual DbSet<tbl_ticket> tbl_tickets { get; set; }

    public virtual DbSet<tbl_usuario> tbl_usuarios { get; set; }

    public virtual DbSet<tbl_usuario_empr> tbl_usuario_emprs { get; set; }

    public virtual DbSet<tbl_usuario_rol> tbl_usuario_rols { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=FERNANDO; Database=v1-DB; Trusted_Connection=True; Encrypt=False");
    */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("v1-DB-ConnectionString"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tbl_categorium>(entity =>
        {
            entity.HasKey(e => e.catg_id).HasName("PK__tbl_cate__DC199F4DC41B276E");

            entity.Property(e => e.catg_estado).IsFixedLength();
        });

        modelBuilder.Entity<tbl_empresa>(entity =>
        {
            entity.HasKey(e => e.empr_id).HasName("PK__tbl_empr__C8617A92A78A3115");

            entity.Property(e => e.empr_estado).IsFixedLength();
        });

        modelBuilder.Entity<tbl_especialidad>(entity =>
        {
            entity.HasKey(e => e.espc_id).HasName("PK__tbl_espe__CA84D258CB524DF9");
        });

        modelBuilder.Entity<tbl_estado>(entity =>
        {
            entity.HasKey(e => e.estd_id).HasName("PK__tbl_esta__63A3F55014F64B0D");

            entity.Property(e => e.estd_estado).IsFixedLength();
        });

        modelBuilder.Entity<tbl_rol>(entity =>
        {
            entity.HasKey(e => e.rol_id).HasName("PK__tbl_rol__CF32E443EFDD0961");

            entity.Property(e => e.rol_estado).IsFixedLength();
        });

        modelBuilder.Entity<tbl_ticket>(entity =>
        {
            entity.HasKey(e => e.tick_id).HasName("PK__tbl_tick__26D5005EA36509B1");

            entity.HasOne(d => d.catg).WithMany(p => p.tbl_tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_tickests_catg_id_fk");

            entity.HasOne(d => d.estd).WithMany(p => p.tbl_tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_estado_estd_id_fk");

            entity.HasOne(d => d.tecnico).WithMany(p => p.tbl_tickettecnicos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_usuario_usur_tecn_fk");

            entity.HasOne(d => d.usuario).WithMany(p => p.tbl_ticketusuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_usuario_tick_id_fk");
        });

        modelBuilder.Entity<tbl_usuario>(entity =>
        {
            entity.HasKey(e => e.usur_id).HasName("PK__tbl_usua__8DD109A8F416F11B");

            entity.Property(e => e.usur_estado).IsFixedLength();

            entity.HasOne(d => d.espc).WithMany(p => p.tbl_usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_especialidad_espc_id_fk");
        });

        modelBuilder.Entity<tbl_usuario_empr>(entity =>
        {
            entity.HasKey(e => e.usur_empr_id).HasName("PK__tbl_usua__6F67CCCAE65BBF57");

            entity.HasOne(d => d.empr).WithMany(p => p.tbl_usuario_emprs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_empresa_empr_id_fk");

            entity.HasOne(d => d.usur).WithMany(p => p.tbl_usuario_emprs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_usuario_empr_id_fk");
        });

        modelBuilder.Entity<tbl_usuario_rol>(entity =>
        {
            entity.HasKey(e => e.usur_rol_id).HasName("PK__tbl_usua__BA8DE7A6E3FCDED5");

            entity.HasOne(d => d.rol).WithMany(p => p.tbl_usuario_rols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_rol_rol_id_fk");

            entity.HasOne(d => d.usur).WithMany(p => p.tbl_usuario_rols)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_usuario_usur_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
