using HotelManagementSystem.Domain.Models;
using System.Collections.Generic;

/*
 * Owner of IPromoCodeService Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain
{
    public interface IPromoCodeService
    {
        /*
         * <summary>
         * Search and retrieve all existing promo code
         * </summary>
         * <returns>A Enumerable list of promo code records<returns>
         */
        IEnumerable<PromoCode> GetAllPromoCode();

        /*
         * <summary>
         * Search and retrieve the existing promo code by promo code
         * </summary>
         * <param> promoCode, the promo code string </param>
         * <returns>A promo code object<returns>
         */
        PromoCode GetPromoCode(string promoCode);

        /*
         * <summary>
         * Create a promo code based on the promo code object
         * </summary>
         * <param> promoCode, a promo code object </param>
         * <returns>true, if promo code is created successfully<returns>
         */
        bool CreatePromoCode(PromoCode promoCode);

        /*
         * <summary>
         * Delete a promo code based on the promo code id
         * </summary>
         * <param> id, unique id of promo code </param>
         * <returns>true, if promo code is deleted successfully<returns>
         */
        bool DeletePromoCode(int id);

        /*
         * <summary>
         * Update a promo code based on the promo code object
         * </summary>
         * <param> promoCode, a promo code object </param>
         * <returns>true, if promo code is updated successfully<returns>
         */
        bool UpdatePromoCode(PromoCode promoCode);
    }
}
