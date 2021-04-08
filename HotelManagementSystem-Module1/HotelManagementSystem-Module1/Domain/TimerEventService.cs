using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Collections.Generic;

namespace HotelManagementSystem.Domain
{
    public class TimerEventService : IHostedService
    {

        private readonly IServiceScopeFactory scopeFactory;

        public TimerEventService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }
        /// <summary>
        /// Task to start background timer
        /// </summary>
        /// <returns>Task</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(SetTimer, cancellationToken);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Task to stop background timer
        /// </summary>
        /// <returns>null</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return null;
        }

        /// <summary>
        /// When SetTimer() gets called 
        /// it will generate pin number and send email once the timer reaches specifed time
        /// </summary>
        /// <returns>null</returns>
        private Task SetTimer()
        {

            using var scope = scopeFactory.CreateScope();
            var iPinRepo = scope.ServiceProvider.GetRequiredService<IPinRepository>();

            while (true)
            {

                Debug.WriteLine("TIME TO CHANGE PIN");
                //Wait 2 minutes till next execution
                var genPin = GeneratePin();
                var pinObj = iPinRepo.GetPin();
                pinObj.UpdatePin(genPin);
                iPinRepo.UpdatePin(pinObj);

                //ensure pin is in DB
                if (iPinRepo.ValidatePin(genPin) != null)
                {
                    SendEmail(genPin);
                    Debug.WriteLine(iPinRepo.ValidatePin(genPin).PinNumberDetails().ToString());
                }
                else
                {
                    Debug.WriteLine("No pin found");
                }

                DateTime nextStop = DateTime.Now.AddSeconds(60);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }


        }

        /// <summary>
        /// SendEmail() will get a list of managers and send them an email to notify them the pin change
        /// </summary>
        private void SendEmail(string pin)
        {
            //Retrieve List of managers
            IEnumerable<Staff> staffList;
            using var scope = scopeFactory.CreateScope();
            var staffGateway = scope.ServiceProvider.GetRequiredService<IStaffGateway>();
            staffList = staffGateway.RetrieveStaffDetailsByRole("Manager");
            //Uses XML reader to read the mail template
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            var mailBody = "";
            var mailAddFrom = "";
            var mailPw = "";
            var mailSubj = "";

            XmlReader xReader = XmlReader.Create("Properties/MailTemplate.xml", settings);
            while (xReader.Read())
            {
                switch (xReader.Name)
                {
                    case "EmailSubject":
                        mailSubj = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        break;
                    case "EmailBody":
                        mailBody = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        break;
                    case "EmailAdressFrom":
                        mailAddFrom = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        break;
                    case "EmailPassword":
                        mailPw = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        break;
                    default:
                        break;
                }
            }

            //Iterate through the list and send emails
            foreach (var staffs in staffList)
            {
                Debug.WriteLine(staffs.StaffEmailDetail());

                if (staffs.StaffEmailDetail() != "")
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(mailAddFrom, "No-Reply@2106proj@gmail.com");
                    mailMessage.To.Add(staffs.StaffEmailDetail());
                    mailMessage.Subject = mailSubj;
                    mailMessage.Body = mailBody;

                    string message = string.Format(mailMessage.Body, staffs.StaffUsernameDetail(), pin);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = message;

                    using var smtpClient = new SmtpClient();
                    smtpClient.Connect("smtp.gmail.com", 465, true);
                    smtpClient.Authenticate(mailAddFrom, mailPw);
                    smtpClient.Send((MimeMessage)mailMessage);
                    smtpClient.Disconnect(true);
                }
            }


        }

        /// <summary>
        /// Generate 4 digit pin for managers
        /// </summary>
        /// <returns>string</returns>
        private string GeneratePin()
        {
            Random _random = new Random();
            var random_digit = 0;
            random_digit = _random.Next(1000, 9999);
            var pinBuilder = new StringBuilder();
            pinBuilder.Append(random_digit);
            return pinBuilder.ToString();
        }


    }
}