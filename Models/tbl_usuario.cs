using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v1.Models;

[Table("tbl_usuario")]
public partial class tbl_usuario
{
    [Key]
    public int usur_id { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string usur_cedula { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string usur_corrreo { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string usur_clave { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string usur_nombre { get; set; } = string.Empty;

    [StringLength(500)]
    [Unicode(false)]
    public string usur_apellido { get; set; } = string.Empty;

    [StringLength(1)]
    [Unicode(false)]
    public char usur_estado { get; set; } = 'A';

    public int espc_id { get; set; }

    public string ConfirmationToken { get; set; }
    public bool EmailConfirmed { get; set; }
    public string ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenExpiry { get; set; }

    [ForeignKey("espc_id")]
    [InverseProperty("tbl_usuarios")]
    public virtual tbl_especialidad espc { get; set; } = null!;

    [InverseProperty("tecnico")]
    public virtual ICollection<tbl_ticket> tbl_tickettecnicos { get; set; } = new List<tbl_ticket>();

    [InverseProperty("usuario")]
    public virtual ICollection<tbl_ticket> tbl_ticketusuarios { get; set; } = new List<tbl_ticket>();

    [InverseProperty("usur")]
    public virtual ICollection<tbl_usuario_empr> tbl_usuario_emprs { get; set; } = new List<tbl_usuario_empr>();

    [InverseProperty("usur")]
    public virtual ICollection<tbl_usuario_rol> tbl_usuario_rols { get; set; } = new List<tbl_usuario_rol>();
}
