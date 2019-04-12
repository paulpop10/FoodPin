using System;

namespace FoodPin.View
{
    public class WalkthroughViewControllerDelegate : IWalkthroughPageViewControllerDelegate
    {
        private readonly Action _updateUI;

        public WalkthroughViewControllerDelegate(Action updateUI)
        {
            _updateUI = updateUI;
        }

        public void DidUpdatePageIndex()
        {
            _updateUI();
        }
    }
}
