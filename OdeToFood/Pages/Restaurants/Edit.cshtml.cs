using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;
        [BindProperty]
        public Restaurant RestaurantOnPage { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int restaurantId)
        {
            UpdateCuisines();
            // Querying the datastream, loading it into our object
            RestaurantOnPage = restaurantData.GetById(restaurantId);

            if(RestaurantOnPage == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Checks against the object's model binding
            if(ModelState.IsValid)
            {
                restaurantData.Update(RestaurantOnPage);
                restaurantData.Commit();

                // Send them to the detail page of their current restaurant
                return RedirectToPage("./Detail", new { restaurantId = RestaurantOnPage.Id });
            }

            // The input wasn't valid, therefore we need to update the page again and reload it.
            UpdateCuisines();
            return Page();
        }

        /// <summary>
        /// Updates the enumerated list of cuisines for selection.
        /// </summary>
        private void UpdateCuisines()
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
        }
    }
}