using System;

namespace FoodPin
{
    public class Restaurant
    {
        public Restaurant(string name, string type, string location, string image, bool isVisited)
        {
            Name = name;
            Type = type;
            Location = location;
            Image = image;
            IsVisited = isVisited;
        }

        public string Name { get; }

        public string Type { get; }

        public string Location { get; }

        public string Image { get; }

        public bool IsVisited { get; set; }
    }
}
