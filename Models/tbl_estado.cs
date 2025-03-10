using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_estado")]
public partial class tbl_estado
{
    [Key]
    public int estd_id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string estd_nombre { get; set; } = string.Empty;

    [StringLength(1)]
    [Unicode(false)]
    public string estd_estado { get; set; } = string.Empty;

    [InverseProperty("estd")]
    public virtual ICollection<tbl_ticket> tbl_tickets { get; set; } = new List<tbl_ticket>();
}
