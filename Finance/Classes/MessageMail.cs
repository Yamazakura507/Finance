using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Maui.ApplicationModel.Communication;
using MimeKit;
using Finance.Properties;
using System.Net;
using System.Text;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Finance.Classes
{
    public static class MessageMail
    {
        //fond
        //l:no-reply@fond29.ru
        //p:2yxaXk7a4V1hJrtgTN2q

        //my
        //l:daniil.kuptsov.05@mail.ru
        //p:sMHJanxHBYPab74bH4N1

        async public static void SendMessageMail(this Page page, string htmlMessege, string subject, string[] emails, Dictionary<string, string> keyValuePairs, FileInfo[] files = null)
        {
            foreach (var item in keyValuePairs)
            {
                htmlMessege = htmlMessege.Replace(item.Key, item.Value);
            }

            using (var client = new SmtpClient())
            {
                var msg = new MimeMessage();

                msg.From.Add(new MailboxAddress("FINANCE", "daniil.kuptsov.05@mail.ru"));
                //msg.To.Add(new MailboxAddress("", "daniil.kuptsov.05@mail.ru"));

                foreach (var email in emails)
                {
                    msg.To.Add(new MailboxAddress("", email));
                }

                msg.Subject = subject;

                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = htmlMessege;

                //вложить файлы к письму
                if (files != null)
                {
                    foreach (FileInfo file in files)
                    {
                        builder.Attachments.Add(file.Name,File.ReadAllBytes(file.FullName));
                    }
                }

                msg.Body = builder.ToMessageBody();
                client.ServerCertificateValidationCallback = (mysender, certificate, chain, sslPolicyErrors) => { return true; };
                client.CheckCertificateRevocation = false;

                await client.ConnectAsync("smtp.mail.ru", 465, SecureSocketOptions.Auto).ConfigureAwait(false);
                await client.AuthenticateAsync("daniil.kuptsov.05@mail.ru", "hdxnvcBU2u8GMn4ZcSu5").ConfigureAwait(false);

                var task = await client.SendAsync(msg).ConfigureAwait(false);
                await client.DisconnectAsync(false).ConfigureAwait(false);

                GC.Collect();
            }
        }

        public static void MailGoodRegistration(this Page page, string email, Dictionary<string, string> keyValuePairs)
        {
            page.SendMessageMail(Encoding.UTF8.GetString(Properties.Resources.mailGoodRegistration), "Успешная регистрация", new[] { email }, keyValuePairs, default);
        }

        public static void CheckCodeMail(this Page page, string email, Dictionary<string, string> keyValuePairs)
        {
            page.SendMessageMail(Encoding.UTF8.GetString(Properties.Resources.mailActiv), "Подтвердитверждение учетной записи", new[] { email }, keyValuePairs, default);
        }

        public static void NewPassMail(this Page page, string email, Dictionary<string, string> keyValuePairs)
        {
            page.SendMessageMail(Encoding.UTF8.GetString(Properties.Resources.mailNewPass), "Новый пароль", new[] { email }, keyValuePairs, default);
        }
        public static void SupportMail(this Page page, string email, Dictionary<string, string> keyValuePairs)
        {
            page.SendMessageMail(Encoding.UTF8.GetString(Properties.Resources.mailSupport), "Обращение", new[] { email }, keyValuePairs, default);
        }
        public static void SupportAnswerMail(this Page page, string email, Dictionary<string, string> keyValuePairs)
        {
            page.SendMessageMail(Encoding.UTF8.GetString(Properties.Resources.mailSupportAnswer), "Ответ на обращение", new[] { email }, keyValuePairs, default);
        }
    }
}
