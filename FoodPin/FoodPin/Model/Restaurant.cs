using System;

namespace FoodPin
{
    public class Restaurant
    {
        public Restaurant(string name, string type, string location, string phone, string description, string image, bool isVisited, string rating = "")
        {
            Name = name;
            Type = type;
            Location = location;
            Phone = phone;
            Description = description;
            Image = image;
            IsVisited = isVisited;
            Rating = rating;
        }

        public Restaurant()
        {
        }

        public string Name { get; }

        public string Type { get; }

        public string Location { get; }

        public string Phone { get; }

        public string Description { get; }

        public string Image { get; }

        public bool IsVisited { get; set; }

        public string Rating { get; set; }
    }
}
