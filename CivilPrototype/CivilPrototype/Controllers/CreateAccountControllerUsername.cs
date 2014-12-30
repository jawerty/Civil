
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace CivilPrototype
{
	public class CreateAccountControllerUsername : UIViewController
	{
		float skillsListHeight;
		public CreateAccountControllerUsername () : base ()
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			NavigationController.NavigationBarHidden = false;
//			NavigationController.NavigationBar.Translucent = false;
//			NavigationController.NavigationBar.BarTintColor = UIColor.White;
			NavigationItem.Title = "Sign Up";
			NSDictionary attributes = NSDictionary.FromObjectAndKey(UIFont.FromName("GeosansLight",30),UIStringAttributeKey.Font);
			NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes (attributes);
			var s = new UIBarButtonItem (new UIImageView{Image = UIImage.FromFile("civil.png"),Frame = new RectangleF(0,0,35,40)});
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem ("\U000025C0\U0000FE0E Back", UIBarButtonItemStyle.Done, delegate {NavigationController.PopViewControllerAnimated(true);
			});
			s.Width = 50;
			this.NavigationItem.SetRightBarButtonItem (s,true);
			CAGradientLayer gradient = new CAGradientLayer ();
			gradient.Frame = View.Bounds;
			gradient.Colors = new CGColor[]{UIColor.FromRGB(122,255,145).CGColor, UIColor.FromRGB(68,213,80).CGColor};
			//View.Layer.InsertSublayer (gradient, 0);
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("crossword.png"));
			//View.BackgroundColor = UIColor.White;
			View.ClipsToBounds = true;
			UITextView header, subHeader;
			UIView vLine;
			UITextField usernameField, passwordField, confirmPasswordField;
			RoundableUILabel emailLabel;
			UIButton nextButton;
			header = new CHeader("Now enter a username and a password") {

				Frame = new RectangleF (DesignConstants.GetMiddleX (View.Bounds.Width, View.Bounds.Width - 20), 25, View.Bounds.Width - 20, 160),
				Font = UIFont.FromName("GeosansLight",25),
			};
			usernameField = new CTextFieldWLabel(new RectangleF(DesignConstants.GetMiddleX(View.Bounds.Width,300),header.Frame.Y + header.Frame.Height + 20,300,50), "Username")
			{
				BorderStyle = UITextBorderStyle.RoundedRect,
				LeftViewMode = UITextFieldViewMode.Always,
				BackgroundColor = UIColor.White,
				SecureTextEntry = false
			};
			passwordField = new CTextFieldWLabel(new RectangleF(DesignConstants.GetMiddleX(View.Bounds.Width,300),usernameField.Frame.Y + usernameField.Frame.Height + 20,300,50), "Password")
			{
				BorderStyle = UITextBorderStyle.RoundedRect,
				LeftViewMode = UITextFieldViewMode.Always,
				BackgroundColor = UIColor.White,
				SecureTextEntry = true
			};
			confirmPasswordField = new CTextFieldWLabel(new RectangleF(DesignConstants.GetMiddleX(View.Bounds.Width,300),passwordField.Frame.Y + passwordField.Frame.Height + 5,300,50), "Confirm")
			{
				BorderStyle = UITextBorderStyle.RoundedRect,
				LeftViewMode = UITextFieldViewMode.Always,
				BackgroundColor = UIColor.White,
				SecureTextEntry = true
			};
			nextButton = new CGreenButton ("Next"){
				Frame = new RectangleF (DesignConstants.GetMiddleX(View.Bounds.Width,View.Bounds.Width-100),
					confirmPasswordField.Frame.Y + confirmPasswordField.Frame.Height + 50,
					View.Bounds.Width - 100,
					DesignConstants.ButtonHeight),
			};
			nextButton.TouchUpInside += delegate {
				NavigationController.PushViewController (new CreateAccountControllerReview(), true);
				NSUserDefaults.StandardUserDefaults.SetString(usernameField.Text,"createAccountUsername");
				NSUserDefaults.StandardUserDefaults.SetString(passwordField.Text,"createAccountPassword");
				NSUserDefaults.StandardUserDefaults.SetString(confirmPasswordField.Text,"createAccountConfirmPassword");
			};

			View.AddSubview (header);
			View.AddSubview(usernameField);
			View.AddSubview(passwordField);
			View.AddSubview(confirmPasswordField);
			View.AddSubview (nextButton);
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

