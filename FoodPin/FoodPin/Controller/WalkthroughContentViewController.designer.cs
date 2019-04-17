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
    [Register ("WalkthroughContentViewController")]
    partial class WalkthroughContentViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ContentImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel HeadingLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SubHeadingLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ContentImageView != null) {
                ContentImageView.Dispose ();
                ContentImageView = null;
            }

            if (HeadingLabel != null) {
                HeadingLabel.Dispose ();
                HeadingLabel = null;
            }

            if (SubHeadingLabel != null) {
                SubHeadingLabel.Dispose ();
                SubHeadingLabel = null;
            }
        }
    }
}