using System;
using FoodPin.Extensions;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewCell : UITableViewCell
    {
        private Restaurant _restaurant;

        public RestaurantTableViewCell(IntPtr handle) : base(handle)
        {
        }

        public void SetWithRestaurant(Restaurant restaurant)
        {
            _restaurant = restaurant;
            ThumbnailImageView.Image = _restaurant.GetImage();
            NameLabel.Text = _restaurant.Name;
            NameLabel.Lines = 0;
            TypeLabel.Text = _restaurant.Type;
            TypeLabel.Lines = 0;
            LocationLabel.Text = _restaurant.Location;
            LocationLabel.Lines = 0;
        }

        public void OnCheckInClicked()
        {
            var isVisited = !_restaurant.IsVisited;
            _restaurant.IsVisited = isVisited;
        }
    }
} 
