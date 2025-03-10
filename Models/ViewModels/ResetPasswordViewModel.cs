using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace v1.Models.ViewModels;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; }

    public string Token { get; set; }
}
