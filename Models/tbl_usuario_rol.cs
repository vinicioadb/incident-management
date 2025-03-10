using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_usuario_rol")]
public partial class tbl_usuario_rol
{
    [Key]
    public int usur_rol_id { get; set; }

    public int usur_id { get; set; }

    public int rol_id { get; set; }

    [ForeignKey("rol_id")]
    [InverseProperty("tbl_usuario_rols")]
    public virtual tbl_rol rol { get; set; } = null!;

    [ForeignKey("usur_id")]
    [InverseProperty("tbl_usuario_rols")]
    public virtual tbl_usuario usur { get; set; } = null!;
}
