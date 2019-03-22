using System;
using System.Collections.Generic;
using FoodPin.Extensions;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewController : UITableViewController
    {
        private const string CellIdentifier = "datacell";
        #region Restaurant Data               
        private readonly List<Restaurant> _restaurants = new List<Restaurant>
            {
            new Restaurant(name: "Cafe Deadend", type: "Coffee & Tea Shop", location: "Hong Kong", image: "cafedeadend", isVisited: false),
            new Restaurant(name: "Homei", type: "Cafe", location: "Hong Kong", image: "homei", isVisited: false),
            new Restaurant(name: "Teakha", type: "Tea House", location: "Hong Kong", image: "teakha", isVisited: false),
            new Restaurant(name: "Cafe loisl", type: "Austrian / Causual Drink", location: "Hong Kong", image: "cafeloisl", isVisited: false),
            new Restaurant(name: "Petite Oyster", type: "French", location: "Hong Kong", image: "petiteoyster", isVisited: false),
            new Restaurant(name: "For Kee Restaurant", type: "Bakery", location: "Hong Kong", image: "forkeerestaurant", isVisited: false),
            new Restaurant(name: "Po's Atelier", type: "Bakery", location: "Hong Kong", image: "posatelier", isVisited: false),
            new Restaurant(name: "Bourke Street Backery", type: "Chocolate", location: "Sydney", image: "bourkestreetbakery", isVisited: false),
            new Restaurant(name: "Haigh's Chocolate", type: "Cafe", location: "Sydney", image: "haighschocolate", isVisited: false),
            new Restaurant(name: "Palomino Espresso", type: "American / Seafood", location: "Sydney", image: "palominoespresso", isVisited: false),
            new Restaurant(name: "Upstate", type: "American", location: "New York", image: "upstate", isVisited: false),
            new Restaurant(name: "Traif", type: "American", location: "New York", image: "traif", isVisited: false),
            new Restaurant(name: "Graham Avenue Meats", type: "Breakfast & Brunch", location: "New York", image: "grahamavenuemeats", isVisited: false),
            new Restaurant(name: "Waffle & Wolf", type: "Coffee & Tea", location: "New York", image: "wafflewolf", isVisited: false),
            new Restaurant(name: "Five Leaves", type: "Coffee & Tea", location: "New York", image: "fiveleaves", isVisited: false),
            new Restaurant(name: "Cafe Lore", type: "Latin American", location: "New York", image: "cafelore", isVisited: false),
            new Restaurant(name: "Confessional", type: "Spanish", location: "New York", image: "confessional", isVisited: false),
            new Restaurant(name: "Barrafina", type: "Spanish", location: "London", image: "barrafina", isVisited: false),
            new Restaurant(name: "Donostia", type: "Spanish", location: "London", image: "donostia", isVisited: false),
            new Restaurant(name: "Royal Oak", type: "British", location: "London", image: "royaloak", isVisited: false),
            new Restaurant(name: "CASK Pub and Kitchen", type: "Thai", location: "London", image: "caskpubkitchen", isVisited: false)
        };
        #endregion

        public RestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }
        #region View controller life cycle
        public override void ViewDidLoad()
        {
            TableView.CellLayoutMarginsFollowReadableWidth = true;
            if (NavigationController != null)
            {
                NavigationController.NavigationBar.PrefersLargeTitles = true;
            }         
        }
        #endregion
        #region Table View Delegate
        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var deleteAction = UIContextualAction.FromContextualActionStyle(
             UIContextualActionStyle.Destructive,
             "Delete",
             (Delete, sourceView, completionHndler) =>
             {
                 _restaurants.RemoveAt(indexPath.Row);

                 tableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);

                 completionHndler(true);
             });

            var shareAction = UIContextualAction.FromContextualActionStyle(
            UIContextualActionStyle.Normal,
            "Share",
            (Share, sourceView, completionHandler)
            =>
            {
                var activityItems = _restaurants[indexPath.Row].GetActivityItems();
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
            var checkInAlertActionTitle = _restaurants[indexPath.Row].IsVisited ? "Undo Check In" : "Check In";
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
            checkInAction.Image = _restaurants[indexPath.Row].GetCheckInImage();       
            var swipeconfiguration = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { checkInAction });
            return swipeconfiguration;
        }
        #endregion
        #region Navigation
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showRestaurantDetail")
            {              
                var indexpath = TableView.IndexPathForSelectedRow;
                if (indexpath != null)
                {
                    var destinationController = segue.DestinationViewController as RestaurantDetailViewController;
                    destinationController.Restaurant = _restaurants[indexpath.Row];           
                }
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
        #endregion
    }
}
