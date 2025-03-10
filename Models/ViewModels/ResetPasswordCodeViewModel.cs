using System;
using System.ComponentModel.DataAnnotations;

namespace v1.Models.ViewModels;

public class ResetPasswordCodeViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "El código es obligatorio.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Ingrese un código válido.")]
    public string Code { get; set; }
}