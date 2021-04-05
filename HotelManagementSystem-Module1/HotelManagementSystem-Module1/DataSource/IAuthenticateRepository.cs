

namespace HotelManagementSystem.DataSource
{
    public interface IAuthenticateRepository
    {
        bool ValidateLogin(string staff_user, string staff_password);

    }
}
