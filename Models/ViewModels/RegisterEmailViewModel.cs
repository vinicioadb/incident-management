using System;
using System.ComponentModel.DataAnnotations;

namespace v1.Models.ViewModels;

public class RegisterEmailViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
    public string Email { get; set; }
}
