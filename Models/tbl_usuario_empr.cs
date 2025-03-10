using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_usuario_empr")]
public partial class tbl_usuario_empr
{
    [Key]
    public int usur_empr_id { get; set; }

    public int usur_id { get; set; }

    public int empr_id { get; set; }

    [ForeignKey("empr_id")]
    [InverseProperty("tbl_usuario_emprs")]
    public virtual tbl_empresa empr { get; set; } = null!;

    [ForeignKey("usur_id")]
    [InverseProperty("tbl_usuario_emprs")]
    public virtual tbl_usuario usur { get; set; } = null!;
}
