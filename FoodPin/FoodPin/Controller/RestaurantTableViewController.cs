using System;
using System.Collections.Generic;
using FoodPin.Controller;
using FoodPin.Extensions;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewController : UITableViewController
    {
        private const string CellIdentifier = "datacell";
        private DataBaseConnection dataBaseConnection;
        private List<RestaurantMO> _restaurantsMO = new List<RestaurantMO>();

        public RestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }
        #region View controller life cycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            dataBaseConnection = DataBaseConnection.Instance;
            TableView.CellLayoutMarginsFollowReadableWidth = true;
            SetNavigationController();
            SetCustomFontForNavigationBar();

            TableView.BackgroundView = EmptyListImageView;
            if (TableView.BackgroundView != null)
            {
                TableView.BackgroundView.Hidden = true;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _restaurantsMO = dataBaseConnection.Conn.Table<RestaurantMO>().ToList();
            TableView.ReloadData();
            SetEmptyTableViewBackground();
            if (NavigationController != null)
            {
                NavigationController.HidesBarsOnSwipe = true;
            }
        }
        #endregion
        #region Table View Delegate
        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var deleteAction = UIContextualAction.FromContextualActionStyle(
             UIContextualActionStyle.Destructive,
             "Delete",
             (Delete, sourceView, completionHandler) =>
             {
                 var restaurantId = _restaurantsMO[indexPath.Row].Id;
                 dataBaseConnection.Conn.Table<RestaurantMO>().Delete(x => x.Id == restaurantId);
                 _restaurantsMO.RemoveAt(indexPath.Row);
                 SetEmptyTableViewBackground();
                 completionHandler(true);
             });

            var shareAction = UIContextualAction.FromContextualActionStyle(
            UIContextualActionStyle.Normal,
            "Share",
            (Share, sourceView, completionHandler)
            =>
            {
                var activityItems = _restaurantsMO[indexPath.Row].GetActivityItems();
                var activityController = new UIActivityViewController(activityItems, null);
                SetUpPopover(activityController, tableView.CellAt(indexPath));
                PresentViewController(activityController, true, null);
                completionHandler(true);
            });

            deleteAction.BackgroundColor = UIColor.FromRGB(231, 76, 60);
            deleteAction.Image = UIImage.FromBundle("delete");

            shareAction.BackgroundColor = UIColor.FromRGB(254, 149, 38);
            shareAction.Image = UIImage.FromBundle("share");
            var swipeConfiguration = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { deleteAction, shareAction });

            return swipeConfiguration;
        }

        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var checkInAlertActionTitle = _restaurantsMO[indexPath.Row].IsVisited ? "Undo Check In" : "Check In";
            var checkInAction = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                checkInAlertActionTitle,
                (Check_in, sourceView, completionHandler)
                =>
                {
                    var cell = tableView.CellAt(indexPath) as RestaurantTableViewCell;
                    if (cell != null)
                    {
                        cell.OnCheckInClicked();
                    }
                });
            checkInAction.BackgroundColor = UIColor.FromRGB(0, 190, 0);
            checkInAction.Image = _restaurantsMO[indexPath.Row].GetCheckInImage();
            var swipeconfiguration = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { checkInAction });
            return swipeconfiguration;
        }

        #endregion
        #region Navigation
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            if (segue.Identifier == "showRestaurantDetail")
            {
                var indexpath = TableView.IndexPathForSelectedRow;
                if (indexpath != null)
                {
                    var destinationController = segue.DestinationViewController as NewRestaurantDetailViewController;
                    destinationController.RestaurantMO = _restaurantsMO[indexpath.Row];
                }
            }
            else
            if (segue.Identifier == "addRestaurant")
            {
                var destinationController = segue.DestinationViewController as AddRestaurantNavigationController;
                var addRestaurantTableViewController = destinationController.TopViewController as AddRestaurantTableViewController;
                addRestaurantTableViewController.AddRestaurantCloseDelegate = UnwindToHome;
            }
        }
        #endregion
        #region Extra methods
        private void OnCallActionSelected(UIAlertAction obj)
        {
            var alertmessage = UIAlertController.Create("Service Unavailable", "Sorry, the call feature is not available yet.Please retry later.", UIAlertControllerStyle.Alert);
            alertmessage.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alertmessage, true, null);
        }

        private void SetUpPopover(UIViewController uiViewController, UITableViewCell cell)
        {
            var popoverController = uiViewController.PopoverPresentationController;
            if (popoverController != null)
            {
                if (cell != null)
                {
                    popoverController.SourceView = cell;
                    popoverController.SourceRect = cell.Bounds;
                }
            }
        }

        private void SetNavigationController()
        {
            NavigationController?.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            if (NavigationController != null)
            {
                NavigationController.NavigationBar.ShadowImage = new UIImage();
                NavigationController.HidesBarsOnSwipe = true;
                NavigationController.NavigationBar.PrefersLargeTitles = true;
            }
        }

        private void SetCustomFontForNavigationBar()
        {
            var customFont = UIFont.FromName("Arial", 40);
            if (customFont != null)
            {
                if (NavigationController != null)
                {
                    NavigationController.NavigationBar.LargeTitleTextAttributes = new UIStringAttributes
                    {
                        ForegroundColor = UIColor.FromRGB(231, 76, 60),
                        Font = customFont
                    };
                }
            }
        }

        private void UnwindToHome()
        {
            DismissViewController(true, null);
        }

        private void SetEmptyTableViewBackground()
        {
            if (_restaurantsMO.Count > 0)
            {
                if (TableView.BackgroundView != null)
                {
                    TableView.BackgroundView.Hidden = true;
                }

                TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            }
            else
            {
                if (TableView.BackgroundView != null)
                {
                    TableView.BackgroundView.Hidden = false;
                }

                TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            }
        }
        #endregion
    }
}
