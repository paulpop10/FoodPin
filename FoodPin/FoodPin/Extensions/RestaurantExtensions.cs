using System;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin.Extensions
{
    public static class RestaurantExtensions
    {
        public static UIImage GetImage(this RestaurantMO restaurantMO)
        {
            NSData data = NSData.FromArray(restaurantMO.Image);
            return UIImage.LoadFromData(data);
        }

        public static NSObject[] GetActivityItems(this RestaurantMO restaurantMO)
        {
            var defaultText = "Just checking in at " + restaurantMO.Name;
            var activityItems = new NSObject[] { };
            var imageToLoad = restaurantMO.GetImage();
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

        public static UIImage GetCheckInImage(this RestaurantMO restaurantMO)
        {
            UIImage image = restaurantMO.IsVisited ? UIImage.FromBundle("undo") : UIImage.FromBundle("tick");
            return image;
        }

        public static UIImage GetCheckmarkImage(this RestaurantMO restaurantMO)
        {
            UIImage image = restaurantMO.IsVisited ? UIImage.FromBundle("CheckmarkImageView") : null;
            return image;
        }
    }
}
