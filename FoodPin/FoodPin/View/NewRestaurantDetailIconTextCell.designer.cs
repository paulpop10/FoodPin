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
    [Register ("NewRestaurantDetailIconTextCell")]
    partial class NewRestaurantDetailIconTextCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView IconImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ShortTextLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (IconImageView != null) {
                IconImageView.Dispose ();
                IconImageView = null;
            }

            if (ShortTextLabel != null) {
                ShortTextLabel.Dispose ();
                ShortTextLabel = null;
            }
        }
    }
}