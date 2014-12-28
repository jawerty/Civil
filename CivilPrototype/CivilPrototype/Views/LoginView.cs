using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;

namespace CivilPrototype
{
	public class LoginView: UIView
	{
		UIViewController rootControl;
		UITextField usernameField, passwordField;
		UITextView titleView;
		UIImageView topImageView;
		UIView topView, contentView;
		RoundableUIView inputView;
		UIView topHR, contentHR;
		UIImage topViewImage, loginImage, loginImageDark, createImage, createImageDark;
		float pageStartHeight, imageViewHeight, imageViewWidth, hRHeight;//defines where navigation bar, etc ends
		float headerWidth;
		float inputViewWidth;
		float minorButtonsForgotShare, minorButtonsCreateShare;

		public LoginView (RectangleF frame, UIViewController rootControl) : base ()
		{
			this.Frame = frame;
			this.rootControl = rootControl;
			BackgroundColor = DesignConstants.lgrey;
			topViewImage = UIImage.FromFile ("iphone.png");

			pageStartHeight = 25;
			imageViewHeight = 150;
			imageViewWidth = CalculateImageViewWidth (topViewImage, imageViewHeight) + 40;
			hRHeight = 1;

			headerWidth = Bounds.Width + DesignConstants.HeaderFrameWidth;

			inputViewWidth = Bounds.Width - 20;

			loginImage = UIImage.FromFile ("login.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			loginImageDark = UIImage.FromFile ("loginDark.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			createImage = UIImage.FromFile ("createAcc.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			createImageDark = UIImage.FromFile ("createAccDark.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));

			minorButtonsForgotShare = .4f;
			minorButtonsCreateShare = 1 - .4f;
			//GRADIENT BACKGROUND CODE vvv
			//CAGradientLayer gradient = new CAGradientLayer ();
			//gradient.Frame = Bounds;
			//gradient.Colors = new CGColor[]{UIColor.White.CGColor, UIColor.Black.CGColor};
			//Layer.InsertSublayer (gradient, 0);
			topView = new UIView {
				Frame = new RectangleF (0,
					pageStartHeight,
					Bounds.Width,
					hRHeight+imageViewHeight),
				BackgroundColor = DesignConstants.lgrey
			};

			topImageView = new UIImageView {
				Frame = new RectangleF (DesignConstants.GetMiddleX(Bounds.Width,imageViewWidth),
					0,
					imageViewWidth,
					imageViewHeight),
				Image = topViewImage
			};
			topHR = new UIView {
				BackgroundColor = DesignConstants.grey,
				Frame = new RectangleF (0,
					topImageView.Frame.Height,
					topView.Bounds.Width,
					hRHeight),
			};
			topView.AddSubview (topImageView);
			topView.AddSubview (topHR);
			contentView = new UIView {
				Frame = new RectangleF (0,
					topView.Frame.Height + topView.Frame.Y,
					Bounds.Width,
					Bounds.Height - topView.Frame.Height),
				BackgroundColor = UIColor.White
			};
			titleView = new UITextView {
				Text = "Civil",
				TextColor = DesignConstants.dgrey,
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (DesignConstants.GetMiddleX(Bounds.Width,headerWidth),
					0,
					headerWidth,
					DesignConstants.HeaderFrameHeight),
			};


			inputView = new RoundableUIView {
				CornerRadius = 5,
				BorderColor = DesignConstants.grey.CGColor,
				BorderWidth = 1.0f,
				BackgroundColor = UIColor.White,
				Frame = new RectangleF (DesignConstants.GetMiddleX(Bounds.Width,inputViewWidth),
					15 + titleView.Frame.Y + titleView.Frame.Height,
					inputViewWidth,
					(DesignConstants.TextFieldHeight * 2) + 15)
			};
			usernameField = new UITextField {
				Placeholder = "Username",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName (DesignConstants.TextFieldFontStyle, DesignConstants.TextFieldFontSize),
				Frame = new RectangleF ((inputView.Bounds.Width / 2) - ((inputView.Bounds.Width - 20) / 2),
					0,
					inputView.Bounds.Width - 20,
					DesignConstants.TextFieldHeight)
			};
			contentHR = new UIView {
				BackgroundColor = UIColor.FromRGB (230, 230, 230),
				Frame = new RectangleF ((inputView.Bounds.Width / 2) - ((inputView.Bounds.Width - 20) / 2),
					DesignConstants.TextFieldHeight + 2,
					inputView.Bounds.Width - 20,
					1),
			};
			passwordField = new UITextField {
				Placeholder = "Password",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName (DesignConstants.TextFieldFontStyle, DesignConstants.TextFieldFontSize),
				Frame = new RectangleF ((inputView.Bounds.Width / 2) - ((inputView.Bounds.Width - 20) / 2),
					DesignConstants.TextFieldHeight + 5,
					inputView.Bounds.Width - 20,
					DesignConstants.TextFieldHeight),
				SecureTextEntry = true
			};
			inputView.Add (usernameField);
			inputView.Add (passwordField);
			inputView.Add (contentHR);

			var submitButton = UIButton.FromType (DesignConstants.ButtonType);
			submitButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			submitButton.SetBackgroundImage (loginImage, UIControlState.Normal);
			submitButton.SetBackgroundImage (loginImageDark, UIControlState.Highlighted);
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

				Frame = new RectangleF (DesignConstants.ButtonFrameX,
					submitButton.Frame.Y + submitButton.Frame.Height + 10,
					Bounds.Width + DesignConstants.ButtonWidth,
					DesignConstants.ButtonHeight),

			};

			var createAccountButton = UIButton.FromType (DesignConstants.ButtonType);
			createAccountButton.SetTitleColor (DesignConstants.dgrey, UIControlState.Normal);
			createAccountButton.SetBackgroundImage (createImage, UIControlState.Normal);
			createAccountButton.SetBackgroundImage (createImageDark, UIControlState.Highlighted);
			createAccountButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			createAccountButton.SetTitle ("Create Account", UIControlState.Normal);
			createAccountButton.Frame = new RectangleF (minorButtonsView.Bounds.Width * minorButtonsForgotShare,
				0,
				minorButtonsView.Bounds.Width * minorButtonsCreateShare,
				DesignConstants.ButtonHeight);
			createAccountButton.TouchUpInside += delegate {
				rootControl.NavigationController.PushViewController (new CreateAccountController (rootControl.NavigationController), true);

			};
			var forgotPassword = UIButton.FromType (UIButtonType.RoundedRect);
			forgotPassword.Frame = new RectangleF (0, 0, minorButtonsView.Bounds.Width * minorButtonsForgotShare, DesignConstants.ButtonHeight);
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
		public float CalculateImageViewWidth(UIImage image,float height){

			float realWidth = image.CGImage.Width;
			float realHeight = image.CGImage.Height;
			return (realWidth / realHeight) * height;
		}
	}
}