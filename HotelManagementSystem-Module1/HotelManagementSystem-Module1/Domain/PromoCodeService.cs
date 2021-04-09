using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain.Models;
using System.Collections.Generic;

/*
 * Owner of PromoCodeService: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IPromoCodeRepository _promoCodeRepository;

        public PromoCodeService(IPromoCodeRepository promoCodeRepository)
        {
            _promoCodeRepository = promoCodeRepository;
        }

        public IEnumerable<PromoCode> GetAllPromoCode()
        {
            return _promoCodeRepository.GetAll();
        }

        public PromoCode GetPromoCode(string promoCode)
        {
            return _promoCodeRepository.GetByPromoCode(promoCode);
        }

        public bool CreatePromoCode(PromoCode promoCode)
        {
            _promoCodeRepository.Insert(promoCode);
            return true;
        }

        public bool DeletePromoCode(int id)
        {
            PromoCode promoCode = _promoCodeRepository.GetById(id);
            if (promoCode == null)
                return false;
            _promoCodeRepository.Delete(promoCode);
            return true;
        }

        public bool UpdatePromoCode(PromoCode promoCode)
        {
            _promoCodeRepository.Update(promoCode);
            return true;
        }
    }
}
