using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        PromoCode GetByPromoCode(string promoCode);
    }
}
