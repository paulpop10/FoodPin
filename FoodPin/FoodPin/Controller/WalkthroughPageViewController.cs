using System;
using FoodPin.View;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class WalkthroughPageViewController : UIPageViewController
    {
        private readonly string[] _pageHeadings = { "CREATE YOUR OWN FOOD GUIDE", "SHOW YOU THE LOCATION", "DISCOVER GREAT RESTAURANTS" };
        private readonly string[] _pageImages = { "onboarding-1", "onboarding-2", "onboarding-3" };
        private readonly string[] _pageSubHeadings = { "Pin your favorite restaurants and create your own food guide", "Search and locate your favourite restaurant on Maps", "Find restaurants shared by your friends and other foodies" };

        public WalkthroughPageViewController(IntPtr handle) : base(handle)
        {
        }

        public int CurrentIndex { get; set; }

        public IWalkthroughPageViewControllerDelegate IWalkthroughDelegate { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DataSource = new PageViewControllerDataSource(this);
            Delegate = new PageViewControllerDelegate(this, UpdateIndex);
            CreateStartingPageContent();
        }

        public WalkthroughContentViewController CreateContentViewController(int index)
        {
            if (index < 0 || index >= _pageHeadings.Length)
            {
                return null;
            }

            var storyboard = UIStoryboard.FromName("Onboarding", null);
            var pageContentViewcontroller = storyboard.InstantiateViewController(nameof(WalkthroughContentViewController)) as WalkthroughContentViewController;
            if (pageContentViewcontroller != null)
            {
                pageContentViewcontroller.ImageFile = _pageImages[index];
                pageContentViewcontroller.Heading = _pageHeadings[index];
                pageContentViewcontroller.SubHeading = _pageSubHeadings[index];
                pageContentViewcontroller.Index = index;
                return pageContentViewcontroller;
            }

            return null;
        }

        public void ForwardPage()
        {
            CurrentIndex++;
            var nextViewController = CreateContentViewController(CurrentIndex);
            if (nextViewController != null)
            {
                SetViewControllers(new[] { nextViewController }, UIPageViewControllerNavigationDirection.Forward, true, null);
            }
        }

        private void UpdateIndex()
        {
            IWalkthroughDelegate.DidUpdatePageIndex();
        }

        private void CreateStartingPageContent()
        {
            var startingViewController = CreateContentViewController(0);
            if (startingViewController != null)
            {
                SetViewControllers(new[] { startingViewController }, UIPageViewControllerNavigationDirection.Forward, true, null);
            }
        }
    }
}