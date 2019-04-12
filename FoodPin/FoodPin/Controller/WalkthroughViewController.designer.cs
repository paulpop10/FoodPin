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
    [Register ("WalkthroughViewController")]
    partial class WalkthroughViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton NextButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPageControl PageControl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SkipButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (NextButton != null) {
                NextButton.Dispose ();
                NextButton = null;
            }

            if (PageControl != null) {
                PageControl.Dispose ();
                PageControl = null;
            }

            if (SkipButton != null) {
                SkipButton.Dispose ();
                SkipButton = null;
            }
        }
    }
}