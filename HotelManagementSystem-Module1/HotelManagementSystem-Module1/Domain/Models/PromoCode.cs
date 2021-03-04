using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain.Models
{
    public class PromoCode
    {
        [Key]
        private int PromoCodeId { get; set; }

        [Required]
        private string PromoCodeString { get; set; }

        [Required]
        private int Discount { get; set; }

        public PromoCode()
        {

        }

        private PromoCode(Dictionary<string, object> promoCodeDictionary)
        {
            PromoCodeString = (string)promoCodeDictionary["promoCodeString"];
            Discount = (int)promoCodeDictionary["discount"];
        }

        private Dictionary<string, object> PromoCodeDetails()
        {
            var promoCodeDetail = new Dictionary<string, object>
            {
                ["promoCodeId"] = PromoCodeId,
                ["promoCodeString"] = PromoCodeString,
                ["discount"] = Discount
            };

            return promoCodeDetail;
        }

        public Dictionary<string, object> GetPromoCode()
        {
            Dictionary<string, object> promoCodeDetail = PromoCodeDetails();

            return promoCodeDetail;
        }

        public object SetPromoCode(Dictionary<string, object> promoCodeDetail)
        {
            PromoCode obj = new PromoCode(promoCodeDetail);

            return obj;
        }
    }
}
