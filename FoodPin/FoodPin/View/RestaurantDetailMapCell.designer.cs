// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FoodPin
{
    [Register ("RestaurantDetailMapCell")]
    partial class RestaurantDetailMapCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MapView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MapView != null) {
                MapView.Dispose ();
                MapView = null;
            }
        }
    }
}