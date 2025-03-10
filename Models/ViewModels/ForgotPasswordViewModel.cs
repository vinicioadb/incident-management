using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace v1.Models.ViewModels;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
    public string Email { get; set; }
}
