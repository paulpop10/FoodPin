using System;

namespace FoodPin.Model
{
    public class RestaurantInfoWithIcon : IRestaurantInfo
    {
        public RestaurantInfoWithIcon(string text, string imageName)
        {
            Text = text;
            ImageName = imageName;
        }

        public string Text { get; }

        public string ImageName { get; }
    }
}
