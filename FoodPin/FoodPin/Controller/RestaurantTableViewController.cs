using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using CoreSpotlight;
using FoodPin.Controller;
using FoodPin.Extensions;
using FoodPin.Model;
using FoodPin.View;
using Foundation;
using UIKit;
using UserNotifications;

namespace FoodPin
{
    public partial class RestaurantTableViewController : UITableViewController
    {
        private const string CellIdentifier = "datacell";
        private const string ViewedWalkthrough = "hasViewedWalkthrough";
        private DataBaseConnection _dataBaseConnection;
        private UISearchController _searchController;
        private List<RestaurantMO> _restaurantsMO = new List<RestaurantMO>();
        private IEnumerable<RestaurantMO> _searchResults;
        private List<RestaurantMO> _searchResultsMO;

        public RestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }
        #region View controller life cycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _dataBaseConnection = DataBaseConnection.Instance;
            TableView.CellLayoutMarginsFollowReadableWidth = true;
            SetNavigationController();
            SetCustomFontForNavigationBar();
            CustomizeTableView();
            CreateSearchBar();
            DefinesPresentationContext = true;
            CreateQuickActions();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            RetrieveRestaurantsList();
            SetEmptyTableViewBackground();
            EnablePeekAndPop();
            PrepareNotification();
            if (NavigationController != null)
            {
                NavigationController.HidesBarsOnSwipe = true;
            }

