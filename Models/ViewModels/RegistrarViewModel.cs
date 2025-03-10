using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace v1.Models.ViewModels;

public class RegistrarViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string UsurNombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string UsurApellido { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
    public string UsurCorreo { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
    public string UsurClave { get; set; }

    [Required(ErrorMessage = "La cédula es obligatoria")]
    public string UsurCedula { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un rol")]
    public int RolId { get; set; }

    public IEnumerable<SelectListItem> Roles { get; set; }
}
