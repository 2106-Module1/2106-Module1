

namespace HotelManagementSystem.DataSource
{
    public interface IAuthenticateRepository
    {
        /// <summary>
        /// Check and validate whether an user exist with the entered username and password
        /// </summary>
        /// <param name="staff_user">username</param>
        /// <param name="staff_password">password</param>
        /// <returns>bool indicated the login success status</returns>
        bool ValidateLogin(string staff_user, string staff_password);

    }
}
