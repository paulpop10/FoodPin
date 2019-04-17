using System;
using UIKit;

namespace FoodPin.View
{
    public class PageViewControllerDataSource : UIPageViewControllerDataSource
    {
        private readonly WalkthroughPageViewController _walkthroughPageViewController;

        public PageViewControllerDataSource(WalkthroughPageViewController walkthroughPageViewController)
        {
            _walkthroughPageViewController = walkthroughPageViewController;
        }

        public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var index = (referenceViewController as WalkthroughContentViewController).Index;
            index++;
            return _walkthroughPageViewController.CreateContentViewController(index);
        }

        public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var index = (referenceViewController as WalkthroughContentViewController).Index;
            index--;
            return _walkthroughPageViewController.CreateContentViewController(index);
        }
    }
}
