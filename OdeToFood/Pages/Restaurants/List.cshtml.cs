using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Data;
using OdeToFood.Core;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;

        public string Message { get; set; }
        // IO model for querying our restaurant list
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }
        
        public void OnGet() // You just have to pass the name of the input as a parameter and ASP.NET will actually search for it
        {
            Message = config["Hello!"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}