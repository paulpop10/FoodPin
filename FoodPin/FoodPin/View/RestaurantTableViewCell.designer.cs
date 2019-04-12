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
    [Register ("RestaurantTableViewCell")]
    partial class RestaurantTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LocationLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ThumbnailImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TypeLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LocationLabel != null) {
                LocationLabel.Dispose ();
                LocationLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (ThumbnailImageView != null) {
                ThumbnailImageView.Dispose ();
                ThumbnailImageView = null;
            }

            if (TypeLabel != null) {
                TypeLabel.Dispose ();
                TypeLabel = null;
            }
        }
    }
}