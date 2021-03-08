using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class PromoCodeController : Controller
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodeController(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        public IActionResult PromoCodeView()
        {
            IEnumerable<PromoCode> promoCodeList = _promoCodeService.GetAllPromoCode();
            ArrayList mainList = new ArrayList();

            // For Loop to store all existing reservation with guest name and email into a ArrayList to pass to View page 
            foreach (var pc in promoCodeList)
            {
                Dictionary<string, object> promo = pc.GetPromoCode();
                String[] subList =
                {
                    promo["promoCodeString"].ToString(),
                    promo["discount"].ToString()
                };
                mainList.Add(subList);
            }

            // Passing data over to View Page via ViewBag "/Reservation/ReservationView"
            ViewBag.mainList = mainList;
            return View();
        }

        [HttpPost]
        public IActionResult CreatePromoCode()
        {
            Dictionary<string, object> promoCode = new Dictionary<string, object>();
            var formPromoCode = "MBSOFF" + Request.Form["discount"];
            promoCode.Add("promoCodeString", formPromoCode);
            promoCode.Add("discount", Convert.ToInt32(Request.Form["discount"]));

            // Creating Reservation object and storing it to database
            PromoCode promoCodeObj = (PromoCode)new PromoCode().SetPromoCode(promoCode);
            _promoCodeService.CreatePromoCode(promoCodeObj);

            return Redirect("PromoCodeView");
        }
    }
}
