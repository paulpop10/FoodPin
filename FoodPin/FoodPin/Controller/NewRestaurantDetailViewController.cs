using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using FoodPin.Extensions;
using FoodPin.Model;
using FoodPin.Controller;
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

        public RestaurantMO RestaurantMO { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            SetHeaderView();
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            SetNavigationController();
            CreateRestaurantInfoList();
            HeaderView.SetRatingImageView(UIImage.FromBundle(RestaurantMO.Rating));
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
                restaurantMapCell.Configure(RestaurantMO.Location);
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
            base.ViewWillAppear(animated);
            if (NavigationController != null)
            {
                NavigationController.HidesBarsOnSwipe = false;
                NavigationController.SetNavigationBarHidden(false, true);
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            if (segue.Identifier == "showMap")
            {
                var destinationcontroller = segue.DestinationViewController as MapViewController;
                destinationcontroller.RestaurantMO = RestaurantMO;
            } 
            else if (segue.Identifier == "showReview")
            {
                var destinationController = segue.DestinationViewController as ReviewViewController;
                destinationController.RestaurantMO = RestaurantMO;
                destinationController.CloseDelegate = CloseSegue;
                destinationController.RateDelegate = RateRestaurant;
            }
        }

        private void CloseSegue()
        {
            DismissViewController(true, null);
        }

        private void RateRestaurant(string rate)
        {
            var dataBaseConnection = DataBaseConnection.Instance;
            RestaurantMO.Rating = rate;
            DismissViewController(true, () =>
           {
               HeaderView.SetRatingImageView(UIImage.FromBundle(rate));
           });
            dataBaseConnection.Conn.Update(RestaurantMO);
        }

        private void CreateRestaurantInfoList()
        {
            _restaurantInfoList = new List<IRestaurantInfo>
            {
                new RestaurantInfoWithIcon(RestaurantMO.Phone, "phone"),
                new RestaurantInfoWithIcon(RestaurantMO.Location, "map"),
                new RestaurantInfo(RestaurantMO.Summary),
                new RestaurantMapText("HOW TO GET HERE"),
                new RestaurantMap()
            };
        }

        private void SetHeaderView()
        {
            HeaderView.SetNameLabel(RestaurantMO.Name);
            HeaderView.SetTypeLabel(RestaurantMO.Type);
            HeaderView.SetHeaderImageView(RestaurantMO.GetImage());
            HeaderView.SetHeartImageView(RestaurantMO.GetCheckmarkImage());
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
