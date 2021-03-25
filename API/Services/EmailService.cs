using API.DTOs;
using API.Entities;
using API.Interfaces;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using API.Services.Helpers;

namespace API.Services
{
    /// <summary>
    /// Email sending service
    /// TODO - Konfiguracja serwisu
    /// </summary>
    public class EmailService : IEmailService
    {
        public async Task<bool> SendPasswordConfirmationEmailAsync(string emailAdress, string confirmationLink)
        {
            StringBuilder template = EmailTemplateHelpers.GetPasswordConfimartionTemplate();

            string emailSubject = "[Sklep Gabio.pl] Potwierdź swój adres email";

            return await SendEmail(
                emailAdress, 
                emailSubject,
                template, 
                new { ActivationLink = $"https://gabio.pl/potwierdz-email/{confirmationLink}" });
        }

        public async Task<bool> SendPasswordResetEmailAsync(string emailAdress, string username, string resetLink)
        {
            StringBuilder template = EmailTemplateHelpers.GetPasswordResetTemplate();

            string emailSubject = "[Sklep Gabio.pl] Reset hasła";

            return await SendEmail(
                emailAdress,
                emailSubject,
                template,
                new { 
                    ResetLink = $"https://gabio.pl/resetuj-haslo/{resetLink}",
                    Username = username
                });
        }

        private SmtpSender GetEmailSender()
        {
            return new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });
        }

        private async Task<bool> SendEmail(string emailAdress, string emailSubject, StringBuilder template, object templateArguments)
        {
            Email.DefaultSender = GetEmailSender();
            Email.DefaultRenderer = new RazorRenderer();

            SendResponse email = await Email
                    .From("Test@Sender.com")
                    .To(emailAdress)
                    .Subject(emailSubject)
                    .UsingTemplate(template.ToString(), templateArguments)
                    .SendAsync();

            return email.Successful;
        }
    }
}
