using System;
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
            TypeLabel.Layer.CornerRadius = 5;
            TypeLabel.Layer.MasksToBounds = true;
        }
    }
}