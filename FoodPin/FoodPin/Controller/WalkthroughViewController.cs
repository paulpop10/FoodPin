using System;
using FoodPin.View;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class WalkthroughViewController : UIViewController
    {
        private const string ViewedWalkthrough = "hasViewedWalkthrough";
        private const string NextButttonBeginningTitle = "NEXT";
        private const string NextButtonEndingTitle = "GET STARTED";
        private WalkthroughPageViewController _walkthroughPageViewController;

        public WalkthroughViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CustomizeNextButton();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            AddButtonsClickListeners();
        }

        public override void ViewWillDisappear(bool animated)
        {
            RemoveButtonsClickListeners();
            base.ViewWillDisappear(animated);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            var walkthroughViewControllerDelegate = new WalkthroughViewControllerDelegate(UpdateCurrentPage);
            var destination = segue.DestinationViewController;
            var pageViewController = destination as WalkthroughPageViewController;
            if (pageViewController != null)
            {
                _walkthroughPageViewController = pageViewController;
                _walkthroughPageViewController.IWalkthroughDelegate = walkthroughViewControllerDelegate;
            }
        }

        private void CustomizeNextButton() 
        {
            NextButton.Layer.CornerRadius = 25;
            NextButton.Layer.MasksToBounds = true;
        }

        private void AddNextButtonClickListener()
        {
            NextButton.TouchUpInside += OnNextButtonClicked;
        }

        private void AddSkipButtonClickListener()
        {
            SkipButton.TouchUpInside += OnSkipButtonClicked;
        }

        private void RemoveNextButtonClickListener()
        {
            NextButton.TouchUpInside -= OnNextButtonClicked;
        }

        private void RemoveSkipButtonClickListener()
        {
            SkipButton.TouchUpInside -= OnSkipButtonClicked;
        }

        private void OnSkipButtonClicked(object sender, EventArgs e)
        {
            NSUserDefaults.StandardUserDefaults.SetBool(true, ViewedWalkthrough);
            DismissViewController(true, null);
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            var index = _walkthroughPageViewController?.CurrentIndex;
            if (index != null)
            {
                switch (index)
                {
                    case 0:
                        _walkthroughPageViewController?.ForwardPage();
                        break;
                    case 1:
                        _walkthroughPageViewController?.ForwardPage();
                        break;
                    case 2:
                        NSUserDefaults.StandardUserDefaults.SetBool(true, ViewedWalkthrough);
                        DismissViewController(true, null);
                        break;
                    default:
                        break;
                }
            }

            UpdateCurrentPage();
        }

        private void UpdateCurrentPage()
        {
            var index = _walkthroughPageViewController?.CurrentIndex;
            if (index != null)
            {
                switch (index)
                {
                    case 0:
                        NextButton.SetTitle(NextButttonBeginningTitle, UIControlState.Normal);
                        SkipButton.Hidden = false;
                        break;
                    case 1:
                        NextButton.SetTitle(NextButttonBeginningTitle, UIControlState.Normal);
                        SkipButton.Hidden = false;
                        break;
                    case 2:
                        NextButton.SetTitle(NextButtonEndingTitle, UIControlState.Normal);
                        SkipButton.Hidden = true;
                        break;
                    default:
                        break;
                }

                PageControl.CurrentPage = (System.nint)index;
            }
        }

        private void AddButtonsClickListeners()
        {
            AddNextButtonClickListener();
            AddSkipButtonClickListener();
        }

        private void RemoveButtonsClickListeners()
        {
            RemoveNextButtonClickListener();
            RemoveSkipButtonClickListener();
        }
    }
}