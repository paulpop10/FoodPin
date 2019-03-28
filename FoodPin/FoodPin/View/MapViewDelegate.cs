using System;
using MapKit;
using UIKit;

namespace FoodPin.View
{
    public class MapViewDelegate : MKMapViewDelegate
    {
        private const string Identifier = "MyMarker";

        public MapViewDelegate()
        {
        }

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is MKUserLocation)
            {
                return null;
            }

            var annotationView = mapView.DequeueReusableAnnotation(Identifier) as MKMarkerAnnotationView;
            annotationView = annotationView ?? new MKMarkerAnnotationView(annotation, Identifier);
            annotationView.GlyphText = "\ud83d\ude38";
            annotationView.MarkerTintColor = UIColor.Red;

            return annotationView;
        }
    }
}
