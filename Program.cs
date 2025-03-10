using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using v1.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailService, EmailService>();
//builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<v1DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("v1-DB-ConnectionString")));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configuración de autenticación
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Acceso/Ingresar"; // Redirigir al login si no está autenticado
        options.LogoutPath = "/Acceso/Salir";
        options.AccessDeniedPath = "/Home/Ingresar"; // Redirigir si no tiene permisos
    });

//builder.Services.AddSession();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/* Correo Electronico */
public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message, string name);
}

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message, string name)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("DESK CORE - Correos", _smtpSettings.Username));
        emailMessage.To.Add(new MailboxAddress(name, email));
        emailMessage.Subject = subject;

        // Diseño del cuerpo del correo
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #fff;
                            padding: 20px;
                            border: 1px solid #ddd;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background-color: #007bff;
                            color: #fff;
                            padding: 10px 0;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                        }}
                        .footer {{
                            background-color: #f4f4f4;
                            padding: 10px 0;
                            text-align: center;
                            color: #777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Desk Core</h1>
                        </div>
                        <div class='content'>
                            <p>{message}</p>
                        </div>
                        <div class='footer'>
                            <p>Este correo fue enviado desde Desk Core.</p>
                        </div>
                    </div>
                </body>
                </html>"
        };

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
/*/
public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message, string name)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Desk COre", _smtpSettings.Username));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
*/

/*
public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message, string name)
    {
        var emailMessage = new MimeMessage();
     
        emailMessage.From.Add(new MailboxAddress("Desk COre", _smtpSettings.Username));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var client = new SmtpClient())
        {
            client.IsSecure = true;

            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
*/

public class SmtpSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}


/* SendGrid
public class EmailService : IEmailService
{
    private readonly SendGridSettings _sendGridSettings;

    public EmailService(IOptions<SendGridSettings> sendGridSettings)
    {
        _sendGridSettings = sendGridSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message, string name)
    {
        var client = new SendGridClient(_sendGridSettings.ApiKey2);
        
        var emite = new EmailAddress("vinicio.alexander.gm@gmail.com", "DESK CORE");
        var asunto = subject;
        var recibe = new EmailAddress(email, name);
        var mensaje = message;
        var htmlContent = $"<strong>{message}</strong>";

        var msg = MailHelper.CreateSingleEmail(emite, recibe, asunto, mensaje, htmlContent);

        var response = await client.SendEmailAsync(msg);

        if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
        {
            throw new Exception("Error al enviar el correo electrónico.");
        }
    }
}

public class SendGridSettings
{
    public string ApiKey { get; set; }
    public string ApiKey2 { get; set; }
}
*/