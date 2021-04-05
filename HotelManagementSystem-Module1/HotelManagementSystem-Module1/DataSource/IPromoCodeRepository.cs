using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        PromoCode GetByPromoCode(string promoCode);
    }
}
