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
    [Register ("AddRestaurantTableViewController")]
    partial class AddRestaurantTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem NewRestaurantCloseButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem NewRestaurantSaveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView PhotoImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantAddressLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FoodPin.RoundedTextField RestaurantAddressTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantDescriptionLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView RestaurantDescriptionTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantNameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FoodPin.RoundedTextField RestaurantNameTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantPhoneLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FoodPin.RoundedTextField RestaurantPhoneTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RestaurantTypeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FoodPin.RoundedTextField RestaurantTypeTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (NewRestaurantCloseButton != null) {
                NewRestaurantCloseButton.Dispose ();
                NewRestaurantCloseButton = null;
            }

            if (NewRestaurantSaveButton != null) {
                NewRestaurantSaveButton.Dispose ();
                NewRestaurantSaveButton = null;
            }

            if (PhotoImageView != null) {
                PhotoImageView.Dispose ();
                PhotoImageView = null;
            }

            if (RestaurantAddressLabel != null) {
                RestaurantAddressLabel.Dispose ();
                RestaurantAddressLabel = null;
            }

            if (RestaurantAddressTextField != null) {
                RestaurantAddressTextField.Dispose ();
                RestaurantAddressTextField = null;
            }

            if (RestaurantDescriptionLabel != null) {
                RestaurantDescriptionLabel.Dispose ();
                RestaurantDescriptionLabel = null;
            }

            if (RestaurantDescriptionTextView != null) {
                RestaurantDescriptionTextView.Dispose ();
                RestaurantDescriptionTextView = null;
            }

            if (RestaurantNameLabel != null) {
                RestaurantNameLabel.Dispose ();
                RestaurantNameLabel = null;
            }

            if (RestaurantNameTextField != null) {
                RestaurantNameTextField.Dispose ();
                RestaurantNameTextField = null;
            }

            if (RestaurantPhoneLabel != null) {
                RestaurantPhoneLabel.Dispose ();
                RestaurantPhoneLabel = null;
            }

            if (RestaurantPhoneTextField != null) {
                RestaurantPhoneTextField.Dispose ();
                RestaurantPhoneTextField = null;
            }

            if (RestaurantTypeLabel != null) {
                RestaurantTypeLabel.Dispose ();
                RestaurantTypeLabel = null;
            }

            if (RestaurantTypeTextField != null) {
                RestaurantTypeTextField.Dispose ();
                RestaurantTypeTextField = null;
            }
        }
    }
}