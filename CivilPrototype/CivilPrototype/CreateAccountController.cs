
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.IO;
using System.Text;

namespace CivilPrototype
{
	public class CreateAccountController : UIViewController
	{
		UINavigationController navigation;
		public CreateAccountController (UINavigationController nav) : base ()
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
			this.View.BackgroundColor = DesignConstants.lgrey;
			UITextField firstNameField,lastNameField,emailField,usernameField, passwordField,passwordCheckField;
			UITextView titleView;
			float marginAdjustment = 7.0f;
			float heightAdjustment = 7.0f;
			// keep the code the username UITextField
			titleView = new UITextView {
				Text = "Create an account",
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderFontSize),
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
			var submitButton = UIButton.FromType(DesignConstants.ButtonType);
				submitButton.Frame = new RectangleF(DesignConstants.ButtonFrameX, 375, View.Bounds.Width + DesignConstants.ButtonWidth, DesignConstants.ButtonHeight);
				submitButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.LargeButtonFontSize);
				submitButton.SetTitle("Sign up!", DesignConstants.ButtonControlState);
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
				loginButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.LargeButtonFontSize);
				loginButton.SetTitle("Back to Login", DesignConstants.ButtonControlState);
				loginButton.TouchUpInside += delegate
				{
					navigation.PopToRootViewController(true);
				};
			View.AddSubview(loginButton);
			View.AddSubview(submitButton);
			View.AddSubview(titleView);
			View.AddSubview(firstNameField); 
			View.AddSubview(lastNameField);
			View.AddSubview(emailField); 
			View.AddSubview(usernameField);
			View.AddSubview(passwordField); 
			View.AddSubview(passwordCheckField);
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

