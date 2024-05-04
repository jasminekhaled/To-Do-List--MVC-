﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Security.Cryptography;
using System.Text;

namespace ToDoList_DomainModel.Helpers
{
    public static class MailService
    {

        public static string DisplayName = "Testingmailservice";
        public static string Password = "aujh ueet cxdo npar";
        public static string Email = "jemyjemy1212@gmail.com";
        public static string Host = "smtp.gmail.com";
        public static int Port = 587;


        public static async Task<bool> SendEmailAsync(string mailTo, string subject, string body)
        {

            try
            {

                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(Email),
                    Subject = subject
                };

                email.To.Add(MailboxAddress.Parse(mailTo));

                var builder = new BodyBuilder();


                builder.HtmlBody = body;
                email.Body = builder.ToMessageBody();
                email.From.Add(new MailboxAddress(DisplayName, Email));

                using var smtp = new SmtpClient();
                smtp.Connect(Host, Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(Email, Password);
                await smtp.SendAsync(email);

                smtp.Disconnect(true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        // Function to Generate a random string with a given size
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;
            for (var i = 0; i < size; i++)
            {
                var @char = (char)RandomNumberGenerator.GetInt32(offset, offset + lettersOffset);
                builder.Append(@char);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

    }
}