            _searchController.SearchBar.Hidden = false;
        }

        public override void ViewWillDisappear(bool animated)
        {
            if (_searchController != null && _searchController.SearchBar != null)
            {
                _searchController.SearchBar.Hidden = true;
            }

            base.ViewWillDisappear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (NSUserDefaults.StandardUserDefaults.BoolForKey(ViewedWalkthrough))
            {
                return;
            }

            var storyboard = UIStoryboard.FromName("Onboarding", null);
            var walkthroughViewController = storyboard.InstantiateViewController(nameof(WalkthroughViewController)) as WalkthroughViewController;
            if (walkthroughViewController != null)
            {
                PresentViewController(walkthroughViewController, true, null);
            }     
        }

        #endregion
        #region Table View Delegate
        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var deleteAction = UIContextualAction.FromContextualActionStyle(
             UIContextualActionStyle.Destructive,
             AppResources.Delete,
             (Delete, sourceView, completionHandler) =>
             {
                 var restaurantId = _restaurantsMO[indexPath.Row].Id;
                 _dataBaseConnection.Conn.Table<RestaurantMO>().Delete(x => x.Id == restaurantId);
                 _restaurantsMO.RemoveAt(indexPath.Row);
                 TableView.ReloadData();
                 PrepareNotification();
                 SetEmptyTableViewBackground();
                 completionHandler(true);
             });

            var shareAction = UIContextualAction.FromContextualActionStyle(
            UIContextualActionStyle.Normal,
            AppResources.Share,
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
            var checkInAlertActionTitle = _restaurantsMO[indexPath.Row].IsVisited ? AppResources.UndoCheckIn : AppResources.CheckIn;
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
                    destinationController.RestaurantMO = _searchController.Active ? _searchResultsMO[indexpath.Row] : _restaurantsMO[indexpath.Row];
                    destinationController.HidesBottomBarWhenPushed = true;
                }
            }
            else
            if (segue.Identifier == "addRestaurant")
            {
                var destinationController = segue.DestinationViewController as AddRestaurantNavigationController;
                var addRestaurantTableViewController = destinationController.TopViewController as AddRestaurantTableViewController;
                addRestaurantTableViewController.AddRestaurantCloseDelegate = UnwindToHome;
                addRestaurantTableViewController.HidesBottomBarWhenPushed = true;
            }
        }
        #endregion
        #region Extra methods
        private async void RetrieveRestaurantsList()
        {
            if (_dataBaseConnection != null)
            {
                _restaurantsMO = _dataBaseConnection.Conn.Table<RestaurantMO>().ToList();
            }
            ////Required by the async method
            await Task.Delay(1);
            InvokeOnMainThread(() => { TableView.ReloadData(); });
        }

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

        private void FilterContent(string searchedText)
        {
             _searchResults = from RestaurantMO in _restaurantsMO where (RestaurantMO.Name.StartsWith(searchedText, StringComparison.InvariantCultureIgnoreCase) || RestaurantMO.Location.Contains(searchedText, StringComparison.InvariantCultureIgnoreCase)) select RestaurantMO;
             _searchResultsMO = _searchResults.ToList<RestaurantMO>();
        }

        private void OnSearchTextUpdated(string searchText)
        {
            if (searchText != null)
            {
                FilterContent(searchText);
                TableView.ReloadData();
            }
        }

        private void CreateSearchBar()
        {
            _searchController = new UISearchController(searchResultsController: null);
            TableView.TableHeaderView = _searchController.SearchBar;
            _searchController.SearchResultsUpdater = new SearchControllerDelegate(OnSearchTextUpdated);
            _searchController.DimsBackgroundDuringPresentation = false;
            _searchController.SearchBar.Placeholder = AppResources.SearchRestaurants;
            _searchController.SearchBar.BarTintColor = UIColor.White;
            _searchController.SearchBar.BackgroundImage = new UIImage();
            _searchController.SearchBar.TintColor = UIColor.FromRGB(231, 76, 60);
        }

        private void CustomizeTableView()
        {
            TableView.BackgroundView = EmptyListImageView;
            if (TableView.BackgroundView != null)
            {
                TableView.BackgroundView.Hidden = true;
            }
        }

        private void CreateQuickActions()
        {
            if (TraitCollection.ForceTouchCapability == UIForceTouchCapability.Available)
            {
                var bundleIdentifier = NSBundle.MainBundle.BundleIdentifier;
                if (bundleIdentifier != null)
                {
                    var shortcutItem1 = new UIApplicationShortcutItem("(com.companyname.FoodPinn).OpenFavorites", "Show Favorites", null, UIApplicationShortcutIcon.FromTemplateImageName("favorite"), null);
                    var shortcutItem2 = new UIApplicationShortcutItem("(com.companyname.FoodPinn).OpenDiscover", "Discover Restaurants", null, UIApplicationShortcutIcon.FromTemplateImageName("discover"), null);
                    var shortcutItem3 = new UIApplicationShortcutItem("(com.companyname.FoodPinn).NewRestaurant", "New Restaurant", null, UIApplicationShortcutIcon.FromType(UIApplicationShortcutIconType.Add), null);
                    UIApplication.SharedApplication.ShortcutItems = new[] { shortcutItem1, shortcutItem2, shortcutItem3 };
                }
            }
        }

        private void EnablePeekAndPop()
        {
            if (TraitCollection.ForceTouchCapability == UIForceTouchCapability.Available)
            {
                RegisterForPreviewingWithDelegate(new ViewControllerPreviewDelegate(this, _restaurantsMO), View);
            }
        }

        private void PrepareNotification()
        {
            if (_restaurantsMO.Count <= 0)
            {
                return;
            }

            var randomNum = new Random().Next(0, _restaurantsMO.Count);
            var suggestedRestaurant = _restaurantsMO[randomNum];
            var content = new UNMutableNotificationContent();

            CreateNotificationContent(content, suggestedRestaurant);
            AddingPhotoToNotification(suggestedRestaurant, content);
            CreateNotificationCustomActions(content);
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(10, false);
            var request = UNNotificationRequest.FromIdentifier("foodpin.restaurantsuggestion", content, trigger);
            UNUserNotificationCenter.Current.AddNotificationRequest(request, null);
        }

        private void CreateNotificationContent(UNMutableNotificationContent content, RestaurantMO restaurantMO)
        {
            content.Title = "Restaurant Recommendation";
            content.Subtitle = "Try new food today";
            content.Body = "I recommend you to check out " + restaurantMO.Name + ".The restaurant is one of your favorites. It is located at " + restaurantMO.Location + " .Would you like to give it a try?";
            content.Sound = UNNotificationSound.Default;
            NSDictionary userInfo = new NSDictionary("phone", restaurantMO.Phone);
            content.UserInfo = userInfo;
        }

        private void AddingPhotoToNotification(RestaurantMO restaurantMO, UNMutableNotificationContent content)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var suggestedRestaurantImage = Path.Combine(documents, "suggested-restaurant.jpg");

            var image = restaurantMO.GetImage();
            if (image != null)
            {
                try
                {
                    NSError err = null;
                    image.AsJPEG(1)?.Save(suggestedRestaurantImage, false, out err);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    NSError error;
                    UNNotificationAttachmentOptions attachmentOptions = new UNNotificationAttachmentOptions();
                    var finalImage = "file:///" + suggestedRestaurantImage;
                    var restaurantImage = UNNotificationAttachment.FromIdentifier("restaurantImage", new NSUrl(finalImage), attachmentOptions, out error);
                    if (restaurantImage != null)
                    {
                        content.Attachments = new[] { restaurantImage };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void CreateNotificationCustomActions(UNMutableNotificationContent content)
        {
            var categoryIdentifier = "foodpin.restaurantaction";
            var makeReservationAction = UNNotificationAction.FromIdentifier("foodpin.makeReservation", "Reserve a table", UNNotificationActionOptions.Foreground);
            var cancelAction = UNNotificationAction.FromIdentifier("foodpin.cancel", "Later", UNNotificationActionOptions.None);
            UNNotificationAction[] actions = new UNNotificationAction[] { makeReservationAction, cancelAction };
            string[] intentIdentifiers = new string[] { };
            UNNotificationCategoryOptions notificationCategoryOptions = new UNNotificationCategoryOptions();
            var category = UNNotificationCategory.FromIdentifier(categoryIdentifier, actions, intentIdentifiers, notificationCategoryOptions);
            UNUserNotificationCenter.Current.SetNotificationCategories(new NSSet<UNNotificationCategory>(category));
            content.CategoryIdentifier = categoryIdentifier;
        }
        #endregion
    }
}
