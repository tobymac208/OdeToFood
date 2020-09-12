using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class CreateModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly HtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant RestaurantOnPage { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public CreateModel(IRestaurantData restaurantData, HtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            // We need to use this
            this.htmlHelper = htmlHelper;
        }   

        public void OnGet()
        {
            // Load the page
            RefreshCuisineSelectBox();
        }

        public IActionResult OnPost()
        {
            // Check the fields against the data binding
            if(ModelState.IsValid)
            {
                restaurantData.Create(RestaurantOnPage);
            }

            RefreshCuisineSelectBox();
            return Page();
        }

        /// <summary>
        /// Loads all of the cuisine types onto the page.
        /// </summary>
        private void RefreshCuisineSelectBox()
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
        }
    }
}