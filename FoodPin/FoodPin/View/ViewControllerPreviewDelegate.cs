using System;
using System.Collections.Generic;
using CoreGraphics;
using FoodPin.Model;
using UIKit;

namespace FoodPin.View
{
    public class ViewControllerPreviewDelegate : UIViewControllerPreviewingDelegate
    {
        private readonly RestaurantTableViewController _restaurantTableViewController;
        private readonly List<RestaurantMO> _restaurantsMO;

        public ViewControllerPreviewDelegate(RestaurantTableViewController restaurantTableViewController, List<RestaurantMO> restaurantsMO)
        {
            _restaurantTableViewController = restaurantTableViewController;
            _restaurantsMO = restaurantsMO;
        }

        public override void CommitViewController(IUIViewControllerPreviewing previewingContext, UIViewController viewControllerToCommit)
        {
            _restaurantTableViewController.ShowViewController(viewControllerToCommit, this);
        }

        public override UIViewController GetViewControllerForPreview(IUIViewControllerPreviewing previewingContext, CGPoint location)
        {
            var indexPath = _restaurantTableViewController.TableView.IndexPathForRowAtPoint(location);
            if (indexPath == null)
            {
                return null;
            }

            var cell = _restaurantTableViewController.TableView.CellAt(indexPath);
            if (cell == null)
            {
                return null;
            }

            var restaurantDetaiViewController = _restaurantTableViewController.Storyboard?.InstantiateViewController("RestaurantDetailViewController") as NewRestaurantDetailViewController;
            if (restaurantDetaiViewController == null)
            {
                return null;
            }

            var selectedRestaurant = _restaurantsMO[indexPath.Row];
            restaurantDetaiViewController.RestaurantMO = selectedRestaurant;
            restaurantDetaiViewController.PreferredContentSize = new CGSize(0, 460);
            previewingContext.SourceRect = cell.Frame;

            return restaurantDetaiViewController;
        }
    }
}
