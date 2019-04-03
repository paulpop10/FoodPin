using System;
using UIKit;

namespace FoodPin.View
{
    public class TextFieldDelegate : UITextFieldDelegate
    {
        private readonly UIView _view;

        public TextFieldDelegate(UIView view)
        {
            _view = view;
        }

        public override bool ShouldReturn(UITextField textField)
        {
            var nextTextField = _view.ViewWithTag(textField.Tag + 1);
            if (nextTextField != null)
            {
                textField.ResignFirstResponder();
                nextTextField.BecomeFirstResponder();
            }

            return true;
        }
    }
}
