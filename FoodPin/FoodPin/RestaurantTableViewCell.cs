using System;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewCell : UITableViewCell
    {            
        public RestaurantTableViewCell(IntPtr handle) : base(handle)
        {
        }

        public void SetImage(UIImage thumbnailImageView)
        {
            ThumbnailImageView.Image = thumbnailImageView;       
        }

        public void SetName(string nameLabel)
        {
            NameLabel.Text = nameLabel;
        }

        public void SetLocation(string locationLabel)
        {
            LocationLabel.Text = locationLabel;
        }

        public void SetType(string typeLabel)
        {
            TypeLabel.Text = typeLabel;
        }

        public void SetCheckmarkImageView(UIImage checkmarkImage)
        {
            CheckmarkImageView.Image = checkmarkImage;
        }
    }
}