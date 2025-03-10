using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_rol")]
public partial class tbl_rol
{
    [Key]
    public int rol_id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string rol_nombre { get; set; } = string.Empty;

    [StringLength(1)]
    [Unicode(false)]
    public char rol_estado { get; set; }

    [InverseProperty("rol")]
    public virtual ICollection<tbl_usuario_rol> tbl_usuario_rols { get; set; } = new List<tbl_usuario_rol>();
}
