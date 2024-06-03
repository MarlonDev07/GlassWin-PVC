using System;
using System.Net;
using System.Net.Mail;

namespace Negocio.SMS__WhatsApp_Email
{
    public class N_SendEmail
    {
        public bool SendEmailUser(string Email, string Username, string PassWord)
        {
            string remitente = "glasswin526@gmail.com";
            string contraseña = "cyym jcsm aiwh xpyh";
            string destinatario = Email.ToLower();
            string asunto = "Nombre de Usuario y Contraseña ";
            string cuerpo = "De parte de GlassWin sofware Solution le enviamos el nombre de Usuario y Contraseña para el inicio de Secion en la aplicacion.\n" + "UserName: " + Username.Trim() + "\n" + "PassWord: " + PassWord.Trim();

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(remitente, contraseña),
                EnableSsl = true,
            };

            MailMessage mensaje = new MailMessage(remitente, destinatario, asunto, cuerpo);

            try
            {
               // Enviar el correo
               smtpClient.Send(mensaje);
               return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}
