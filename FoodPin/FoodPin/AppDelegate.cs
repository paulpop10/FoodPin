using System;
using System.Linq;
using FoodPin.Controller;
using FoodPin.Model;
using FoodPin.View;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace FoodPin
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        //// class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var dataBaseConnection = DataBaseConnection.Instance;
            dataBaseConnection.Conn.CreateTable<RestaurantMO>();
            SetBackButtonOnNavigationBar();
            TabBarCustomization();
            Localization();
            AskingForUserPermissions();
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            return true;
        }

        public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            completionHandler(HandleQuickAction(shortcutItem));
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        private void SetBackButtonOnNavigationBar()
        {
            var backButtonImage = UIImage.FromBundle("back");
            UINavigationBar.Appearance.BackIndicatorImage = backButtonImage;
            UINavigationBar.Appearance.BackIndicatorTransitionMaskImage = backButtonImage;
        }

        private void TabBarCustomization()
        {
            UITabBar.Appearance.TintColor = UIColor.FromRGB(231, 76, 60);
            UITabBar.Appearance.BarTintColor = UIColor.Black;
        }

        private void Localization()
        {
            var localize = new Localize();
            AppResources.Culture = localize.GetCurrentCultureInfo();
        }

        private string RetrieveQuickActionsShortNames(string fullIdentifier)
        {
            var shortcutIdentifier = fullIdentifier.Split('.').Last();
            if (shortcutIdentifier != null)
            {
                return shortcutIdentifier;
            }

            return null;
        }

        private bool HandleQuickAction(UIApplicationShortcutItem shortcutItem)
        {
            var shortcutType = shortcutItem.Type;
            var shortcutIdentifier = RetrieveQuickActionsShortNames(shortcutType);
            if (shortcutIdentifier == null)
            {
                return false;
            }

            var tabBarController = Window?.RootViewController as UITabBarController;
            if (tabBarController == null)
            {
                return false;
            }

            switch (shortcutIdentifier)
            {
                case "OpenFavorites":
                    tabBarController.SelectedIndex = 0;
                    DismissAddRestaurantViewController();
                    break;
                case "OpenDiscover":
                    tabBarController.SelectedIndex = 1;
                    DismissAddRestaurantViewController();   
                    break;
                case "NewRestaurant":
                    var navController = tabBarController.ViewControllers?[0];
                    if (navController != null)
                    {
                        var restaurantTableViewController = navController.ChildViewControllers[0];
                        restaurantTableViewController.PerformSegue("addRestaurant", restaurantTableViewController);
                    }
                    else
                    {
                        return false;
                    }

                    break;
                default:
                    break;
            }

            return true;
        }

        private UIViewController RetrieveCurrentShownViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            while (viewController.PresentedViewController != null)
            {
                viewController = viewController.PresentedViewController;
            }

            if (viewController is UINavigationController navigationController)
            {
                viewController = navigationController.ViewControllers.Last();
            }

            return viewController;
        }

        private void DismissAddRestaurantViewController()
        {
            var currentShownViewController = RetrieveCurrentShownViewController();
            if (currentShownViewController is AddRestaurantTableViewController)
            {
                currentShownViewController.DismissViewController(true, null);
            }
        }

        private void AskingForUserPermissions()
        {       
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound | UNAuthorizationOptions.Badge, CompletionHandler);
        }

        private void CompletionHandler(bool granted, NSError error)
        {
            if (granted)
            {
                Console.WriteLine("User notifications are allowed");
            }
            else
            {
                Console.WriteLine("User notifications are not allowed");
            }
        }
    }
}
