using HotelManagementSystem.Domain.Models;



namespace HotelManagementSystem.Domain
{
    public interface IAuthenticate
    {
        Staff RetrieveStaff();
        bool AuthenticateLogin(string staff_user, string staff_password);
        bool AuthenticatePin(string pin);
    }
}
