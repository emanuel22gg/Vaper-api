using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Vaper_Api.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // O el que uses
        private readonly int _smtpPort = 587;
        private readonly string _fromEmail = "vaperone4@gmail.com"; // Cambia esto
        private readonly string _fromPassword = "hadx qbyz guhn gdnk"; // Contraseña de aplicación

        public async Task<bool> EnviarEmailRecuperacion(string emailDestino, string codigo)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vaper App", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = "Recuperación de Contraseña - Vaper App";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Recuperación de Contraseña</h2>
                        <p>Has solicitado recuperar tu contraseña.</p>
                        <p>Tu código de verificación es:</p>
                        <h1 style='color: #2196F3; font-size: 32px; letter-spacing: 5px;'>{codigo}</h1>
                        <p>Este código expira en 15 minutos.</p>
                        <p>Si no solicitaste este cambio, ignora este mensaje.</p>
                    "
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_fromEmail, _fromPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar email: {ex.Message}");
                return false;
            }
        }
    }
}