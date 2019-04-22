using System;
using System.Collections.Generic;
using System.Security.Policy;
using CoreSpotlight;
using Foundation;
using SafariServices;
using UIKit;

namespace FoodPin
{
    public partial class AboutTableViewController : UITableViewController
    {
        private readonly List<string> _sectionTitles = new List<string> { AppResources.Feedback, AppResources.FollowUs };
        private readonly List<List<(string image, string text, string link)>> _sectionContent = new List<List<(string image, string text, string link)>>
        {
            new List<(string image, string text, string link)> { (image: "store", text: AppResources.RateUs, link: "https://www.apple.com/itunes/charts/paid-apps/"), (image: "chat", text: AppResources.TellTheFeedback, link: "http://www.appcoda.com/contact") },
            new List<(string image, string text, string link)> { (image: "twitter", text: "Twitter", link: "https://twitter.com/appcodamobile"), (image: "facebook", text: "Facebook", link: "https://facebook.com/appcodamobile"), (image: "instagram", text: "Instagram", link: "https://www.instagram.com/appcodadotcom") }
        };

        public AboutTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CustomizeTableView();
            CustomizeNavigationBar();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var link = _sectionContent[indexPath.Section][indexPath.Row].link;
            switch (indexPath.Section)
            {
                case 0:
                    if (indexPath.Row == 0)
                    {
                        var firstSectionURL = new NSUrl(link);
                        if (firstSectionURL != null)
                        {
                            UIApplication.SharedApplication.OpenUrl(firstSectionURL);
                        }
                    }
                    else if (indexPath.Row == 1)
                    {
                        PerformSegue("showWebView", this);
                    }

                    break;
                case 1:
                    var secondSectionURL = new NSUrl(link);
                    if (secondSectionURL != null)
                    {
                        var safariController = new SFSafariViewController(secondSectionURL);
                        PresentViewController(safariController, true, null);
                    }

                    break;
                default:
                    break;
            }

            tableView.DeselectRow(indexPath, false);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showWebView")
            {
                var destinationController = segue.DestinationViewController as WebViewController;
                if (destinationController != null)
                {
                    var indexPath = TableView.IndexPathForSelectedRow;
                    destinationController.TargetURL = _sectionContent[indexPath.Section][indexPath.Row].link;
                }
            }
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return _sectionTitles.Count;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _sectionContent[(int)section].Count;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return _sectionTitles[(int)section];
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("AboutCellIdentifier");
            var cellData = _sectionContent[indexPath.Section][indexPath.Row];
            if (cell.TextLabel != null)
            {
                cell.TextLabel.Text = cellData.text;
            }

            if (cell.ImageView != null)
            {
                cell.ImageView.Image = UIImage.FromBundle(cellData.image);
            }

            return cell;
        }

        private void CustomizeNavigationBar()
        {
            if (NavigationController != null)
            {
                NavigationController.NavigationBar.PrefersLargeTitles = true;
                NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                NavigationController.NavigationBar.ShadowImage = new UIImage();
            }

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

        private void CustomizeTableView()
        {
            TableView.CellLayoutMarginsFollowReadableWidth = true;
            TableView.TableFooterView = new UIView();
        }
    }
}