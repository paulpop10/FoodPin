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
    [Register ("RestaurantDetailSeparatorCell")]
    partial class RestaurantDetailSeparatorCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView Separator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SubsectionTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Separator != null) {
                Separator.Dispose ();
                Separator = null;
            }

            if (SubsectionTitle != null) {
                SubsectionTitle.Dispose ();
                SubsectionTitle = null;
            }
        }
    }
}