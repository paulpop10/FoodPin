using System;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantDetailSeparatorCell : UITableViewCell
    {
        public RestaurantDetailSeparatorCell(IntPtr handle) : base(handle)
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        public void SetWithRestaurant(RestaurantMapText restaurantMapText)
        {
            SubsectionTitle.Text = restaurantMapText.Text;
            SubsectionTitle.Lines = 0;
        }
    }
}