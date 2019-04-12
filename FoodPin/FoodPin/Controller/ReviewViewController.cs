using System;
using System.Drawing;
using CoreGraphics;
using FoodPin.Extensions;
using FoodPin.Model;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class ReviewViewController : UIViewController
    {
        private const string LoveReaction = "love";
        private const string CoolReaction = "cool";
        private const string HappyReaction = "happy";
        private const string SadReaction = "sad";
        private const string AngryReaction = "angry";

        public ReviewViewController(IntPtr handle) : base(handle)
        {
        }

        public RestaurantMO RestaurantMO { get; set; }

        public Action CloseDelegate { get; set; }

        public Action<string> RateDelegate { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CustomizeBackgroundImage();
            ExitButton.TouchUpInside += (sender, e) => CloseDelegate();
            AddRateButtonsAction();
            SetAnimationStartState();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SetAnimationEndState();
        }

        private void CustomizeBackgroundImage()
        {
            BackgroundImageView.Image = RestaurantMO.GetImage();
            var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Light);
            var blurEffectView = new UIVisualEffectView(blurEffect);
            blurEffectView.Frame = View.Bounds;
            BackgroundImageView.AddSubview(blurEffectView);

            blurEffectView.TranslatesAutoresizingMaskIntoConstraints = false;
            blurEffectView.TopAnchor.ConstraintEqualTo(BackgroundImageView.TopAnchor).Active = true;
            blurEffectView.BottomAnchor.ConstraintEqualTo(BackgroundImageView.BottomAnchor).Active = true;
            blurEffectView.LeftAnchor.ConstraintEqualTo(BackgroundImageView.LeftAnchor).Active = true;
            blurEffectView.RightAnchor.ConstraintEqualTo(BackgroundImageView.RightAnchor).Active = true;
        }

        private void SetAnimationStartState()
        {
            UIButton[] rateButtons = new UIButton[] { LoveButton, CoolButton, HappyButton, SadButton, AngryButton };
            var moveRightTransform = CGAffineTransform.MakeTranslation(600, 0);
            var moveTopTransform = CGAffineTransform.MakeTranslation(0, 6000);
            var scaleUpTransform = CGAffineTransform.MakeScale(10, 10);
            var moveRightScaleTransform = scaleUpTransform * moveRightTransform;
            var moveTopScaleTransform = scaleUpTransform * moveTopTransform;
            foreach (var rateButton in rateButtons)
            {
                rateButton.Transform = moveRightScaleTransform;
                rateButton.Alpha = 0;
            }

            ExitButton.Transform = moveTopScaleTransform;
            ExitButton.Alpha = 0;
        }

        private void SetAnimationEndState()
        {
            UIButton[] rateButtons = new UIButton[] { LoveButton, CoolButton, HappyButton, SadButton, AngryButton };
            var animationDuration = 0.4;
            var animationDelay = 0.05;
            var animationDamping = 0.1;
            var animationSpringVelocity = 0.3;
            foreach (var rateButton in rateButtons)
            {
                UIView.AnimateNotify(animationDuration, animationDelay + 0.05, (nfloat)animationDamping, (nfloat)animationSpringVelocity, UIViewAnimationOptions.BeginFromCurrentState, () => { rateButton.Alpha = 1; rateButton.Transform = CGAffineTransform.MakeIdentity(); }, null);
            }

            UIView.AnimateNotify(0.4, 0.1, (nfloat)0.1, (System.nfloat)0.3, UIViewAnimationOptions.BeginFromCurrentState, () => { ExitButton.Alpha = 1; ExitButton.Transform = CGAffineTransform.MakeIdentity(); }, null);
        }

        private void AddRateButtonsAction()
        {
            LoveButton.TouchUpInside += (sender, e) => RateDelegate(LoveReaction);
            CoolButton.TouchUpInside += (sender, e) => RateDelegate(CoolReaction);
            HappyButton.TouchUpInside += (sender, e) => RateDelegate(HappyReaction);
            SadButton.TouchUpInside += (sender, e) => RateDelegate(SadReaction);
            AngryButton.TouchUpInside += (sender, e) => RateDelegate(AngryReaction);
        }
    }
}