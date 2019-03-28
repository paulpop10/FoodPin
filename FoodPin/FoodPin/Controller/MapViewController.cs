using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
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

        public Restaurant Restaurant { get; set; }

        public override void ViewDidLoad()
        {
            SetAnnotation();
            CustomizeMapView();
        }

        public void SetAnnotation()
        {
            var geoCoder = new CLGeocoder();
            geoCoder.GeocodeAddress(Restaurant.Location, AnnotationHandler);
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
                annotation.Title = Restaurant.Name;
                annotation.Subtitle = Restaurant.Type;
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