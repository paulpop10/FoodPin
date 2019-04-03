using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class RoundedTextField : UITextField
    {
        private static readonly UIEdgeInsets Padding = new UIEdgeInsets(0, 10, 0, 10);

        public RoundedTextField(IntPtr handle) : base(handle)
        {
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            return Padding.InsetRect(forBounds);
        }

        public override CGRect PlaceholderRect(CGRect forBounds)
        {
            return Padding.InsetRect(forBounds);
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            return Padding.InsetRect(forBounds);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            Layer.CornerRadius = 5;
            Layer.MasksToBounds = true;
        }
    }
}
