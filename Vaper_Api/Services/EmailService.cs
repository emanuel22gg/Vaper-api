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

        private string GetHtmlTemplate(string titulo, string contenido, string logoHtml)
        {
            return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        </head>
        <body style='margin: 0; padding: 0; font-family: ""Segoe UI"", ""Helvetica Neue"", Helvetica, Arial, sans-serif; background-color: #0f172a; color: #f8fafc;'>
            <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #0f172a; padding: 20px 0;'>
                <tr>
                    <td align='center'>
                        <table width='100%' cellpadding='0' cellspacing='0' style='max-width: 600px; background-color: #000000; border-radius: 12px; overflow: hidden; box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.5); margin: 0 10px; border: 1px solid #334155;'>
                            <!-- Header -->
                            <tr>
                                <td style='padding: 20px 30px; border-bottom: 2px solid #eab308;'>
                                    <table width='100%' cellpadding='0' cellspacing='0'>
                                        <tr>
                                            <td style='text-align: left; vertical-align: middle;'>
                                                <h1 style='margin: 0; font-size: 32px; font-weight: 700; font-family: ""Impact"", ""Arial Black"", sans-serif; font-style: italic;'>
                                                    <span style='color: #ffffff;'>Vaper</span><br/>
                                                    <span style='color: #eab308; font-size: 22px;'>one mede 9</span>
                                                </h1>
                                            </td>
                                            <td style='text-align: right; vertical-align: middle; width: 90px;'>
                                                <a href='https://vaper-web.vercel.app/' style='text-decoration: none; display: inline-block;'>
                                                    {logoHtml}
                                                </a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            <!-- Body -->
                            <tr>
                                <td style='padding: 40px 30px;'>
                                    <h2 style='color: #eab308; margin-top: 0; margin-bottom: 20px; font-size: 22px;'>{titulo}</h2>
                                    {contenido}
                                </td>
                            </tr>
                            
                            <!-- Footer -->
                            <tr>
                                <td style='background-color: #111827; padding: 20px 30px; text-align: center; border-top: 1px solid #334155;'>
                                    <div style='margin-bottom: 20px;'>
                                        <a href='https://vaper-web.vercel.app/' style='background-color: #eab308; color: #000000; padding: 12px 25px; text-decoration: none; border-radius: 6px; font-weight: bold; font-size: 15px; display: inline-block; font-family: sans-serif;'>Visitar Tienda Web</a>
                                    </div>
                                    <p style='margin: 0 0 10px 0; color: #94a3b8; font-size: 14px; line-height: 1.5;'>
                                        Si tienes algún problema o duda, comunícate al número<br/>
                                        <strong style='color: #eab308; font-size: 16px;'>312 247 5042</strong>
                                    </p>
                                    <p style='margin: 0; color: #64748b; font-size: 12px;'>
                                        &copy; {DateTime.Now.Year} Vaper One Mede 9. Todos los derechos reservados.
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>";
        }

        private MimeEntity GetMessageBody(string titulo, string contenido)
        {
            var builder = new BodyBuilder();
            string logoHtml = "";
            try 
            {
                var imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "logo.jpg");
                if (!System.IO.File.Exists(imagePath))
                {
                    imagePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "assets", "logo.jpg");
                }

                if (System.IO.File.Exists(imagePath))
                {
                    var logo = builder.LinkedResources.Add(imagePath);
                    logo.ContentId = "vaperlogo";
                    logoHtml = "<img src=\"cid:vaperlogo\" alt=\"Logo\" style=\"max-width: 90px; height: auto; border: none; border-radius: 8px;\" />";
                }
            }  
            catch { }

            if (string.IsNullOrEmpty(logoHtml)) 
            {
                logoHtml = "";
            }

            builder.HtmlBody = GetHtmlTemplate(titulo, contenido, logoHtml);
            return builder.ToMessageBody();
        }

        public async Task<(bool Exitoso, string? Error)> EnviarEmailConfirmacionPedido(string emailDestino, string nombreCompleto, int numeroPedido, DateTime fechaCreacion, decimal? total, string? metodoPago, string? direccionEntrega)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vaper One", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = $"¡Tu pedido #{numeroPedido} fue creado exitosamente! - Vaper One";

                string contenido = $@"
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Hola <strong style='color: #ffffff;'>{nombreCompleto}</strong>,</p>
                    <p style='margin: 0 0 25px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Tu pedido ha sido creado exitosamente. Aquí tienes el resumen de tu compra:</p>
                    
                    <table cellpadding='0' cellspacing='0' style='width: 100%; border-collapse: collapse; margin-bottom: 25px; border-radius: 8px; overflow: hidden; border: 1px solid #334155; color: #e2e8f0;'>
                        <tr style='background-color: #1e293b;'>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; border-right: 1px solid #334155; font-weight: 600; color: #cbd5e1; width: 40%;'>Número de pedido</td>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; color: #ffffff;'>#{numeroPedido}</td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; border-right: 1px solid #334155; font-weight: 600; color: #cbd5e1;'>Fecha</td>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; color: #ffffff;'>{fechaCreacion:dd/MM/yyyy HH:mm}</td>
                        </tr>
                        <tr style='background-color: #1e293b;'>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; border-right: 1px solid #334155; font-weight: 600; color: #cbd5e1;'>Método de pago</td>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; color: #ffffff;'>{(metodoPago ?? "No especificado")}</td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; border-right: 1px solid #334155; font-weight: 600; color: #cbd5e1;'>Dirección de entrega</td>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; color: #ffffff;'>{(direccionEntrega ?? "No especificada")}</td>
                        </tr>
                        <tr style='background-color: #eab308;'>
                            <td style='padding: 15px; border-right: 1px solid #ca8a04; font-weight: 700; color: #000000; font-size: 16px;'>Total</td>
                            <td style='padding: 15px; font-weight: 700; color: #000000; font-size: 16px;'>${total:N2}</td>
                        </tr>
                    </table>
                    
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Pronto recibirás actualizaciones sobre el estado de tu pedido.</p>
                    <p style='margin: 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Gracias por tu compra.<br/><strong style='color: #eab308;'>Equipo Vaper One</strong></p>
                ";

                message.Body = GetMessageBody("¡Pedido Confirmado!", contenido);

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
                message.From.Add(new MailboxAddress("Vaper One", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = "Tu cuenta ha sido aprobada - Vaper One";

                string contenido = $@"
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Hola <strong style='color: #ffffff;'>{nombreCompleto}</strong>,</p>
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Nos complace informarte que tu documento ha sido validado exitosamente y tu cuenta ha sido <strong style='color: #34d399;'>aprobada</strong>.</p>
                    
                    <div style='background-color: #064e3b; border-left: 4px solid #34d399; padding: 15px; margin: 25px 0; border-radius: 0 6px 6px 0;'>
                        <p style='margin: 0; color: #a7f3d0; font-size: 15px;'>Ya puedes iniciar sesión en la plataforma con tus credenciales registradas y disfrutar de todos nuestros servicios.</p>
                    </div>
                    
                    <p style='margin: 0 0 25px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Bienvenido/a a la familia <strong style='color: #eab308;'>Vaper One Mede 9</strong>.</p>
                    <p style='margin: 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Atentamente,<br/><strong style='color: #eab308;'>Equipo Vaper One</strong></p>
                ";

                message.Body = GetMessageBody("¡Cuenta Aprobada!", contenido);

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

        public async Task<(bool Exitoso, string? Error)> EnviarEmailEstadoPedido(string emailDestino, string nombreCompleto, int numeroPedido, string estadoPedido, DateTime fecha, string? guia = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Vaper One", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = $"Actualización de tu pedido #{numeroPedido} - Vaper One";

                string guiaHtml = string.IsNullOrWhiteSpace(guia) ? "" : $@"
                        <tr style='background-color: #1e293b;'>
                            <td style='padding: 12px 15px; border-top: 1px solid #334155; border-right: 1px solid #334155; font-weight: 600; color: #cbd5e1; width: 40%;'>Número de guía</td>
                            <td style='padding: 12px 15px; border-top: 1px solid #334155; color: #ffffff;'>{guia}</td>
                        </tr>";

                string contenido = $@"
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Hola <strong style='color: #ffffff;'>{nombreCompleto}</strong>,</p>
                    <p style='margin: 0 0 25px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Te informamos que el estado de tu pedido realizado el <strong style='color: #ffffff;'>{fecha:dd/MM/yyyy}</strong> ha sido actualizado.</p>
                    
                    <table cellpadding='0' cellspacing='0' style='width: 100%; border-collapse: collapse; margin-bottom: 25px; border-radius: 8px; overflow: hidden; border: 1px solid #334155; color: #e2e8f0;'>
                        <tr>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; border-right: 1px solid #334155; font-weight: 600; color: #cbd5e1; width: 40%;'>Número de pedido</td>
                            <td style='padding: 12px 15px; border-bottom: 1px solid #334155; color: #ffffff;'>#{numeroPedido}</td>
                        </tr>
                        <tr>
                            <td style='padding: 12px 15px; border-right: 1px solid #334155; font-weight: 600; color: #cbd5e1;'>Nuevo estado</td>
                            <td style='padding: 12px 15px; color: #eab308; font-weight: 700; font-size: 16px;'>{estadoPedido.ToUpper()}</td>
                        </tr>
                        {guiaHtml}
                    </table>
                    
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Si tienes alguna pregunta sobre tu pedido, no dudes en contactarnos.</p>
                    <p style='margin: 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Gracias por tu compra.<br/><strong style='color: #eab308;'>Equipo Vaper One</strong></p>
                ";

                message.Body = GetMessageBody("Actualización de Pedido", contenido);

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
                message.From.Add(new MailboxAddress("Vaper One", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = "Tu cuenta no ha sido aprobada - Vaper One";

                string contenido = $@"
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Hola <strong style='color: #ffffff;'>{nombreCompleto}</strong>,</p>
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Lamentamos informarte que tu documento no pudo ser validado y tu cuenta <strong style='color: #f87171;'>no ha sido aprobada</strong> en este momento.</p>
                    
                    <div style='background-color: #7f1d1d; border-left: 4px solid #f87171; padding: 15px; margin: 25px 0; border-radius: 0 6px 6px 0;'>
                        <p style='margin: 0; color: #fecaca; font-size: 15px;'>Si crees que esto es un error, por favor asegúrate de que el documento enviado sea legible y válido, o comunícate con nuestro equipo de soporte para más información.</p>
                    </div>
                    
                    <p style='margin: 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Atentamente,<br/><strong style='color: #eab308;'>Equipo Vaper One</strong></p>
                ";

                message.Body = GetMessageBody("Cuenta No Aprobada", contenido);

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
                message.From.Add(new MailboxAddress("Vaper One", _fromEmail));
                message.To.Add(new MailboxAddress("", emailDestino));
                message.Subject = "Recuperación de Contraseña - Vaper One";

                string contenido = $@"
                    <p style='margin: 0 0 15px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Has solicitado recuperar tu contraseña de acceso a Vaper One Mede 9.</p>
                    <p style='margin: 0 0 20px 0; font-size: 16px; line-height: 1.5; color: #e2e8f0;'>Ingresa el siguiente código de verificación en la aplicación:</p>
                    
                    <div style='background-color: #1e293b; border: 2px dashed #eab308; padding: 20px; text-align: center; margin: 25px 0; border-radius: 8px;'>
                        <h1 style='color: #facc15; font-size: 36px; letter-spacing: 8px; margin: 0; font-family: monospace;'>{codigo}</h1>
                    </div>
                    
                    <p style='margin: 0 0 15px 0; font-size: 14px; color: #94a3b8; line-height: 1.5;'>Este código expira en <strong style='color: #cbd5e1;'>15 minutos</strong>.</p>
                    <p style='margin: 0; font-size: 14px; color: #94a3b8; line-height: 1.5;'>Si no solicitaste este cambio, por favor ignora este mensaje o contacta a soporte si tienes dudas.</p>
                ";

                message.Body = GetMessageBody("Recuperación de Contraseña", contenido);

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
