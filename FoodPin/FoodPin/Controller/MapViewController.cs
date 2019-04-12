using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using FoodPin.Model;
using FoodPin.View;
using Foundation;
using MapKit;
using UIKit;

namespace FoodPin
{
    public partial class MapViewController : UIViewController
    {
        public MapViewController(IntPtr handle) : base(handle)
        {
        }

        public RestaurantMO RestaurantMO { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetAnnotation();
            CustomizeMapView();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.TintColor = UIColor.Black;
        }

        public override void ViewWillDisappear(bool animated)
        {
            NavigationController.NavigationBar.TintColor = UIColor.White;
            base.ViewWillDisappear(animated);
        }

        public void SetAnnotation()
        {
            var geoCoder = new CLGeocoder();
            geoCoder.GeocodeAddress(RestaurantMO.Location, AnnotationHandler);
        }

        private void AnnotationHandler(CLPlacemark[] placemarks, NSError error)
        {
            if (error != null)
            {
                Console.WriteLine(error);
            }

            if (placemarks != null)
            {
                var placemark = placemarks[0];
                var annotation = new MKPointAnnotation(); 
                annotation.Title = RestaurantMO.Name;
                annotation.Subtitle = RestaurantMO.Type;
                var location = placemark.Location;
                if (location != null)
                {
                    annotation.Coordinate = location.Coordinate;
                    MapView.ShowAnnotations(new[] { annotation }, true);
                    MapView.SelectAnnotation(annotation, true);
                }             
            }
        }

        private void CustomizeMapView()
        {
            MapView.Delegate = new MapViewDelegate();
            MapView.ShowsCompass = true;
            MapView.ShowsScale = true;
            MapView.ShowsTraffic = true;
        }
    }
}