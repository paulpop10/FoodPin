using System;
using SQLite;

namespace FoodPin.Model
{
    public class RestaurantMO
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Phone { get; set; }

        public string Location { get; set; }

        public string Summary { get; set; }

        public bool IsVisited { get; set; }

        public string Rating { get; set; }

        public byte[] Image { get; set; }
    }
}
