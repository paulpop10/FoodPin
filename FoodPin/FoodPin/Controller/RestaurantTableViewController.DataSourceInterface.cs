using System;
using FoodPin.Extensions;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewController
    {       
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) as RestaurantTableViewCell;
            cell.SetWithRestaurant(_restaurants[indexPath.Row]);
            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _restaurants.Count;
        }
    }
}
