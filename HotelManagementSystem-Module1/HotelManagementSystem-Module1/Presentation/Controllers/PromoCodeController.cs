using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * Owner of ReservationController: Mod 1 Team 4
 * This Controller is used for Viewing and Creating Promo Codes.
 */
namespace HotelManagementSystem.Presentation.Controllers
{
    public class PromoCodeController : Controller
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodeController(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }
        
        /// <summary>
        /// (Completed)
        /// Function to retrieve all existing Promo Code found in the database
        /// </summary>
        public IActionResult PromoCodeView()
        {
            // Retrieve all existing Promo Codes
            IEnumerable<PromoCode> promoCodeList = _promoCodeService.GetAllPromoCode();
            ArrayList mainList = new ArrayList();

            // For Loop to store all existing promo code into a ArrayList to pass to View page 
            foreach (var pc in promoCodeList)
            {
                Dictionary<string, object> promo = pc.GetPromoCode();
                string[] subList =
                {
                    promo["promoCodeString"].ToString(),
                    promo["discount"].ToString()
                };
                mainList.Add(subList);
            }

            // Passing data over to View Page via ViewBag "/PromoCode/PromoCodeView"
            ViewBag.mainList = mainList;
            return View();
        }
        
        /// <summary>
        /// (Completed)
        /// Function to retrieve all post over Data from the form and insert Promo Code into database
        /// </summary>
        /// <param name="promoCodeForm">Form data parse from client side via POST request</param>
        [HttpPost]
        public IActionResult CreatePromoCode(IFormCollection promoCodeForm)
        {
            // Initialise dictionary to store promo code
            Dictionary<string, object> promoCode = new Dictionary<string, object>();

            // Retrieving POST data and adding to dictionary
            var formPromoCode = "MBS" + DateTime.Now.Day + "OFF" + promoCodeForm["discount"];
            promoCode.Add("promoCodeString", formPromoCode);
            promoCode.Add("discount", Convert.ToInt32(promoCodeForm["discount"]));

            // Creating Promo Code object and storing it to database
            PromoCode promoCodeObj = (PromoCode)new PromoCode().SetPromoCode(promoCode);
            _promoCodeService.CreatePromoCode(promoCodeObj);

            return Redirect("PromoCodeView");
        }
    }
}
