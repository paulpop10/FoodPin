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
            TypeLabel.Text = _restaurant.Type;
            LocationLabel.Text = _restaurant.Location;
            this.CheckIn(_restaurant.IsVisited);
        }

        public void OnCheckInClicked()
        {
            var isVisited = !_restaurant.IsVisited;
            _restaurant.IsVisited = isVisited;
            this.CheckIn(isVisited);
        }

        private void CheckIn(bool check)
        {
            if (check)
            {
                var checkMarkImage = UIImage.FromBundle("CheckmarkImageView");
                this.AccessoryView = new UIImageView(checkMarkImage);
            }
            else
            {
                this.AccessoryView = null;
            }
        }
    }
    }
