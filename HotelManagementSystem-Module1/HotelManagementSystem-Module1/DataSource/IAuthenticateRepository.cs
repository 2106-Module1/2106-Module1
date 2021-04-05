

namespace HotelManagementSystem.DataSource
{
    public interface IAuthenticateRepository
    {

        string CheckPass(string username);
        bool ValidateLogin(string staff_user, string staff_password);

    }
}
