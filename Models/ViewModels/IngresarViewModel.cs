using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace v1.Models.ViewModels;

public class IngresarViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
    public string UsurCorreo { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    public string UsurClave { get; set; }
}

