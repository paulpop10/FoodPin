using System;
using System.Collections.Generic;
using System.Linq;
using FoodPin.Extensions;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class NewRestaurantDetailViewController : UITableViewController
    {
        private List<IRestaurantInfo> _restaurantInfoList;

        public NewRestaurantDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public Restaurant Restaurant { get; set; }

        public override void ViewDidLoad()
        {
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            SetHeaderView();
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            CreateRestaurantInfoList();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (_restaurantInfoList[indexPath.Row] is RestaurantInfoWithIcon)             {                 var restaurantInfoWithIconCell = tableView.DequeueReusableCell(nameof(NewRestaurantDetailIconTextCell)) as NewRestaurantDetailIconTextCell;                 restaurantInfoWithIconCell.SetWithRestaurant(_restaurantInfoList[indexPath.Row] as RestaurantInfoWithIcon);                 return restaurantInfoWithIconCell;             }             else             if (_restaurantInfoList[indexPath.Row] is RestaurantInfo)             {                 var restaurantInfoCell = tableView.DequeueReusableCell(nameof(NewRestaurantDetailTextCell)) as NewRestaurantDetailTextCell;                 restaurantInfoCell.SetWithRestaurant(_restaurantInfoList[indexPath.Row] as RestaurantInfo);                 return restaurantInfoCell;             }             else             {                 return null;             }
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _restaurantInfoList.Count();
        }

        private void CreateRestaurantInfoList()         {             _restaurantInfoList = new List<IRestaurantInfo>             {                 new RestaurantInfoWithIcon(Restaurant.Phone, "phone"),                 new RestaurantInfoWithIcon(Restaurant.Location, "map"),                 new RestaurantInfo(Restaurant.Description)             };         }          private void SetHeaderView()         {             HeaderView.SetNameLabel(Restaurant.Name);             HeaderView.SetTypeLabel(Restaurant.Type);             HeaderView.SetHeaderImageView(Restaurant.GetImage());             HeaderView.SetHeartImageView(Restaurant.GetCheckmarkImage());         } 
    }
}
