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
            var restaurant = _searchController.Active ? _searchResultsMO[indexPath.Row]  : _restaurantsMO[indexPath.Row];
            cell.SetWithRestaurant(restaurant);
            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _searchController.Active ? _searchResultsMO.Count : _restaurantsMO.Count;
        }
             
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return !_searchController.Active;
        }
    }
}
