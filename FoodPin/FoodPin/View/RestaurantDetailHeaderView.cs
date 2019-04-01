using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RestaurantDetailHeaderView : UIView
    {
        public RestaurantDetailHeaderView(IntPtr handle) : base(handle)
        {
        }

        public RestaurantDetailHeaderView()
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            NSBundle.MainBundle.LoadNib(nameof(RestaurantDetailHeaderView), this, null);
            ContainerView.Frame = this.Bounds;
            AddSubview(ContainerView);
        }

        public void SetHeaderImageView(UIImage image)
        {
            HeaderImageView.Image = image;
        }

        public void SetHeartImageView(UIImage image)
        {
            HeartImageView.Image = image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            HeartImageView.TintColor = UIColor.White;
        }

        public void SetNameLabel(string name)
        {
            NameLabel.Text = name;
            NameLabel.Lines = 0;
        }

        public void SetTypeLabel(string type)
        {
            TypeLabel.Text = type;
            TypeLabel.Lines = 0;
            TypeLabel.Layer.CornerRadius = 5;
            TypeLabel.Layer.MasksToBounds = true;
        }

        public void SetRatingImageView(UIImage image)
        {
            RatingImageView.Image = image;
            var scaleTransform = CGAffineTransform.MakeScale((nfloat)0.1, (nfloat)0.1);
            RatingImageView.Transform = scaleTransform;
            RatingImageView.Alpha = 0;
            UIView.AnimateNotify(
            0.4, 
            0, 
            (nfloat)0.3, 
            (System.nfloat)0.7, 
            UIViewAnimationOptions.BeginFromCurrentState, 
            () => 
            {
                RatingImageView.Transform = CGAffineTransform.MakeIdentity();
                RatingImageView.Alpha = 1;
             }, 
             null);
        }
    }
}