using System;

using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.DataSource;

namespace HotelManagementSystem_Module1.Domain
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

        private void SetTimer()
        {
            //will implement setTimer Event 
        }
        
        public Authenticate(IAuthenticateRepository authRep)
        {
            authRepo = authRep;
        }

        public Staff AuthenticateLogin()
        {
            throw new NotImplementedException();
        }

        public bool AuthenticatePin()
        {
            throw new NotImplementedException();
        }

        public Staff RetrieveStaff()
        {
            throw new NotImplementedException();
        }
    }
}
