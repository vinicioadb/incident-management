using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_ticket")]
public partial class tbl_ticket
{
    [Key]
    public int tick_id { get; set; }

    [Column(TypeName = "text")]
    public string tick_descripcion { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string tick_prioridad { get; set; } = string.Empty;

    [Column(TypeName = "datetime")]
    public DateTime tick_fech_creacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime tick_fech_cierre { get; set; }

    [Column(TypeName = "text")]
    public string tick_comentario { get; set; } = string.Empty;

    public int catg_id { get; set; }

    public int estd_id { get; set; }

    public int usuario_id { get; set; }

    public int tecnico_id { get; set; }

    [ForeignKey("catg_id")]
    [InverseProperty("tbl_tickets")]
    public virtual tbl_categorium catg { get; set; } = null!;

    [ForeignKey("estd_id")]
    [InverseProperty("tbl_tickets")]
    public virtual tbl_estado estd { get; set; } = null!;

    [ForeignKey("tecnico_id")]
    [InverseProperty("tbl_tickettecnicos")]
    public virtual tbl_usuario tecnico { get; set; } = null!;

    [ForeignKey("usuario_id")]
    [InverseProperty("tbl_ticketusuarios")]
    public virtual tbl_usuario usuario { get; set; } = null!;
}
