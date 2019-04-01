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
            SetNavigationController();
            CreateRestaurantInfoList();
            TableView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;

        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            if (_restaurantInfoList[indexPath.Row] is RestaurantInfoWithIcon)             {                 var restaurantInfoWithIconCell = tableView.DequeueReusableCell(nameof(NewRestaurantDetailIconTextCell)) as NewRestaurantDetailIconTextCell;                 restaurantInfoWithIconCell.SetWithRestaurant(_restaurantInfoList[indexPath.Row] as RestaurantInfoWithIcon);                 return restaurantInfoWithIconCell;             }             else             if (_restaurantInfoList[indexPath.Row] is RestaurantInfo)             {                 var restaurantInfoCell = tableView.DequeueReusableCell(nameof(NewRestaurantDetailTextCell)) as NewRestaurantDetailTextCell;                 restaurantInfoCell.SetWithRestaurant(_restaurantInfoList[indexPath.Row] as RestaurantInfo);                 return restaurantInfoCell;             }             else
            if (_restaurantInfoList[indexPath.Row] is RestaurantMapText)             {
                var restaurantMapTextCell = tableView.DequeueReusableCell(nameof(RestaurantDetailSeparatorCell)) as RestaurantDetailSeparatorCell;
                restaurantMapTextCell.SetWithRestaurant(_restaurantInfoList[indexPath.Row] as RestaurantMapText);
                return restaurantMapTextCell;             }
            else
            if (_restaurantInfoList[indexPath.Row] is RestaurantMap)
            {
                var restaurantMapCell = tableView.DequeueReusableCell(nameof(RestaurantDetailMapCell)) as RestaurantDetailMapCell;
                restaurantMapCell.Configure(Restaurant.Location);
                return restaurantMapCell;
            }
            else
            {
                return null;
            }
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _restaurantInfoList.Count;
        }

        public override void ViewWillAppear(bool animated)
        {
            if (NavigationController != null)
            {
                NavigationController.HidesBarsOnSwipe = false;
                NavigationController.SetNavigationBarHidden(false, true);
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showMap")
            {
                var destinationcontroller = segue.DestinationViewController as MapViewController;
                destinationcontroller.Restaurant = Restaurant;
            }
        }

        private void CreateRestaurantInfoList()
        {
            _restaurantInfoList = new List<IRestaurantInfo>
            {
                new RestaurantInfoWithIcon(Restaurant.Phone, "phone"),
                new RestaurantInfoWithIcon(Restaurant.Location, "map"),
                new RestaurantInfo(Restaurant.Description),
                new RestaurantMapText("HOW TO GET HERE"),
                new RestaurantMap()
            };
        }

        private void SetHeaderView()
        {
            HeaderView.SetNameLabel(Restaurant.Name);
            HeaderView.SetTypeLabel(Restaurant.Type);
            HeaderView.SetHeaderImageView(Restaurant.GetImage());
            HeaderView.SetHeartImageView(Restaurant.GetCheckmarkImage());
        }

        private void SetNavigationController()
        {
            NavigationController?.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            if (NavigationController != null)
            {
                NavigationController.NavigationBar.ShadowImage = new UIImage();
                NavigationController.NavigationBar.TintColor = UIColor.White;
                NavigationController.HidesBarsOnSwipe = false;
            }
        }
    }
}
