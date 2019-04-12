using System;
using FoodPin.Controller;
using FoodPin.Extensions;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantTableViewCell : UITableViewCell
    {
        private RestaurantMO _restaurantMO;

        public RestaurantTableViewCell(IntPtr handle) : base(handle)
        {
        }

        public void SetWithRestaurant(RestaurantMO restaurantMO)
        {
            _restaurantMO = restaurantMO;
            ThumbnailImageView.Image = _restaurantMO.GetImage();
            NameLabel.Text = _restaurantMO.Name;
            NameLabel.Lines = 0;
            TypeLabel.Text = _restaurantMO.Type;
            TypeLabel.Lines = 0;
            LocationLabel.Text = _restaurantMO.Location;
            LocationLabel.Lines = 0;
        }

        public void OnCheckInClicked()
        {
            var dataBaseConnection = DataBaseConnection.Instance;
            var isVisited = !_restaurantMO.IsVisited;
            _restaurantMO.IsVisited = isVisited;
            dataBaseConnection.Conn.Update(_restaurantMO);
        }     
    }
} 
