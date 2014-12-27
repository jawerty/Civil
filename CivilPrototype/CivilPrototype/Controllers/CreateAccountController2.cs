
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
	public class CreateAccountController2 : UIViewController
	{
		UINavigationController navigation;
		float skillsListHeight;
		public CreateAccountController2 (UINavigationController nav) : base ()
		{
			navigation = nav;
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
			NavigationController.NavigationBarHidden = false;
			NavigationController.NavigationBar.Translucent = false;
			NavigationController.NavigationBar.BarTintColor = UIColor.White;
			NavigationController.NavigationBar.BackItem.Title = "Back";
			NSDictionary attributes = NSDictionary.FromObjectAndKey(
				DesignConstants.dgrey, UIStringAttributeKey.ForegroundColor);
			NavigationController.NavigationBar.TopItem.Title = "Sign up";
			//NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes(attributes)
			var s = new UIBarButtonItem (new UIImageView{Image = UIImage.FromFile("leaf.png"),Frame = new RectangleF(0,0,30,35)});
			s.Width = 50;
			this.NavigationItem.SetRightBarButtonItem (s,true);
			var t = NavigationController.NavigationBar;
			//NavigationController.NavigationBar.TintColor = DesignConstants.dgrey;
			CAGradientLayer gradient = new CAGradientLayer ();
			gradient.Frame = View.Bounds;
			gradient.Colors = new CGColor[]{UIColor.FromRGB(122,255,145).CGColor, UIColor.FromRGB(68,213,80).CGColor};
			View.Layer.InsertSublayer (gradient, 0);
			UITextField firstNameField,lastNameField,emailField,usernameField, passwordField,passwordCheckField;
			UITextView titleView;
			float marginAdjustment = 7.0f;
			float heightAdjustment = 7.0f;
			// keep the code the username UITextField
			titleView = new UITextView {
				Text = "Create an account",
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF(-DesignConstants.HeaderFrameX, 
					DesignConstants.HeaderFrameY, 
					View.Bounds.Height + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight),
			};
			firstNameField = new UITextField
			{
				Placeholder = "First Name",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, 2*(DesignConstants.TextFieldMarginBottom-marginAdjustment), View.Bounds.Width + DesignConstants.TextFieldWidth, DesignConstants.TextFieldHeight-heightAdjustment)
			};
			lastNameField = new UITextField
			{
				Placeholder = "Last Name",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, 2*(2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)), View.Bounds.Width + DesignConstants.TextFieldWidth, DesignConstants.TextFieldHeight-heightAdjustment),
			};
			emailField = new UITextField
			{
				Placeholder = "Email",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, 3*(2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)), View.Bounds.Width + DesignConstants.TextFieldWidth, DesignConstants.TextFieldHeight-heightAdjustment)
			};
			usernameField = new UITextField
			{
				Placeholder = "Username",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, 4*(2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)), View.Bounds.Width + DesignConstants.TextFieldWidth, DesignConstants.TextFieldHeight-heightAdjustment),
			};
			passwordField = new UITextField
			{
				Placeholder = "Password",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, 5*(2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)), View.Bounds.Width + DesignConstants.TextFieldWidth, DesignConstants.TextFieldHeight-heightAdjustment),
				SecureTextEntry = true
			};
			passwordCheckField = new UITextField
			{
				Placeholder = "Confirm Password",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, 6*(2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)), View.Bounds.Width + DesignConstants.TextFieldWidth, DesignConstants.TextFieldHeight-heightAdjustment),
				SecureTextEntry = true
			};
			var skillsList = new EditableListView (new List<string>{},new RectangleF (DesignConstants.TextFieldFrameX, 7*(2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)),	View.Bounds.Width + DesignConstants.TextFieldWidth, 200 ));
			skillsList.Frame = new RectangleF (DesignConstants.TextFieldFrameX, 7*(2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)),	View.Bounds.Width + DesignConstants.TextFieldWidth, skillsList.Height );
			skillsListHeight = skillsList.Height;
			var submitButton = UIButton.FromType(DesignConstants.ButtonType);
			submitButton.Frame = new RectangleF(DesignConstants.ButtonFrameX, 375, View.Bounds.Width + DesignConstants.ButtonWidth, DesignConstants.ButtonHeight);
			submitButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.LargeButtonFontSize);
			submitButton.SetTitle("Sign up!", UIControlState.Normal);
			submitButton.TouchUpInside += delegate
			{
				string firstName = firstNameField.Text;
				string lastName = lastNameField.Text;
				string username = usernameField.Text;
				string password = passwordField.Text;
				string email = emailField.Text;
				string passwordCheck = passwordCheckField.Text;
				CreateAsync(firstName,lastName,email,username,password,passwordCheck);
			};
			var loginButton = UIButton.FromType(DesignConstants.ButtonType);
			loginButton.Frame = new RectangleF(DesignConstants.ButtonFrameX, 435, View.Bounds.Width + DesignConstants.ButtonWidth, DesignConstants.ButtonHeight);
			loginButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			loginButton.SetTitle("Back to Login", UIControlState.Normal);
			loginButton.TouchUpInside += delegate
			{
				navigation.PopToRootViewController(true);
			};
			//			View.AddSubview(loginButton);
			//			View.AddSubview(submitButton);
			//			View.AddSubview(titleView);
			//			View.AddSubview(firstNameField); 
			//			View.AddSubview(lastNameField);
			//			View.AddSubview(emailField); 
			//			View.AddSubview(usernameField);
			//			View.AddSubview(passwordField); 
			//			View.AddSubview(passwordCheckField);
			// Perform any additional setup after loading the view, typically from a nib.
		}


		//Async
		public async void CreateAsync(string firstName,string lastName,string email,string username,string password,string passwordCheck){
			await DataLayer.CreateUser(firstName,lastName,email,username,password,passwordCheck);
			navigation.PopToRootViewController(true);
			navigation.ViewControllers[0].ViewDidLoad();
		}
	}
}

