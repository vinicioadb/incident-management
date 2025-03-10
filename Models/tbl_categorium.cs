using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

public partial class tbl_categorium
{
    [Key]
    public int catg_id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string catg_nombre { get; set; } = string.Empty;

    [StringLength(1)]
    [Unicode(false)]
    public string catg_estado { get; set; } = string.Empty;

    [InverseProperty("catg")]
    public virtual ICollection<tbl_ticket> tbl_tickets { get; set; } = new List<tbl_ticket>();
}
