using System;
using Foundation;
using UIKit;

namespace FoodPin.View
{
    public class ImagePickerControllerDelegate : UIImagePickerControllerDelegate
    {
        private UIImageView _image;
        private Action<bool> _onImageSetDelegate;

        public ImagePickerControllerDelegate(UIImageView image, Action<bool> onImageSetDelegate)
        {
            _image = image;
            _onImageSetDelegate = onImageSetDelegate;
        }

        public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            var selectedImage = info[UIImagePickerController.OriginalImage] as UIImage;
            if (selectedImage != null)
            {
                _image.Image = selectedImage;
                _image.ContentMode = UIViewContentMode.ScaleAspectFill;
                _image.ClipsToBounds = true;

                var leadingConstraint = NSLayoutConstraint.Create(_image, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, _image.Superview, NSLayoutAttribute.Leading, 1, 0);
                leadingConstraint.Active = true;

                var trailingConstraint = NSLayoutConstraint.Create(_image, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, _image.Superview, NSLayoutAttribute.Trailing, 1, 0);
                trailingConstraint.Active = true;

                var topConstraint = NSLayoutConstraint.Create(_image, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _image.Superview, NSLayoutAttribute.Top, 1, 0);
                topConstraint.Active = true;

                var bottomConstraint = NSLayoutConstraint.Create(_image, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, _image.Superview, NSLayoutAttribute.Bottom, 1, 0);
                bottomConstraint.Active = true;
                _onImageSetDelegate(true);
            }
            else 
            {
                _onImageSetDelegate(false);
            }

            picker.DismissViewController(true, null);
        }
    }
}
