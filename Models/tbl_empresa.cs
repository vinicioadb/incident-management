using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_empresa")]
public partial class tbl_empresa
{
    [Key]
    public int empr_id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string empr_nombre { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string empr_RUC { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string empr_telefono { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string empr_correo { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string empr_direccion { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string empr_razon_social { get; set; } = string.Empty;

    [StringLength(1)]
    [Unicode(false)]
    public string empr_estado { get; set; } = string.Empty;

    [InverseProperty("empr")]
    public virtual ICollection<tbl_usuario_empr> tbl_usuario_emprs { get; set; } = new List<tbl_usuario_empr>();
}
