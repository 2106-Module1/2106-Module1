using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        /// <summary>
        /// Search and retrieve promo code by promo code string
        /// </summary>
        /// <param name="promoCode">The string of the promoCode record</param>
        /// <returns>A promo code object based on the search</returns>
        PromoCode GetByPromoCode(string promoCode);
    }
}
