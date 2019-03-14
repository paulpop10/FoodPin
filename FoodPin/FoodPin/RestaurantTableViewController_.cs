using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewController : UITableViewController
    {
        private List<string> restaurantNames = new List<string>
        {
        "Cafe Deadend", "Homei", "Teakha", "Cafe Loisl", "Petite Oyster",
        "For Kee Restaurant", "Po's Atelier", "Bourke Street Bakery",
        "Haigh's Chocolate", "Palomino Espresso", "Upstate", "Traif",
        "Graham Avenue Meats", "Waffle &Wolf", "Five Leaves", "Cafe Lore",
        "Confessional", "Barrafina", "Donostia", "RoyalOak", "CASK Pub and Kitchen"
        };

        private List<string> restaurantImages = new List<string>
        {
        "cafedeadend", "homei", "teakha", "cafeloisl", "petiteoyster",
        "forkeerestaurant", "posatelier", "bourkestreetbakery", "haighschocolate",
        "palominoespresso", "upstate", "traif", "grahamavenuemeats", "wafflewolf", 
        "fiveleaves", "cafelore", "confessional", "barrafina", 
        "donostia", "royaloak", "caskpubkitchen"
        };

        public RestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            const string CellIdentifier = "datacell";
            var cell = tableView.DequeueReusableCell(CellIdentifier);

            if (cell.TextLabel != null)
            {
                cell.TextLabel.Text = restaurantNames[indexPath.Row];
            }

            if (cell.ImageView != null)
            {
                cell.ImageView.Image = UIImage.FromBundle(restaurantImages[indexPath.Row]);
            }

            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return restaurantNames.Count;
        }
    }
}