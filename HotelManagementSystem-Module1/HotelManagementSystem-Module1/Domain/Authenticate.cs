using System;

using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using System.Timers;
using System.Text;

namespace HotelManagementSystem.Domain
{
    public class Authenticate: IAuthenticate 
    {
        private readonly IAuthenticateRepository authRepo;

        private bool ComparePass(string username, string password)
        {
            string pass = authRepo.CheckPass(username);
            if (pass != null)
            {
                return pass == password;
            }
            return false;
        }

        private bool ValidatePin(string pin)
        {
            return authRepo.FindPin("") == pin;
        }

        private string GeneratePin()
        {
            Random _random = new Random();
            var random_digit = 0;
            random_digit = _random.Next(1000, 9999);
            
            var pinBuilder = new StringBuilder();
            pinBuilder.Append(random_digit);

            return pinBuilder.ToString();
        }


        public Authenticate(IAuthenticateRepository authRep)
        {
            authRepo = authRep;
            
        }

        public Staff AuthenticateLogin()
        {
            throw new NotImplementedException();
        }

        public bool AuthenticatePin(string command, string pin)
        {
            //call validate pin 

            string caseCmd = command;
            var validPin = false;
            switch (caseCmd)
            {
                case "UpdatePin":
                    Console.WriteLine("Case 1");
                    break;
                case "ValidatePin":
                    validPin = ValidatePin(pin);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            return validPin;

        }

        public Staff RetrieveStaff()
        {
            throw new NotImplementedException();
        }


    }
}
