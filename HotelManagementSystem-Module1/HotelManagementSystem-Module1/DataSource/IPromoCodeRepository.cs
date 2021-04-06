using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        /*
         * <summary>
         * Search and retrieve promocode by promocode string
         * </summary>
         * <param>promoCode, the string of the promoCode record </param>
         * <returns>A promo code object based on the search<returns>
         */
        PromoCode GetByPromoCode(string promoCode);
    }
}
