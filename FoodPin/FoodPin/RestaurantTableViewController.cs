using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewController : UITableViewController
    {
        private const string CellIdentifier = "datacell";
        private readonly List<string> _restaurantNames = new List<string>
        {
        "Cafe Deadend", "Homei", "Teakha", "Cafe Loisl", "Petite Oyster",
        "For Kee Restaurant", "Po's Atelier", "Bourke Street Bakery",
        "Haigh's Chocolate", "Palomino Espresso", "Upstate", "Traif",
        "Graham Avenue Meats", "Waffle &Wolf", "Five Leaves", "Cafe Lore",
        "Confessional", "Barrafina", "Donostia", "RoyalOak", "CASK Pub and Kitchen"
        };

        private readonly List<string> _restaurantImages = new List<string>
        {
        "cafedeadend", "homei", "teakha", "cafeloisl", "petiteoyster",
        "forkeerestaurant", "posatelier", "bourkestreetbakery", "haighschocolate",
        "palominoespresso", "upstate", "traif", "grahamavenuemeats", "wafflewolf",
        "fiveleaves", "cafelore", "confessional", "barrafina",
        "donostia", "royaloak", "caskpubkitchen"
        };

        private readonly List<string> _restaurantLocations = new List<string>
        {
            "Hong Kong", "Hong Kong", "Hong Kong", "Hong Kong",
             "Hong Kong", "Hong Kong", "Hong Kong", "Sydney", "Sydney",
             "Sydney", "New York", "NewYork", "New York", "New York", "New York",
             "New York", "New York", "London", "London", "London", "London"
        };

        private readonly List<string> _restaurantTypes = new List<string>
        {
            "Coffee & Tea Shop", "Cafe", "Tea House", "Austrian / Causual Drink",
            "French", "Bakery", "Bakery", "Chocolate", "Cafe", "American / Seafood",
            "American", "American", "Breakfast & Brunch", "Coffee & Tea", "Coffee & Tea",
            "Latin American", "Spanish", "Spanish", "Spanish", "British", "Thai"
        };

        private List<bool> _restaurantIsVisited = new List<bool>
        {
            false, false, false, false, false, false, false,
            false, false, false, false, false, false, false,
            false, false, false, false, false, false, false,
        };

        public RestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) as RestaurantTableViewCell;
            cell.SetName(_restaurantNames[indexPath.Row]);
            cell.SetImage(UIImage.FromBundle(_restaurantImages[indexPath.Row]));
            cell.SetLocation(_restaurantLocations[indexPath.Row]);
            cell.SetType(_restaurantTypes[indexPath.Row]);
            CheckIn(cell, _restaurantIsVisited[indexPath.Row]);
            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _restaurantNames.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var optionMenu = UIAlertController.Create(null, "What do you want to do?", UIAlertControllerStyle.ActionSheet);
            SetUpPopover(optionMenu, tableView.CellAt(indexPath));
            var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
            optionMenu.AddAction(cancelAction);
            var callAction = UIAlertAction.Create("Call " + "123-000-" + indexPath.Row, UIAlertActionStyle.Default, OnCallActionSelected);
            optionMenu.AddAction(callAction);
            PresentViewController(optionMenu, true, null);
            tableView.DeselectRow(indexPath, false);
        }

        public override void ViewDidLoad()
        {
            TableView.CellLayoutMarginsFollowReadableWidth = true;
        }

        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var deleteAction = UIContextualAction.FromContextualActionStyle(
             UIContextualActionStyle.Destructive,
             "Delete",
             (Delete, sourceView, completionHndler) =>
             {
                 _restaurantNames.RemoveAt(indexPath.Row);
                 _restaurantLocations.RemoveAt(indexPath.Row);
                 _restaurantTypes.RemoveAt(indexPath.Row);
                 _restaurantIsVisited.RemoveAt(indexPath.Row);
                 _restaurantImages.RemoveAt(indexPath.Row);

                 tableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);

                 completionHndler(true);
             });

            var shareAction = UIContextualAction.FromContextualActionStyle(
            UIContextualActionStyle.Normal,
            "Share",
            (Share, sourceView, completionHandler)
            =>
            {
                var defaultText = "Just checking in at " + _restaurantNames[indexPath.Row];
                var activityitems = new NSObject[] { };
                var imageToLoad = UIImage.FromBundle(_restaurantImages[indexPath.Row]);
                if (imageToLoad != null)
                {
                    activityitems = new NSObject[] { FromObject(new NSString(defaultText)), imageToLoad };
                }
                else
                {
                    activityitems = new NSObject[] { FromObject(new NSString(defaultText)) };
                }

                var activityController = new UIActivityViewController(activityitems, null);
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
            var checkInAlertActionTitle = _restaurantIsVisited[indexPath.Row] ? "Undo Check In" : "Check In";
            var checkInAction = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                checkInAlertActionTitle,
                (Check_in, sourceView, completionHandler)
                =>
                {
                    var cell = tableView.CellAt(indexPath) as RestaurantTableViewCell;
                    if (cell != null)
                    {
                        _restaurantIsVisited[indexPath.Row] = !_restaurantIsVisited[indexPath.Row];
                        CheckIn(cell, _restaurantIsVisited[indexPath.Row]);
                    }
                });
            checkInAction.BackgroundColor = UIColor.FromRGB(0, 190, 0);
            checkInAction.Image = _restaurantIsVisited[indexPath.Row] ? UIImage.FromBundle("undo") : UIImage.FromBundle("tick");         
            var swipeconfiguration = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { checkInAction });
            return swipeconfiguration;
        }

        private void OnCallActionSelected(UIAlertAction obj)
        {
            var alertmessage = UIAlertController.Create("Service Unavailable", "Sorry, the call feature is not available yet.Please retry later.", UIAlertControllerStyle.Alert);
            alertmessage.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alertmessage, true, null);
        }

        private void CheckIn(RestaurantTableViewCell cell, bool check)
        {
            if (check)
            {
                var checkMarkImage = UIImage.FromBundle("CheckmarkImageView");
                cell.AccessoryView = new UIImageView(checkMarkImage);              
            }
            else
            {
                cell.AccessoryView = null;
            }
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
    }
}
