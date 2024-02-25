using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MimeKit;
using MailKit.Net.Smtp;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DotLiquid.Util;
using Twilio;
using Twilio.Rest.Lookups.V1;

namespace RP_Reality_realestate.Models
{
    public class ContactMail
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Your contact number :")]
        [Required(ErrorMessage = "A phone number is required.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^(?!(\d)\1{9})\d{10}$", ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Msg { get; set; }


        public static void SendEmail(string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Nikita Singh", "nikitasingh@flybytech.co"));
                message.To.Add(new MailboxAddress("", to));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = body;
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    // Replace "SMTP_SERVER_IP_ADDRESS" with the actual IP address of the SMTP server
                    client.Connect("smtpout.secureserver.net", 465, true);

                    // Update the credentials accordingly
                    client.Authenticate("nikitasingh@flybytech.co", "Nikita@27");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email to {to}: {ex.Message}");
                throw; // Re-throw the exception to handle it at the controller level
            }
        }
    }
}
