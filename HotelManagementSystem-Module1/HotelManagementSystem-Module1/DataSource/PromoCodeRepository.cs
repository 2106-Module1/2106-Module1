using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly IAppDbContext _appContext;

        public PromoCodeRepository(IAppDbContext appContext)
        {
            _appContext = appContext;
        }

        public IEnumerable<PromoCode> GetAll()
        {
            return _appContext.PromoCodesDb().AsEnumerable();
        }

        public PromoCode GetById(int id)
        {
            return _appContext.PromoCodesDb().AsEnumerable().SingleOrDefault(entity => (int)entity.GetPromoCode()["promoCodeId"] == id);
        }

        public PromoCode GetByPromoCode(string promoCode)
        {
            return _appContext.PromoCodesDb().AsEnumerable().SingleOrDefault(entity => (string)entity.GetPromoCode()["promoCodeString"] == promoCode);
        }

        public void Insert(PromoCode entity)
        {
            if (entity != null)
            {
                _appContext.PromoCodesDb().Add(entity);
                _appContext.SaveChanges();
            }
        }

        public void Update(PromoCode entity)
        {
            if (entity != null)
            {
                _appContext.PromoCodesDb().Update(entity);
                _appContext.SaveChanges();
            }
        }

        public void Delete(PromoCode entity)
        {
            if (entity != null)
            {
                _appContext.PromoCodesDb().Remove(entity);
                _appContext.SaveChanges();
            }
        }
    }
}
