using System;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantDetailIconTextCell : UIView
    {
        public RestaurantDetailIconTextCell(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            NSBundle.MainBundle.LoadNib(nameof(RestaurantDetailIconTextCell), this, null);
            ContainerView.Frame = this.Bounds;
            AddSubview(ContainerView);
        }
    }
}