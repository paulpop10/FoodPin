// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FoodPin
{
    [Register ("RestaurantDetailViewController")]
    partial class RestaurantDetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView RestaurantImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantLocationLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantTypeLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RestaurantImageView != null) {
                RestaurantImageView.Dispose ();
                RestaurantImageView = null;
            }

            if (RestaurantLocationLabel != null) {
                RestaurantLocationLabel.Dispose ();
                RestaurantLocationLabel = null;
            }

            if (RestaurantNameLabel != null) {
                RestaurantNameLabel.Dispose ();
                RestaurantNameLabel = null;
            }

            if (RestaurantTypeLabel != null) {
                RestaurantTypeLabel.Dispose ();
                RestaurantTypeLabel = null;
            }
        }
    }
}