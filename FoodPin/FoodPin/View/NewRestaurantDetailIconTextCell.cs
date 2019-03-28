using System;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class NewRestaurantDetailIconTextCell : UITableViewCell
    {
        public NewRestaurantDetailIconTextCell(IntPtr handle) : base(handle)         {             SelectionStyle = UITableViewCellSelectionStyle.None;         }          public void SetWithRestaurant(RestaurantInfoWithIcon restaurantInfoWithIcon)         {             IconImageView.Image = UIImage.FromBundle(restaurantInfoWithIcon.ImageName);             ShortTextLabel.Lines = 0;             ShortTextLabel.Text = restaurantInfoWithIcon.Text;         }
    }
}