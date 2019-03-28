using System;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class UINavigationControllerWithTopViewControllerStatusBar : UINavigationController
    {
        public UINavigationControllerWithTopViewControllerStatusBar(IntPtr handle) : base(handle)
        {
        }

        public override UIViewController ChildViewControllerForStatusBarStyle()
        {
            return TopViewController;
        }
    }
}