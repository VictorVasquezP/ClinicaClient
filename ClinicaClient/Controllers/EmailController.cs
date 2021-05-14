using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClinicaClient.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        [HttpPost]
        public JsonResult EnviarReceta(string correo, string archivo)
        {
            System.Diagnostics.Debug.WriteLine("correo a enviar " + correo);
            System.Diagnostics.Debug.WriteLine("archivo a enviar " + archivo);
            bool a  = SendEmailReceta(correo, archivo);
            //https://localhost:44347/Email/EnviarReceta?correo=victoe680@gmail.com&archivo=Victor_tarea1_u2.pdf
            return Json("enviado "+a, JsonRequestBehavior.AllowGet);
        }

        public bool SendEmailReceta(string correoDestino,string nameArchivo)
        {
            string rutaCompartida = "\\\\127.0.0.1\\Users\\Public\\";
            try
            {
                var fromAddress = new MailAddress("vpoblete001@gmail.com", "CLINICA MITCHELL");
                const string fromPassword = "ladesiempre";
                const string subject = "Receta";

                string body = "<p style='width: 60%; margin:auto; text-align:justify;font-size:22px; line-height: 35px; color: #787675; font-family: monospace; margin-top:-50px'>Se le compratió por este medio la receta de su consulta</p>";


                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                var Mensaje = new MailMessage();
                
                Mensaje.To.Add(new MailAddress(correoDestino));

                string path = Server.MapPath("~/Files/Recetas/"+nameArchivo);
                System.Diagnostics.Debug.WriteLine("path file " + path);

                //System.IO.File.Copy(path, rutaCompartida + nameArchivo);

                Mensaje.Attachments.Add(new Attachment(path));
                Mensaje.From = fromAddress;
                Mensaje.Subject = subject;
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
                Mensaje.AlternateViews.Add(alternateView);
                Mensaje.Body = body;
                Mensaje.IsBodyHtml = true;
                smtp.Send(Mensaje);


                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }

    }
}