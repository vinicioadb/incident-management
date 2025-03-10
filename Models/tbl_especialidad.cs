using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_especialidad")]
public partial class tbl_especialidad
{
    [Key]
    public int espc_id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string espc_nombre { get; set; } = string.Empty;

    [InverseProperty("espc")]
    public virtual ICollection<tbl_usuario> tbl_usuarios { get; set; } = new List<tbl_usuario>();
}
