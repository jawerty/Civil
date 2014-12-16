using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace CivilPrototype
{
	public class LoginView : UIView
	{
		UIViewController rootControl;
		public LoginView (RectangleF frame,UIViewController rootControl) :base()
		{
			this.Frame = frame;
			this.rootControl = rootControl;
			BackgroundColor = DesignConstants.lgrey;
			UITextField usernameField, passwordField;
			UITextView titleView;
			// keep the code the username UITextField
			titleView = new UITextView {
				Text = "Civil",
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (DesignConstants.HeaderFrameX, 
					DesignConstants.HeaderFrameY + 10,
					Bounds.Width + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight),
			};
			usernameField = new UITextField {
				Placeholder = "Username",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF (DesignConstants.TextFieldFrameX, 
					(4 * DesignConstants.TextFieldMarginBottom), 
					Bounds.Width + DesignConstants.TextFieldWidth, 
					DesignConstants.TextFieldHeight)
			};
			passwordField = new UITextField {
				Placeholder = "Password",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF (DesignConstants.TextFieldFrameX, 
					(6 * DesignConstants.TextFieldMarginBottom), 
					Bounds.Width + DesignConstants.TextFieldWidth, 
					DesignConstants.TextFieldHeight),
				SecureTextEntry = true
			};
			var submitButton = UIButton.FromType (DesignConstants.ButtonType);
			submitButton.Frame = new RectangleF (DesignConstants.ButtonFrameX, 
				340, 
				Bounds.Width + DesignConstants.ButtonWidth, 
				DesignConstants.ButtonHeight);
			submitButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.HeaderFontSize);
			submitButton.SetTitle ("Login", DesignConstants.ButtonControlState);
			submitButton.TouchUpInside += delegate {
				string username = usernameField.Text;
				string password = passwordField.Text;
				LoginUserAsync (username, password);
			};
			var createAccountButton = UIButton.FromType (DesignConstants.ButtonType);
			createAccountButton.Frame = new RectangleF (DesignConstants.ButtonFrameX, 
				400, 
				Bounds.Width + DesignConstants.ButtonWidth, 
				DesignConstants.ButtonHeight);
			createAccountButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.HeaderFontSize);
			createAccountButton.SetTitle ("Create Account", DesignConstants.ButtonControlState);
			createAccountButton.TouchUpInside += delegate {
				rootControl.NavigationController.PushViewController (new CreateAccountController (rootControl.NavigationController), true);

			};
			AddSubview (createAccountButton);
			AddSubview (submitButton);
			AddSubview (usernameField); 
			AddSubview (passwordField);
			AddSubview (titleView);
		}
		public async void LoginUserAsync (string username, string password)
		{

			await DataLayer.LoginUser (username, password);
			rootControl.ViewDidLoad ();
		}
	}
}

