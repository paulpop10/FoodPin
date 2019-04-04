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
    [Register ("NewRestaurantDetailViewController")]
    partial class NewRestaurantDetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView FooterView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FoodPin.RestaurantDetailHeaderView HeaderView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton RateButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FooterView != null) {
                FooterView.Dispose ();
                FooterView = null;
            }

            if (HeaderView != null) {
                HeaderView.Dispose ();
                HeaderView = null;
            }

            if (RateButton != null) {
                RateButton.Dispose ();
                RateButton = null;
            }
        }
    }
}