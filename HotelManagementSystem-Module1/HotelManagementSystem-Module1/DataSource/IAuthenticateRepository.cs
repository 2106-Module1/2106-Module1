

namespace HotelManagementSystem.DataSource
{
    public interface IAuthenticateRepository
    {

        string CheckPass(string username);
        //string FindPin(string username);
        //void UpdatePin(string pin);
        bool validateLogin(string staff_user, string staff_password);

    }
}
