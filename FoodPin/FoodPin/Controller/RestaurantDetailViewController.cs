using System;
using System.Collections.Generic;
using FoodPin.Extensions;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantDetailViewController : UITableViewController
    {
        private List<IRestaurantInfo> restaurantInfoList = new List<IRestaurantInfo>();

        public RestaurantDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public Restaurant Restaurant { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();            
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            this.CreateRestaurantInfoList();
            TableView.DataSource = this;
            TableView.Delegate = this;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            var headerView = new RestaurantDetailHeaderView();
            headerView.SetNameLabel(Restaurant.Name);
            headerView.SetTypeLabel(Restaurant.Type);
            headerView.SetHeaderImageView(Restaurant.GetImage());
            UIImage image = Restaurant.IsVisited ? UIImage.FromBundle("CheckmarkImageView") : null;
            headerView.SetHeartImageView(image);
            return headerView;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return UITableView.AutomaticDimension;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return string.Empty;
        }

        private void CreateRestaurantInfoList()
        {
            restaurantInfoList = new List<IRestaurantInfo>
            {
               new RestaurantInfoWithIcon("phone", Restaurant.Phone),
                new RestaurantInfoWithIcon("map", Restaurant.Location),
                new RestaurantInfo(Restaurant.Description),
            };    
        }
    }
}