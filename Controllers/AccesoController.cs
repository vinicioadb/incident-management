using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using v1.Data;
using BCrypt.Net;
using v1.Models.ViewModels;
using v1.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace v1.Controllers
{
    public class AccesoController : Controller
    {
        private readonly v1DBContext _context;
        private readonly IEmailService _emailService;

        public AccesoController (v1DBContext context, IEmailService emailServive)
        {
            _context = context;
            _emailService = emailServive;
        }

        [HttpGet]
        public IActionResult Ingresar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ingresar(IngresarViewModel model)
        {

            if (ModelState.IsValid)
            {
                string sessionKey = $"numeroDeIntentos_{model.UsurCorreo}";
                int intentosFallidos = HttpContext.Session.GetInt32(sessionKey) ?? 0;

                if (intentosFallidos >= 3)
                {
                    ViewData["ErrorIngreso"] = "Has excedido el número máximo de intentos. Inténtalo de nuevo más tarde.";
                    return View(model);
                }

                var usuario = await _context.tbl_usuarios
                    .Include(u => u.tbl_usuario_rols)
                    .ThenInclude(ur => ur.rol)
                    .FirstOrDefaultAsync(u => u.usur_corrreo == model.UsurCorreo);


                if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.UsurClave, usuario.usur_clave))
                {
                    // Incrementar contador de intentos
                    intentosFallidos++;
                    HttpContext.Session.SetInt32(sessionKey, intentosFallidos);

                    if (intentosFallidos >= 3)
                    {
                        ViewData["ErrorIngreso"] = "Has excedido el número máximo de intentos. Inténtalo de nuevo más tarde.";
                        //ModelState.AddModelError(string.Empty, "Has excedido el número máximo de intentos. Inténtalo de nuevo más tarde.");
                    }
                    else
                    {
                        /*ModelState.AddModelError(nameof(model.UsurCorreo), " ");*/
                        ViewData["ErrorIngreso"] = $"Credenciales Incorrectas tienes {3 - intentosFallidos} intentos restantes";
                    }

                    return View(model);
                }

                HttpContext.Session.Remove(sessionKey);        
                var rol = usuario.tbl_usuario_rols.FirstOrDefault()?.rol.rol_nombre;

                //HttpContext.Session.SetString("usuario_rol", rol);
                /*
                HttpContext.Session.SetString("usuario_nombre", usuario.usur_nombre);
                HttpContext.Session.SetString("usuario_apellido", usuario.usur_apellido);
                */
                
                ViewData["usuario_id"] = usuario.usur_id;
                // Crear claims para autenticación
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.usur_id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.usur_nombre),
                    new Claim(ClaimTypes.Surname, usuario.usur_apellido),
                    new Claim(ClaimTypes.Email, usuario.usur_corrreo),
                    new Claim(ClaimTypes.Role, rol ?? "Cliente") // Por defecto, asignar "Usuario" si no tiene rol
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                // Registrar cookie de autenticación
                await HttpContext.SignInAsync("CookieAuth", principal);

                if (rol == "Administrador")
                {
                    //HttpContext.Session.SetString("Layout", "_AdminLayout");
                    Console.WriteLine(ViewData["usuario_id"]);
                    return RedirectToAction("Index", "Administrador");
                }
                else if (rol == "Tecnico")
                {
                    return RedirectToAction("Index", "Tecnico");
                }
                else
                {
                    return RedirectToAction("Index", "Cliente");
                }

            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Registrar(string email)
        {
           
            var roles = await ObtenerRoles();

            var model = new RegistrarViewModel
            {
                Roles = roles,
                UsurCorreo = email
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya existe
                var existeUsuario = await _context.tbl_usuarios
                    .AnyAsync(u => u.usur_corrreo == model.UsurCorreo);
                
                var existeCedula = await _context.tbl_usuarios
                    .AnyAsync(u => u.usur_cedula == model.UsurCedula);

                if (existeUsuario)
                {   
                    ModelState.AddModelError("UsurCorreo", "El correo ya está registrado.");
                    model.Roles = await ObtenerRoles();
                    return View(model);
                }

                if (existeCedula)
                {
                    ModelState.AddModelError("UsurCedula", "La cedula ya está registrado.");
                    model.Roles = await ObtenerRoles();
                    return View(model);
                }

                // Crear una nueva instancia de Usuario
                var nuevoUsuario = new tbl_usuario
                {
                    usur_cedula = model.UsurCedula,
                    usur_corrreo = model.UsurCorreo,
                    usur_nombre = model.UsurNombre,
                    usur_apellido = model.UsurApellido,
                    usur_clave = EncriptarContraseña(model.UsurClave),
                    espc_id = 1,
                    usur_estado = 'A'
                };

                _context.tbl_usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                // Asociar usuario con el rol seleccionado
                var usuarioRol = new tbl_usuario_rol
                {
                    usur_id = nuevoUsuario.usur_id,
                    rol_id = model.RolId
                };

                _context.tbl_usuario_rols.Add(usuarioRol);
                await _context.SaveChangesAsync();

                ViewData["RegistroExitoso"] = "Registro exitoso!";
                return View("Ingresar");
            }
            /*
            // Si el modelo no es válido, repoblar roles
            model.Roles = await _context.tbl_rols
                .Where(r => r.rol_estado == 'A')
                .Select(r => new SelectListItem
                {
                    Value = r.rol_id.ToString(),
                    Text = r.rol_nombre
                })
                .ToListAsync();

            return View(model);
            */
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Ingresar", "Acceso");
        }

        [HttpGet]
        public IActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Recuperar(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.tbl_usuarios.FirstOrDefaultAsync(u => u.usur_corrreo == model.Email);

                if (usuario == null)
                {
                    // No revelar que el correo no existe
                    ViewData["EnlaceReuperacion"] = "Se ha enviado un enlace de recuperación de contraseña a tu correo electrónico. Por favor, revisa tu bandeja de entrada.";
                    return View("Ingresar");
                }

                // Generar código de recuperación
                var codigoRecuperacion = GenerateResetCode();
                usuario.ResetPasswordToken = codigoRecuperacion;
                usuario.ResetPasswordTokenExpiry = DateTime.Now.AddMinutes(10);
                await _context.SaveChangesAsync();

                // Enviar correo de recuperación
                await _emailService.SendEmailAsync(usuario.usur_corrreo, "Código de Recuperación", $"Tu código de recuperación es: {codigoRecuperacion}", $"{usuario.usur_nombre} {usuario.usur_apellido}");

                return RedirectToAction("VerificarEmail", new { email = usuario.usur_corrreo });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult VerificarEmail(string email)
        {
            var model = new ResetPasswordCodeViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerificarEmail(ResetPasswordCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.tbl_usuarios.FirstOrDefaultAsync(u => u.usur_corrreo == model.Email && u.ResetPasswordToken == model.Code);

                if (usuario == null || usuario.ResetPasswordTokenExpiry < DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "Código de recuperación inválido o expirado.");
                    return View(model);
                }

                // Generar un token de restablecimiento único
                var resetToken = Guid.NewGuid().ToString();
                usuario.ResetPasswordToken = resetToken;
                usuario.ResetPasswordTokenExpiry = DateTime.Now.AddMinutes(10);
                await _context.SaveChangesAsync();

                return RedirectToAction("RestablecerContrasena", new { token = resetToken });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RestablecerContrasena(string token)
        {
            //var model = new ResetPasswordViewModel { Email = email };
            //return View(model);
            var usuario = await _context.tbl_usuarios.FirstOrDefaultAsync(u => u.ResetPasswordToken == token);
            
            if (usuario == null || usuario.ResetPasswordTokenExpiry < DateTime.Now)
            {
                return View("Error");
            }

            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestablecerContrasena(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.tbl_usuarios.FirstOrDefaultAsync(u => u.ResetPasswordToken == model.Token);

                if (usuario == null || usuario.ResetPasswordTokenExpiry < DateTime.UtcNow)
                {
                    return View("Error");
                }

                usuario.usur_clave = EncriptarContraseña(model.Password);
                usuario.ResetPasswordToken = null;
                usuario.ResetPasswordTokenExpiry = null;
                await _context.SaveChangesAsync();

                ViewData["RecuperacionExitosa"] = "Has recuperado tu contraseña. Ahora inicia sesión";
                return View("Ingresar");
                //return RedirectToAction("ResetPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RegistrarEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarEmail(RegisterEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya existe
                var existeUsuario = await _context.tbl_usuarios
                    .AnyAsync(u => u.usur_corrreo == model.Email);

                if (existeUsuario)
                {
                    ModelState.AddModelError("Email", "El correo ya está registrado.");
                    return View(model);
                }

                // Generar código de confirmación
                var confirmationCode = GenerateConfirmationCode();

                // Guardar el correo y el código de confirmación en la sesión
                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetString("ConfirmationCode", confirmationCode);

                // Enviar correo de confirmación
                await _emailService.SendEmailAsync(model.Email, "Código de Confirmación", $"Tu código de confirmación es: {confirmationCode}", $"{model.Email}");

                return RedirectToAction("ConfirmarEmail");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ConfirmarEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarEmail(ConfirmEmailCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = HttpContext.Session.GetString("Email");
                var confirmationCode = HttpContext.Session.GetString("ConfirmationCode");

                if (model.Code == confirmationCode)
                {
                    // Limpiar el código de confirmación de la sesión
                    HttpContext.Session.Remove("ConfirmationCode");

                    // Redirigir al formulario de registro completo
                    return RedirectToAction("Registrar", new { email = email });
                }

                ModelState.AddModelError("Code", "Código de confirmación inválido.");
            }

            return View(model);
        }

        
        #region Utilidades
            
            private string EncriptarContraseña(string contraseña)
            {
                // Usar BCrypt para generar un hash seguro
                return BCrypt.Net.BCrypt.HashPassword(contraseña);
            }

            public async Task<List<SelectListItem>> ObtenerRoles()
            {
                var roles = await _context.tbl_rols
                .Where(r => r.rol_estado == 'A') // Solo roles activos
                .Select(r => new SelectListItem
                {
                    Value = r.rol_id.ToString(),
                    Text = r.rol_nombre
                })
                .ToListAsync();
                
                return roles;
            }
            
            public async Task<List<SelectListItem>> ObtenerEstados()
            {
                var estados = await _context.tbl_estados
                .Where(e => e.estd_estado == 'A'.ToString())
                .Select(e => new SelectListItem
                {
                    Value = e.estd_id.ToString(),
                    Text = e.estd_nombre

                })
                .ToListAsync();

                return estados;
            }

            public async Task<List<SelectListItem>> ObtenerCategorias()
            {
                var categorias = await _context.tbl_categoria
                .Where(c => c.catg_estado == 'A'.ToString())
                .Select(c => new SelectListItem
                {
                    Value = c.catg_id.ToString(),
                    Text = c.catg_nombre

                })
                .ToListAsync();

                return categorias;
            }

            private string GenerateConfirmationCode()
            {
                return new Random().Next(100000, 999999).ToString();
            }

            private string GenerateResetCode()
            {
                return new Random().Next(100000, 999999).ToString();
            }
            
        #endregion
    }
}
