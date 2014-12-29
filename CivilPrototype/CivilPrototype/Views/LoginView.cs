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
		CHeader titleView;
		UIImageView topImageView;
		UIView topView, contentView;
		RoundableUIView inputView;
		CHR topHR, contentHR;
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
			imageViewHeight = .3125f*Bounds.Height;
			imageViewWidth = CalculateImageViewWidth (topViewImage, imageViewHeight) + 40;
			hRHeight = 1;

			headerWidth = Bounds.Width + DesignConstants.HeaderFrameWidth;

			inputViewWidth = Bounds.Width - (.0625f*Bounds.Width);

			minorButtonsForgotShare = .4f;
			minorButtonsCreateShare = 1 - .4f;
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
			topHR = new CHR {
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
			titleView = new CHeader ("Civil");
			titleView.Frame = new RectangleF   (DesignConstants.GetMiddleX (Bounds.Width, headerWidth),
												0,
												headerWidth,
												DesignConstants.HeaderFrameHeight);

			inputView = new CTextFieldBundle(new RectangleF  	(DesignConstants.GetMiddleX (Bounds.Width, inputViewWidth),
																(.03125f * Bounds.Height) + titleView.Frame.Y + titleView.Frame.Height,
																inputViewWidth,
																(DesignConstants.TextFieldHeight * 2) + (.01125f * Bounds.Height)),
											 "Username",
											 false,
											 "Password",
											 true,
											 Bounds);
			var submitButton = new CGreenButton ("Login"){
				Frame = new RectangleF (DesignConstants.ButtonFrameX,
										inputView.Frame.Y + inputView.Frame.Height + (.08f*Bounds.Height),
										Bounds.Width + DesignConstants.ButtonWidth,
										DesignConstants.ButtonHeight),
			};
			submitButton.TouchUpInside += delegate {
				string username = usernameField.Text;
				string password = passwordField.Text;
				LoginUserAsync (username, password);
			};

			var minorButtonsView = new UIView {

				Frame = new RectangleF (DesignConstants.ButtonFrameX,
					submitButton.Frame.Y + submitButton.Frame.Height + (.02083f*Bounds.Height),
					Bounds.Width + DesignConstants.ButtonWidth,
					DesignConstants.ButtonHeight),

			};
			var createAccountButton = new CWhiteButton ("Create Account") {

				Frame = new RectangleF (minorButtonsView.Bounds.Width * minorButtonsForgotShare,
					0,
					minorButtonsView.Bounds.Width * minorButtonsCreateShare,
					DesignConstants.ButtonHeight)

			};
			createAccountButton.TouchUpInside += delegate {
				rootControl.NavigationController.PushViewController (new CreateAccountController (rootControl.NavigationController), true);

			};
			var forgotPassword = new CButton ("Forgot Password?").Button;
			forgotPassword.Frame = new RectangleF (0, 0, minorButtonsView.Bounds.Width * minorButtonsForgotShare, DesignConstants.ButtonHeight);

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