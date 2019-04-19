using System;
using System.Threading.Tasks;
using CoreFoundation;
using DeviceCheck;
using FoodPin.Controller;
using FoodPin.Model;
using FoodPin.View;
using Foundation;
using UIKit;

namespace FoodPin
{
    public partial class AddRestaurantTableViewController : UITableViewController
    {
        private bool _wasImageSet;

        public AddRestaurantTableViewController(IntPtr handle) : base(handle)
        {
        }

        public Action AddRestaurantCloseDelegate { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();         
            CustomizeView();
            SetLabelsTexts();
            SetTextFieldsPlaceholders();
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
                var photoSourceRequestController = UIAlertController.Create(string.Empty, AppResources.ChoosingPhotoSource, UIAlertControllerStyle.ActionSheet);
                var cameraAction = UIAlertAction.Create(AppResources.Camera, UIAlertActionStyle.Default, CameraHandler);
                var photoLibraryAction = UIAlertAction.Create(AppResources.PhotoLibrary, UIAlertActionStyle.Default, PhotoLibraryHandler);
                var cancelAction = UIAlertAction.Create(AppResources.Cancel, UIAlertActionStyle.Cancel, null);
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
                imagePicker.Delegate = new ImagePickerControllerDelegate(PhotoImageView, OnImageSet);
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
                imagePicker.Delegate = new ImagePickerControllerDelegate(PhotoImageView, OnImageSet);
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
            NavigationItem.Title = AppResources.NewRestaurantTitle;
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
               RestaurantDescriptionTextView.Text == string.Empty ||
               !_wasImageSet)
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
            RestaurantMO restaurantMO = new RestaurantMO()
            {
                Name = RestaurantNameTextField.Text,
                Type = RestaurantTypeTextField.Text,
                Location = RestaurantAddressTextField.Text,
                Phone = RestaurantPhoneTextField.Text,
                Summary = RestaurantDescriptionTextView.Text,
                IsVisited = false,
                Image = PhotoImageView.Image.AsPNG().ToArray(),
                Rating = string.Empty
            };
            InsertNewRestaurant(restaurantMO);
        }

        private async void InsertNewRestaurant(RestaurantMO restaurantMO)
        {
            var dataBaseConnection = DataBaseConnection.Instance;
            if (dataBaseConnection != null)
            {
                dataBaseConnection.Conn.Insert(restaurantMO);     
            }
            ////Required by the async method
            await Task.Delay(1);
            InvokeOnMainThread(() => { CreateAddedRestaurantAlertController(restaurantMO.Name); });
        }

        private void CreateFieldsNotFilledAlertController()
        {
            var emptyFieldsAlert = UIAlertController.Create("Oops", AppResources.EmptyFieldsWarning, UIAlertControllerStyle.ActionSheet);
            var cancelAction = UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null);
            emptyFieldsAlert.AddAction(cancelAction);
            SetUpPopover(emptyFieldsAlert, TableView.CellAt(NSIndexPath.FromRowSection(0, 0)));
            PresentViewController(emptyFieldsAlert, true, null);
        }

        private void CreateAddedRestaurantAlertController(string restaurantName)
        {
            var addedRestaurantAlert = UIAlertController.Create(AppResources.Success, restaurantName + AppResources.AddedRestaurantWarning, UIAlertControllerStyle.ActionSheet);
            var cancelAction = UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, (obj) => { AddRestaurantCloseDelegate(); });
            addedRestaurantAlert.AddAction(cancelAction);
            SetUpPopover(addedRestaurantAlert, TableView.CellAt(NSIndexPath.FromRowSection(0, 0)));
            PresentViewController(addedRestaurantAlert, true, null);
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
            var onlyNumbersAlert = UIAlertController.Create("Oops", AppResources.NumbersOnlyWarning, UIAlertControllerStyle.ActionSheet);
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

        private void OnImageSet(bool wasImageSet)
        {
            _wasImageSet = wasImageSet;
        }

        private void SetLabelsTexts()
        {
            RestaurantNameLabel.Text = AppResources.RestaurantNameLabel;
            RestaurantTypeLabel.Text = AppResources.RestaurantTypeLabel;
            RestaurantAddressLabel.Text = AppResources.RestaurantAddressLabel;
            RestaurantPhoneLabel.Text = AppResources.RestaurantPhoneLabel;
            RestaurantDescriptionLabel.Text = AppResources.RestaurantDescriptionLabel;
        }

        private void SetTextFieldsPlaceholders()
        {
            RestaurantNameTextField.Placeholder = AppResources.RestaurantNameTextFieldPlaceholder;
            RestaurantTypeTextField.Placeholder = AppResources.RestaurantTypeTextFieldPlaceholder;
            RestaurantAddressTextField.Placeholder = AppResources.RestaurantAddressTextFieldPlaceholder;
            RestaurantPhoneTextField.Placeholder = AppResources.RestaurantPhoneTextFieldPlaceholder;
            RestaurantDescriptionTextView.Text = AppResources.RestaurantDescriptionTextViewPlaceholder;
        }
    }
}