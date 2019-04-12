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
    [Register ("ReviewViewController")]
    partial class ReviewViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AngryButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView BackgroundImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CoolButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ExitButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton HappyButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LoveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SadButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AngryButton != null) {
                AngryButton.Dispose ();
                AngryButton = null;
            }

            if (BackgroundImageView != null) {
                BackgroundImageView.Dispose ();
                BackgroundImageView = null;
            }

            if (CoolButton != null) {
                CoolButton.Dispose ();
                CoolButton = null;
            }

            if (ExitButton != null) {
                ExitButton.Dispose ();
                ExitButton = null;
            }

            if (HappyButton != null) {
                HappyButton.Dispose ();
                HappyButton = null;
            }

            if (LoveButton != null) {
                LoveButton.Dispose ();
                LoveButton = null;
            }

            if (SadButton != null) {
                SadButton.Dispose ();
                SadButton = null;
            }
        }
    }
}