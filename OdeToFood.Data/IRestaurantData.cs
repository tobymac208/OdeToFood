using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        /// <summary>
        /// Gets a list of restaurants.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A list of items, depending on if there's a name passed or not.</returns>
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int? id);
        Restaurant Update(Restaurant restaurant);
        void Create(Restaurant newRestaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant(){Id = 1, Name = "Fogo De Chao", Location = "Chicago, USA", Cuisine = CuisineType.Italian},
                new Restaurant(){Id = 2, Name = "Domino's", Location = "Tennessee, USA", Cuisine = CuisineType.Italian},
                new Restaurant(){Id = 3, Name = "Fiesta Cancun", Location = "Main, USA", Cuisine = CuisineType.Mexican}
            };
        }

        /// <summary> Need a why to grab the data fron the .NET service </summary>
        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {


            return from r in restaurants
                where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                orderby r.Name
                select r;
        }

        /// <summary>
        /// Easily retrieves an item from our list by 'id'.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Restaurant GetById(int? id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant restaurantToUpdate)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == restaurantToUpdate.Id);
            if(restaurant != null)
            {
                restaurant.Name = restaurantToUpdate.Name;
                restaurant.Location = restaurantToUpdate.Location;
                restaurant.Cuisine = restaurantToUpdate.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public void Create(Restaurant newRestaurant)
        {
            newRestaurant.Id = restaurants.Count + 1;

            restaurants.Add(newRestaurant);
        }
    }
}