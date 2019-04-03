using System;
using FoodPin.View;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class AddRestaurantTableViewController : UITableViewController
    {
        public AddRestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }

        public Action AddRestaurantCloseDelegate { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CustomizeView();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            AddButtonsClickListeners();
        }

        public override void ViewWillDisappear(bool animated)
        {
            RemoveButtonsClickListeners();
            base.ViewWillDisappear(animated);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
           if (indexPath.Row == 0)
            {
                var photoSourceRequestController = UIAlertController.Create(string.Empty, "Choose your photo source", UIAlertControllerStyle.ActionSheet);
                var cameraAction = UIAlertAction.Create("Camera", UIAlertActionStyle.Default, CameraHandler);
                var photoLibraryAction = UIAlertAction.Create("Photo Library", UIAlertActionStyle.Default, PhotoLibraryHandler);
                var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
                photoSourceRequestController.AddAction(cameraAction);
                photoSourceRequestController.AddAction(photoLibraryAction);
                photoSourceRequestController.AddAction(cancelAction);
                SetUpPopover(photoSourceRequestController, tableView.CellAt(indexPath));
                PresentViewController(photoSourceRequestController, true, null);
            }
        }

        private void CustomizeView()
        {
            DismissKeyboard();
            SetFieldTags();
            RoundedTextView();
            SetTextFieldsDelegate();
            CustomizeNavigationBar();
            SetDinamicLabelsSize();
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        }

        private void PhotoLibraryHandler(UIAlertAction obj)
        {
            if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {
                var imagePicker = new UIImagePickerController();
                imagePicker.Delegate = new ImagePickerControllerDelegate(PhotoImageView);
                imagePicker.AllowsEditing = false;
                imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
                PresentViewController(imagePicker, true, null);
            }
        }

        private void CameraHandler(UIAlertAction obj)
        {
            if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                var imagePicker = new UIImagePickerController();
                imagePicker.Delegate = new ImagePickerControllerDelegate(PhotoImageView);
                imagePicker.AllowsEditing = false;
                imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
                PresentViewController(imagePicker, true, null);
            }
        }

        private void SetFieldTags()
        {
            RestaurantNameTextField.Tag = 1;
            RestaurantNameTextField.BecomeFirstResponder();
            RestaurantTypeTextField.Tag = 2;
            RestaurantAddressTextField.Tag = 3;
            RestaurantPhoneTextField.Tag = 4;
            RestaurantDescriptionTextView.Tag = 5;    
        }

        private void RoundedTextView()
        {
            RestaurantDescriptionTextView.Layer.CornerRadius = 5;
            RestaurantDescriptionTextView.Layer.MasksToBounds = true;
        }

        private void SetTextFieldsDelegate()
        {
            RestaurantNameTextField.Delegate = new TextFieldDelegate(View);
            RestaurantTypeTextField.Delegate = new TextFieldDelegate(View);
            RestaurantPhoneTextField.Delegate = new TextFieldDelegate(View);
            RestaurantAddressTextField.Delegate = new TextFieldDelegate(View);         
        }

        private void CustomizeNavigationBar()
        {
            if (NavigationController != null)
            {
                NavigationController.NavigationBar.TintColor = UIColor.White;
                NavigationController.NavigationBar.ShadowImage = new UIImage();
                var customFont = UIFont.FromName("Arial", 35);
                if (customFont != null)
                {
                    if (NavigationController != null)
                    {
                        NavigationController.NavigationBar.LargeTitleTextAttributes = new UIStringAttributes
                        {
                            ForegroundColor = UIColor.FromRGB(231, 76, 60),
                            Font = customFont
                        };
                    }
                }
            }
        }

        private void SetUpPopover(UIAlertController uiAlertController, UITableViewCell cell)
        {
            var popoverController = uiAlertController.PopoverPresentationController;
            if (popoverController != null)
            {
                if (cell != null)
                {
                    popoverController.SourceView = cell;
                    popoverController.SourceRect = cell.Bounds;
                }
            }
        }

        private void AddSaveButtonClickListener()
        {
            NewRestaurantSaveButton.Clicked += OnSaveButtonClicked;
        }

        private void AddCloseButtonClickListener()
        {
            NewRestaurantCloseButton.Clicked += OnCloseButtonClicked;
        }

       private void RemoveCloseButtonClickListener()
        {
            NewRestaurantCloseButton.Clicked -= OnCloseButtonClicked;
        }

        private void RemoveSaveButtonClickListener()
        {
            NewRestaurantSaveButton.Clicked -= OnSaveButtonClicked;
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            AddRestaurantCloseDelegate();
        }

        private void OnSaveButtonClicked(object sender, object e)
        {
            if (CheckFilledTextFields() && OnlyNumbersPhoneTextField(RestaurantPhoneTextField.Text))
            {
                SetNewRestaurantData();
                AddRestaurantCloseDelegate();
            }
            else if (CheckFilledTextFields() && !OnlyNumbersPhoneTextField(RestaurantPhoneTextField.Text))
            {
                CreateOnlyNumbersAlertController();
            }
            else if (!CheckFilledTextFields() && OnlyNumbersPhoneTextField(RestaurantPhoneTextField.Text))
            {
                CreateFieldsNotFilledAlertController();
            }
            else if (!CheckFilledTextFields() && !OnlyNumbersPhoneTextField(RestaurantPhoneTextField.Text))
            {
                CreateFieldsNotFilledAlertController();
            }
        }

        private void AddButtonsClickListeners()
        {
            AddSaveButtonClickListener();
            AddCloseButtonClickListener();
        }

        private void RemoveButtonsClickListeners()
        {
            RemoveSaveButtonClickListener();
            RemoveCloseButtonClickListener();
        }

        private bool CheckFilledTextFields()
        {
            if (RestaurantNameTextField.Text == string.Empty ||
               RestaurantTypeTextField.Text == string.Empty ||
               RestaurantPhoneTextField.Text == string.Empty ||
               RestaurantAddressTextField.Text == string.Empty ||
               RestaurantDescriptionTextView.Text == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetNewRestaurantData()
        {
            var restaurantName = RestaurantNameTextField.Text;
            var restaurantType = RestaurantTypeTextField.Text;
            var restaurantPhone = RestaurantPhoneTextField.Text;
            var restaurantAddress = RestaurantAddressTextField.Text;
            var restaurantDescription = RestaurantDescriptionTextView.Text;
            Console.WriteLine(
                "Name: " + restaurantName + "\n" +
                "Type: " + restaurantType + "\n" +
                "Location: " + restaurantAddress + "\n" +
                "Phone: " + restaurantPhone + "\n" +
                "Description: " + restaurantDescription + "\n");
        }

        private void CreateFieldsNotFilledAlertController()
        {
            var emptyFieldsAlert = UIAlertController.Create("Oops", "We can't proceed because at least one of the fields is blank.Please note that all fields are required.", UIAlertControllerStyle.ActionSheet);
            var cancelAction = UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null);
            emptyFieldsAlert.AddAction(cancelAction);
            SetUpPopover(emptyFieldsAlert, TableView.CellAt(NSIndexPath.FromRowSection(0, 0)));
            PresentViewController(emptyFieldsAlert, true, null);
        }

        private void SetDinamicLabelsSize()
        {
            RestaurantNameLabel.Lines = 0;
            RestaurantTypeLabel.Lines = 0;
            RestaurantAddressLabel.Lines = 0;
            RestaurantPhoneLabel.Lines = 0;
            RestaurantDescriptionLabel.Lines = 0;
        }

        private bool OnlyNumbersPhoneTextField(string phoneNumber)
        {
           for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (phoneNumber[i] < '0' || phoneNumber[i] > '9')
                {
                    return false;
                }       
            }

            return true;
        }

        private void CreateOnlyNumbersAlertController()
        {
            var onlyNumbersAlert = UIAlertController.Create("Oops", "The phone number should contain numbers only.", UIAlertControllerStyle.ActionSheet);
            var cancelAction = UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null);
            onlyNumbersAlert.AddAction(cancelAction);
            SetUpPopover(onlyNumbersAlert, TableView.CellAt(NSIndexPath.FromRowSection(0, 0)));
            PresentViewController(onlyNumbersAlert, true, null);
        }

        private void DismissKeyboard()
        {
            var hideKeyboard = new UITapGestureRecognizer((obj) => View.EndEditing(true));
            hideKeyboard.CancelsTouchesInView = false;
            View.AddGestureRecognizer(hideKeyboard);
        }
    }
}