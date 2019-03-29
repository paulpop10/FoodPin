using System;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class NewRestaurantDetailTextCell : UITableViewCell
    {
        public NewRestaurantDetailTextCell(IntPtr handle) : base(handle)         {             SelectionStyle = UITableViewCellSelectionStyle.None;         }          public void SetWithRestaurant(RestaurantInfo restaurantInfo)         {             DescriptionLabel.Lines = 0;             DescriptionLabel.Text = restaurantInfo.Text;         }
    }
}