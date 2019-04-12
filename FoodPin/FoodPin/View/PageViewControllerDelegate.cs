using System;
using UIKit;

namespace FoodPin.View
{
    public class PageViewControllerDelegate : UIPageViewControllerDelegate
    {
        private readonly WalkthroughPageViewController _walkthroughPageViewController;
        private readonly Action _updateIndexDelegate;

        public PageViewControllerDelegate(WalkthroughPageViewController walkthroughPageViewController, Action updateIndex)
        {
            _walkthroughPageViewController = walkthroughPageViewController;
            _updateIndexDelegate = updateIndex;
        }
       
        public override void DidFinishAnimating(UIPageViewController pageViewController, bool finished, UIViewController[] previousViewControllers, bool completed)
        {
            if (completed)
            {
                var contentViewController = pageViewController.ViewControllers[0] as WalkthroughContentViewController;
                if (contentViewController != null)
                {
                    _walkthroughPageViewController.CurrentIndex = contentViewController.Index;
                    _updateIndexDelegate();
                }
            }
        }
    }
}
