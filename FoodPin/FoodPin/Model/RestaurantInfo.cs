using System;

namespace FoodPin.Model
{
    public class RestaurantInfo : IRestaurantInfo
    {
        public RestaurantInfo(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
