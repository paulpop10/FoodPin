using System;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class WalkthroughContentViewController : UIViewController
    {
        public WalkthroughContentViewController(IntPtr handle) : base(handle)
        {
        }

        public string Heading { get; set; }

        public string SubHeading { get; set; }

        public string ImageFile { get; set; }

        public int Index { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            HeadingLabel.Text = Heading;
            HeadingLabel.Lines = 0;
            SubHeadingLabel.Text = SubHeading;
            SubHeadingLabel.Lines = 0;
            ContentImageView.Image = UIImage.FromBundle(ImageFile);
        }
    }
}