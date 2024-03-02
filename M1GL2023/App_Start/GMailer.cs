using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace M1GL2023.App_Start
{
    public class GMailer
    {

        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 25;
            GmailSSL = true;
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

            try
            {
                using (var message = new MailMessage(GmailUsername, ToEmail))
                {
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = IsHtml;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                ///to do: stockage dans une table pour un envoi antérieur
            }
        }

        public static void senMail(string destinataire, string subjet, string body)
        {
            try
            {
                GMailer.GmailUsername = System.Configuration.ConfigurationManager.AppSettings["Email"];
                GMailer.GmailPassword = System.Configuration.ConfigurationManager.AppSettings["PasswordEmail"];

                GMailer mailer = new GMailer();
                mailer.ToEmail = destinataire;
                mailer.Subject = subjet;
                mailer.Body = body;
                mailer.IsHtml = true;
                mailer.Send();
            }
            catch (Exception ex)
            {

            }
        }

        //public static void senMail(string address, string subject, string body)
        //{

        //    using (MailMessage message = new MailMessage())
        //    {
        //        message.Subject = subject;
        //        message.Body = "<pre>" + body + "</pre>";
        //        message.To.Add(address);
        //        message.Priority = MailPriority.High;
        //        message.IsBodyHtml = true;
        //        using (SmtpClient client = new SmtpClient())
        //        {
        //            try
        //            {
        //                client.Send(message);
        //            }

        //            catch (Exception e)
        //            {
        //                e.GetBaseException();
        //                TempMail mail = new TempMail();
        //                mail.DestinataireMail = address;
        //                mail.Envoyer = "N";
        //                mail.TextMail = body;
        //                mail.TitreMail = subject;
        //                service.AddMailSended(mail);
        //            }
        //        }
        //    }
        //}
    }
}