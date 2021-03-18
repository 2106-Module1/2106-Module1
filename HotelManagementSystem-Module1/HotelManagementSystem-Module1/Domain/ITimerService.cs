using HotelManagementSystem.Domain.Models;


namespace HotelManagementSystem.Domain
{
    interface ITimerService
    {
        bool CheckPinExpired();
        void ChangePinState(bool pinState);
    }
}
