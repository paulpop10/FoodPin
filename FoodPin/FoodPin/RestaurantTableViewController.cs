using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewController : UITableViewController
    {
        private const string CellIdentifier = "datacell";
        private readonly List<string> _restaurantNames = new List<string>
        {
        "Cafe Deadend", "Homei", "Teakha", "Cafe Loisl", "Petite Oyster",
        "For Kee Restaurant", "Po's Atelier", "Bourke Street Bakery",
        "Haigh's Chocolate", "Palomino Espresso", "Upstate", "Traif",
        "Graham Avenue Meats", "Waffle &Wolf", "Five Leaves", "Cafe Lore",
        "Confessional", "Barrafina", "Donostia", "RoyalOak", "CASK Pub and Kitchen"
        };

        private readonly List<string> _restaurantImages = new List<string>
        {
        "cafedeadend", "homei", "teakha", "cafeloisl", "petiteoyster",
        "forkeerestaurant", "posatelier", "bourkestreetbakery", "haighschocolate",
        "palominoespresso", "upstate", "traif", "grahamavenuemeats", "wafflewolf",
        "fiveleaves", "cafelore", "confessional", "barrafina",
        "donostia", "royaloak", "caskpubkitchen"
        };

        private readonly List<string> _restaurantLocations = new List<string>
        {
            "Hong Kong", "Hong Kong", "Hong Kong", "Hong Kong",
             "Hong Kong", "Hong Kong", "Hong Kong", "Sydney", "Sydney",
             "Sydney", "New York", "NewYork", "New York", "New York", "New York",
             "New York", "New York", "London", "London", "London", "London"
        };

        private readonly List<string> _restaurantTypes = new List<string>
        {
            "Coffee & Tea Shop", "Cafe", "Tea House", "Austrian / Causual Drink", 
            "French", "Bakery", "Bakery", "Chocolate", "Cafe", "American / Seafood", 
            "American", "American", "Breakfast & Brunch", "Coffee & Tea", "Coffee & Tea",
            "Latin American", "Spanish", "Spanish", "Spanish", "British", "Thai"
        };

        public RestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
         {
            var cell = tableView.DequeueReusableCell(CellIdentifier) as RestaurantTableViewCell;
            cell.SetName(_restaurantNames[indexPath.Row]);
            cell.SetImage(UIImage.FromBundle(_restaurantImages[indexPath.Row]));
            cell.SetLocation(_restaurantLocations[indexPath.Row]);
            cell.SetType(_restaurantTypes[indexPath.Row]);
            return cell;
         }

         public override nint NumberOfSections(UITableView tableView)
         {
             return 1;
         }

         public override nint RowsInSection(UITableView tableView, nint section)
         {
             return _restaurantNames.Count;
         }
    }
}