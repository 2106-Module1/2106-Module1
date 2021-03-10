using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain.Models;

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
