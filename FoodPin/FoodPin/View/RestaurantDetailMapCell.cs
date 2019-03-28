using System;
using CoreLocation;
using Foundation;
using MapKit;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantDetailMapCell : UITableViewCell
    {
        public RestaurantDetailMapCell(IntPtr handle) : base(handle)
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        public override void SetSelected(bool selected, bool animated)
        {
            base.SetSelected(selected, animated);
        }

        public void Configure(string location)
        {
            var geoCoder = new CLGeocoder();
            Console.WriteLine(location);
            geoCoder.GeocodeAddress(location, AnnotationHandler);
        }

        private void AnnotationHandler(CLPlacemark[] placemarks, NSError error)
        {
            if (error != null)
            {
                Console.WriteLine(error.LocalizedDescription);
            }

            if (placemarks != null)
            {
                var placemark = placemarks[0];
                var annotation = new MKPointAnnotation();
                var location = placemark.Location;
                if (location != null)
                {
                    annotation.Coordinate = location.Coordinate;
                    MapView.AddAnnotation(annotation);
                    var region = MKCoordinateRegion.FromDistance(annotation.Coordinate, 250, 250);
                    MapView.SetRegion(region, false);
                }
            }
        }
    }
}