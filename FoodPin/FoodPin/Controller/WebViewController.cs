using System;
using Foundation;
using UIKit;
using WebKit;

namespace FoodPin
{
    public partial class WebViewController : UIViewController
    {
        public WebViewController(IntPtr handle) : base(handle)
        {
        }

        public string TargetURL { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            var url = new NSUrl(TargetURL);
            if (url != null)
            {
                var request = new NSUrlRequest(url);
                WebView.LoadRequest(request);
            }
        }
    }
}