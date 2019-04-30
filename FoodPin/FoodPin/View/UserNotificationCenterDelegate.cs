using System;
using Foundation;
using UIKit;
using UserNotifications;

namespace FoodPin.View
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public UserNotificationCenterDelegate()
        {
        }
               
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            if (response.ActionIdentifier == "foodpin.makeReservation")
            {
                Console.WriteLine("Make reservation...");
                var phone = response.Notification.Request.Content.UserInfo["phone"];
                if (phone != null)
                {
                    var telURL = "tel://" + phone;
                    var url = new NSUrl(telURL);
                    if (url != null)
                    {
                        if (UIApplication.SharedApplication.CanOpenUrl(url))
                        {
                            Console.WriteLine("calling " + telURL);
                            UIApplication.SharedApplication.OpenUrl(url);
                        }
                    }
                }
            }

            completionHandler();
        }
    }
}
