using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;

namespace CivilPrototype
{
	public class LoginView : UIView
	{
		UIViewController rootControl;
		public LoginView (RectangleF frame,UIViewController rootControl) :base()
		{
			this.Frame = frame;
			this.rootControl = rootControl;
			BackgroundColor = UIColor.FromRGB(241,241,241);

			UITextField usernameField, passwordField;
			UITextView titleView;
			UIImageView topImageView;
			UIView topView, contentView;
			CAGradientLayer gradient = new CAGradientLayer ();
			gradient.Frame = Bounds;
			gradient.Colors = new CGColor[]{UIColor.White.CGColor, UIColor.Black.CGColor};
			//Layer.InsertSublayer (gradient, 0);
			// keep the code the username UITextField
			topView = new UIView {
				Frame = new RectangleF (0, 
					25,
					Bounds.Width, 
					151),
				BackgroundColor = UIColor.FromRGB(241,241,241)
			};
			topImageView = new UIImageView {
				Frame = new RectangleF ((Bounds.Width/2)-((Bounds.Width-130)/2), 
					0,
					Bounds.Width-130, 
					150),
				Image = UIImage.FromFile("iphone.png")
			};
			var topHR = new UIView {
				BackgroundColor = UIColor.FromRGB(230,230,230),
				Frame = new RectangleF (0, 
					topImageView.Frame.Height, 
					topView.Bounds.Width,
					1),
			};
			topView.AddSubview (topImageView);
			topView.AddSubview (topHR);
			contentView = new UIView {
				Frame = new RectangleF (0, 
					topView.Frame.Height + topView.Frame.Y,
					Bounds.Width, 
					Bounds.Height-topView.Frame.Height),
				BackgroundColor = UIColor.White
			};
			titleView = new UITextView {
				Text = "Civil",
				TextColor = UIColor.FromRGB(75,75,75),
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (DesignConstants.HeaderFrameX, 
					0,
					Bounds.Width + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight),
			};
			var inputView = new RoundableUIView {
				CornerRadius = 5,
				BorderColor = UIColor.FromRGB(230,230,230).CGColor,
				BorderWidth = 1.0f,
				BackgroundColor = UIColor.White,
				Frame = new RectangleF  ((Bounds.Width/2) - ((Bounds.Width - 20)/2), 
					(2 * DesignConstants.TextFieldMarginBottom)+titleView.Frame.Y, 
										Bounds.Width - 20, 
										(DesignConstants.TextFieldHeight * 2) + 5)
				
			};
			usernameField = new UITextField {
				Placeholder = "Username",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName (DesignConstants.TextFieldFontStyle, DesignConstants.TextFieldFontSize),
				Frame = new RectangleF ((inputView.Bounds.Width/2) - ((inputView.Bounds.Width - 20)/2), 
					0, 
					inputView.Bounds.Width - 20, 
					DesignConstants.TextFieldHeight)
			};
			var hr = new UIView {
				BackgroundColor = UIColor.FromRGB(230,230,230),
				Frame = new RectangleF ((inputView.Bounds.Width/2) - ((inputView.Bounds.Width - 20)/2), 
										DesignConstants.TextFieldHeight+2, 
										inputView.Bounds.Width - 20,
										1),

			};
			passwordField = new UITextField {
				Placeholder = "Password",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName (DesignConstants.TextFieldFontStyle, DesignConstants.TextFieldFontSize),
				Frame = new RectangleF ((inputView.Bounds.Width/2) - ((inputView.Bounds.Width - 20)/2), 
					DesignConstants.TextFieldHeight+5, 
					inputView.Bounds.Width - 20, 
					DesignConstants.TextFieldHeight),
				SecureTextEntry = true
			};
			inputView.Add (usernameField);
			inputView.Add (passwordField);
			inputView.Add (hr);
			var submitButton = UIButton.FromType (DesignConstants.ButtonType);

			submitButton.SetTitleColor (UIColor.White, UIControlState.Normal);

			UIImage greenButtonImage = UIImage.FromFile("login.png").CreateResizableImage(new UIEdgeInsets(18,18,18,18));
			submitButton.SetBackgroundImage(greenButtonImage,UIControlState.Normal);

			UIImage greenButtonImageDark = UIImage.FromFile("loginDark.png").CreateResizableImage(new UIEdgeInsets(18,18,18,18));
			submitButton.SetBackgroundImage(greenButtonImageDark,UIControlState.Highlighted);

			submitButton.Frame = new RectangleF (DesignConstants.ButtonFrameX, 
				inputView.Frame.Y + inputView.Frame.Height + 20, 
				Bounds.Width + DesignConstants.ButtonWidth, 
				DesignConstants.ButtonHeight);

			submitButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.LargeButtonFontSize);
			submitButton.SetTitle ("Login", UIControlState.Normal);
			submitButton.TouchUpInside += delegate {
				string username = usernameField.Text;
				string password = passwordField.Text;
				LoginUserAsync (username, password);
			};
			var minorButtonsView = new UIView {

				Frame = new RectangleF(DesignConstants.ButtonFrameX, 
				submitButton.Frame.Y + submitButton.Frame.Height + 10, 
				Bounds.Width + DesignConstants.ButtonWidth, 
					DesignConstants.ButtonHeight),

			};
			var createAccountButton = UIButton.FromType (DesignConstants.ButtonType);

			createAccountButton.SetTitleColor (UIColor.FromRGB(75,75,75), UIControlState.Normal);

			UIImage grayButtonImage = UIImage.FromFile("createAcc.png").CreateResizableImage(new UIEdgeInsets(18,18,18,18));
			createAccountButton.SetBackgroundImage(grayButtonImage,UIControlState.Normal);

			UIImage grayButtonImageDark = UIImage.FromFile("createAccDark.png").CreateResizableImage(new UIEdgeInsets(18,18,18,18));
			createAccountButton.SetBackgroundImage(grayButtonImageDark,UIControlState.Highlighted);

			createAccountButton.Frame = new RectangleF (minorButtonsView.Bounds.Width*.40f, 
				0, 
				minorButtonsView.Bounds.Width * .60f, 
				DesignConstants.ButtonHeight);

			createAccountButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			createAccountButton.SetTitle ("Create Account", UIControlState.Normal);
			createAccountButton.TouchUpInside += delegate {
				//rootControl.NavigationController.PushViewController (new CreateAccountController (rootControl.NavigationController), true);

			};
			var forgotPassword = UIButton.FromType (UIButtonType.RoundedRect);
			forgotPassword.Frame = new RectangleF (0,0,minorButtonsView.Bounds.Width * .40f,DesignConstants.ButtonHeight);
			forgotPassword.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			forgotPassword.SetTitle ("Forgot Password", UIControlState.Normal);
			minorButtonsView.AddSubview (createAccountButton);
			minorButtonsView.AddSubview (forgotPassword);
			contentView.AddSubview (titleView);
			contentView.AddSubview (inputView);
			contentView.AddSubview (submitButton);
			contentView.AddSubview (minorButtonsView);
			AddSubview (topView);
			AddSubview (contentView);
		}
		public async void LoginUserAsync (string username, string password)
		{
			await DataLayer.LoginUser (username, password);
			rootControl.ViewDidLoad ();
		}
	}
}

