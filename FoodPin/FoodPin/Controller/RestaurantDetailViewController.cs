using System;
using FoodPin.Extensions;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantDetailViewController : UIViewController
    {
        public RestaurantDetailViewController(IntPtr handle) : base(handle)
        {
        }

        public Restaurant Restaurant { get; set; }

        public override void ViewDidLoad()
        {
            RestaurantImageView.Image = Restaurant?.GetImage();
            RestaurantNameLabel.Text = Restaurant?.Name;
            RestaurantTypeLabel.Text = Restaurant?.Type;
            RestaurantLocationLabel.Text = Restaurant?.Location;
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
        }
    }
}