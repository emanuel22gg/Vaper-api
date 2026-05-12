using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Vaper_Api.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _fromEmail = "vaperone4@gmail.com";
        private readonly string _fromPassword = "kihz qguo ctkk wsyi";

        private const string _pieContacto = "<br/><hr style='border: none; border-top: 1px solid #eee;'/><p style='color: #888; font-size: 13px;'>Si tienes algún problema o duda, comunícate al número <strong>3122475042</strong>.</p>";

        public async Task<(bool Exitoso, string? Error)> EnviarEmailConfirmacionPedido(string emailDestino, string nombreCompleto, int numeroPedido, DateTime fechaCreacion, decimal? total, string? metodoPago, string? direccionEntrega)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vaper App", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = $"¡Tu pedido #{numeroPedido} fue creado exitosamente! - Vaper App";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>¡Pedido Confirmado!</h2>
                        <p>Hola <strong>{nombreCompleto}</strong>,</p>
                        <p>Tu pedido ha sido creado exitosamente. Aquí tienes el resumen:</p>
                        <table style='border-collapse: collapse; width: 100%; max-width: 400px;'>
                            <tr>
                                <td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Número de pedido</td>
                                <td style='padding: 8px; border: 1px solid #ddd;'>#{numeroPedido}</td>
                            </tr>
                            <tr>
                                <td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Fecha</td>
                                <td style='padding: 8px; border: 1px solid #ddd;'>{fechaCreacion:dd/MM/yyyy HH:mm}</td>
                            </tr>
                            <tr>
                                <td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Método de pago</td>
                                <td style='padding: 8px; border: 1px solid #ddd;'>{metodoPago ?? "No especificado"}</td>
                            </tr>
                            <tr>
                                <td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Dirección de entrega</td>
                                <td style='padding: 8px; border: 1px solid #ddd;'>{direccionEntrega ?? "No especificada"}</td>
                            </tr>
                            <tr>
                                <td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Total</td>
                                <td style='padding: 8px; border: 1px solid #ddd;'><strong>${total:N2}</strong></td>
                            </tr>
                        </table>
                        <br/>
                        <p>Pronto recibirás actualizaciones sobre el estado de tu pedido.</p>
                        <p>Gracias por tu compra.<br/><strong>Equipo Vaper App</strong></p>
                        {_pieContacto}
                    "
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_fromEmail, _fromPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return (true, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar email de confirmación de pedido: {ex.Message}");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Exitoso, string? Error)> EnviarEmailAprobacion(string emailDestino, string nombreCompleto)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vaper App", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = "Tu cuenta ha sido aprobada - Vaper App";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>¡Cuenta Aprobada!</h2>
                        <p>Hola <strong>{nombreCompleto}</strong>,</p>
                        <p>Nos complace informarte que tu documento ha sido validado exitosamente y tu cuenta ha sido aprobada.</p>
                        <p>Ya puedes iniciar sesión en la plataforma con tus credenciales registradas.</p>
                        <br/>
                        <p>Bienvenido/a a <strong>Vaper App</strong>.</p>
                        {_pieContacto}
                    "
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_fromEmail, _fromPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return (true, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar email de aprobación: {ex.Message}");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Exitoso, string? Error)> EnviarEmailEstadoPedido(string emailDestino, string nombreCompleto, int numeroPedido, string estadoPedido, DateTime fecha)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vaper App", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = $"Actualización de tu pedido #{numeroPedido} - Vaper App";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Actualización de tu Pedido</h2>
                        <p>Hola <strong>{nombreCompleto}</strong>,</p>
                        <p>Te informamos que el estado de tu pedido realizado el <strong>{fecha:dd/MM/yyyy}</strong> ha sido actualizado.</p>
                        <table style='border-collapse: collapse; width: 100%; max-width: 400px;'>
                            <tr>
                                <td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Número de pedido</td>
                                <td style='padding: 8px; border: 1px solid #ddd;'>#{numeroPedido}</td>
                            </tr>
                            <tr>
                                <td style='padding: 8px; border: 1px solid #ddd; font-weight: bold;'>Nuevo estado</td>
                                <td style='padding: 8px; border: 1px solid #ddd; color: #2196F3;'><strong>{estadoPedido}</strong></td>
                            </tr>
                        </table>
                        <br/>
                        <p>Si tienes alguna pregunta sobre tu pedido, no dudes en contactarnos.</p>
                        <p>Gracias por tu compra.<br/><strong>Equipo Vaper App</strong></p>
                        {_pieContacto}
                    "
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_fromEmail, _fromPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return (true, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar email de estado de pedido: {ex.Message}");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Exitoso, string? Error)> EnviarEmailRechazo(string emailDestino, string nombreCompleto)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vaper App", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = "Tu cuenta no ha sido aprobada - Vaper App";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <h2>Cuenta No Aprobada</h2>
                        <p>Hola <strong>{nombreCompleto}</strong>,</p>
                        <p>Lamentamos informarte que tu documento no pudo ser validado y tu cuenta no ha sido aprobada en este momento.</p>
                        <p>Si crees que esto es un error o deseas más información, por favor comunícate con nuestro equipo de soporte.</p>
                        <br/>
                        <p>Atentamente,<br/><strong>Equipo Vaper App</strong></p>
                        {_pieContacto}
                    "
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_fromEmail, _fromPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return (true, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar email de rechazo: {ex.Message}");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Exitoso, string? Error)> EnviarEmailRecuperacion(string emailDestino, string codigo)
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
                        {_pieContacto}
                    "
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_fromEmail, _fromPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return (true, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar email: {ex.Message}");
                return (false, ex.Message);
            }
        }
    }
}
