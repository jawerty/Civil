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
				TextColor = UIColor.White,
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (DesignConstants.HeaderFrameX, 
					DesignConstants.HeaderFrameY + 50,
					Bounds.Width + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight),
			};
			var inputView = new RoundableUIView {
				CornerRadius = 5,
				BackgroundColor = UIColor.White,
				Frame = new RectangleF  ((Bounds.Width/2) - ((Bounds.Width - 20)/2), 
										(4 * DesignConstants.TextFieldMarginBottom), 
										Bounds.Width - 20, 
										(DesignConstants.TextFieldHeight * 2) + 5)
			};
			usernameField = new UITextField {
				Placeholder = "Username",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName (DesignConstants.TextFieldFontStyle, DesignConstants.TextFieldFontSize),
				TextColor = UIColor.Gray,
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
				TextColor = UIColor.Gray,
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
			UIImage greenButtonImage = UIImage.FromFile("greenButton.png").CreateResizableImage(new UIEdgeInsets(18,18,18,18));
			submitButton.SetBackgroundImage(greenButtonImage,UIControlState.Normal);
			UIImage greenButtonImageDark = UIImage.FromFile("greenButtonHighlight.png").CreateResizableImage(new UIEdgeInsets(18,18,18,18));
			submitButton.SetBackgroundImage(greenButtonImageDark,UIControlState.Highlighted);
			submitButton.Frame = new RectangleF (DesignConstants.ButtonFrameX, 
				inputView.Frame.Y + inputView.Frame.Height + 50, 
				Bounds.Width + DesignConstants.ButtonWidth, 
				DesignConstants.ButtonHeight);
			submitButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.LargeButtonFontSize);
			submitButton.SetTitle ("Login", DesignConstants.ButtonControlState);
			submitButton.TouchUpInside += delegate {
				string username = usernameField.Text;
				string password = passwordField.Text;
				//LoginUserAsync (username, password);
			};
			var createAccountButton = UIButton.FromType (DesignConstants.ButtonType);
			createAccountButton.Frame = new RectangleF (DesignConstants.ButtonFrameX, 
				400, 
				Bounds.Width + DesignConstants.ButtonWidth, 
				DesignConstants.ButtonHeight);
			createAccountButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			createAccountButton.SetTitle ("Create Account", DesignConstants.ButtonControlState);
			createAccountButton.TouchUpInside += delegate {
				rootControl.NavigationController.PushViewController (new CreateAccountController (rootControl.NavigationController), true);

			};
			//AddSubview (createAccountButton);
			AddSubview (submitButton);
			AddSubview (inputView);
			AddSubview (titleView);	
		}
		public async void LoginUserAsync (string username, string password)
		{
			await DataLayer.LoginUser (username, password);
			rootControl.ViewDidLoad ();
		}
	}
}

