using HotelManagementSystem.Domain.Models;
using System.Collections.Generic;

namespace HotelManagementSystem.Domain
{
    public interface IPromoCodeService
    {

        IEnumerable<PromoCode> GetAllPromoCode();

        PromoCode GetPromoCode(string promoCode);

        bool CreatePromoCode(PromoCode promoCode);

        bool DeletePromoCode(int id);

        bool UpdatePromoCode(PromoCode promoCode);
    }
}
