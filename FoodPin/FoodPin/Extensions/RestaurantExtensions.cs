using System;
using Foundation;
using UIKit;

namespace FoodPin.Extensions
{
    public static class RestaurantExtensions
    {
        public static UIImage GetImage(this Restaurant restaurant)
        {
            return UIImage.FromBundle(restaurant.Image);
        }

        public static NSObject[] GetActivityItems(this Restaurant restaurant)
        {
            var defaultText = "Just checking in at " + restaurant.Name;
            var activityItems = new NSObject[] { };
            var imageToLoad = restaurant.GetImage();
            if (imageToLoad != null)
            {
                activityItems = new NSObject[] { (new NSString(defaultText)), imageToLoad };
            }
            else
            {
                activityItems = new NSObject[] { (new NSString(defaultText)) };
            }

            return activityItems;
        }

        public static UIImage GetCheckInImage(this Restaurant restaurant)
        {
            if (restaurant.IsVisited)
            {
                return UIImage.FromBundle("undo");
            }
            else
            {
                return UIImage.FromBundle("tick");
            }
        }
    }
}
