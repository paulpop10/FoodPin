using System;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantDetailViewController : UIViewController
    {
        public RestaurantDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public string RestaurantImageName { get; set; }

        public string RestaurantName { get; set; }

        public string RestaurantType { get; set; }

        public string RestaurantLocation { get; set; }

        public override void ViewDidLoad()
        {
            RestaurantImageView.Image = UIImage.FromBundle(RestaurantImageName);
            RestaurantNameLabel.Text = RestaurantName;
            RestaurantTypeLabel.Text = RestaurantType;
            RestaurantLocationLabel.Text = RestaurantLocation;  
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
        }
    }
}