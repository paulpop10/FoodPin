using System;

namespace FoodPin.Model
{
    public class RestaurantMapText : IRestaurantInfo
    {
        public RestaurantMapText(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
