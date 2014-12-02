
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
		UIColor teal = UIColor.FromRGB (12, 91, 108);
		UIColor peakcock = UIColor.FromRGB (7, 57, 62);
		UIColor surfer = UIColor.FromRGB (26, 149, 149);
		UIColor grey = UIColor.FromRGB (110, 115, 123);
		UIColor lgrey = UIColor.FromRGB (192, 198, 200);
		string response;
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
			this.View.BackgroundColor = lgrey;
			UITextField firstNameField,lastNameField,emailField,usernameField, passwordField,passwordCheckField;
			UITextView titleView;
			// keep the code the username UITextField
			float h = 45.0f;
			float marginBot = 10;
			float w = View.Bounds.Width;
			titleView = new UITextView {
				Text = "Sign up",
				BackgroundColor = UIColor.Clear,
				Font = UIFont.FromName ("Baskerville", 25),
				TextAlignment = UITextAlignment.Center,
				Frame = new RectangleF(10, 10, w -20, h),
			};
			firstNameField = new UITextField
			{
				Placeholder = "First Name",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(10, h+marginBot, w -20, h)
			};
			lastNameField = new UITextField
			{
				Placeholder = "Last Name",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(10, 2*(h+marginBot), w -20, h),
			};
			emailField = new UITextField
			{
				Placeholder = "Email",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(10, 3*(h+marginBot), w -20, h)
			};
			usernameField = new UITextField
			{
				Placeholder = "Username",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(10, 4*(h+marginBot), w -20, h),
			};
			passwordField = new UITextField
			{
				Placeholder = "Password",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(10, 5*(h+marginBot), w -20, h),
				SecureTextEntry = true
			};
			passwordCheckField = new UITextField
			{
				Placeholder = "Confirm Password",
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(10, 6*(h+marginBot), w -20, h),
				SecureTextEntry = true
			};
			var submitButton = UIButton.FromType(UIButtonType.RoundedRect);
			submitButton.Frame = new RectangleF(10, 375, w - 20, 50);
			submitButton.Font = UIFont.FromName ("COPPERPLATE", 25);
			submitButton.SetTitle("Create my account!", UIControlState.Normal);
			submitButton.TouchUpInside += delegate
			{
				string firstName = firstNameField.Text;
				string lastName = lastNameField.Text;
				string username = usernameField.Text;
				string password = passwordField.Text;
				string email = emailField.Text;
				string passwordCheck = passwordCheckField.Text;
				CreateAsync(firstName,lastName,email,username,password,passwordCheck);
				navigation.PopToRootViewController(true);
				navigation.ViewControllers[0].ViewDidLoad();
			};
			var loginButton = UIButton.FromType(UIButtonType.RoundedRect);
			loginButton.Frame = new RectangleF(10, 430, w - 20, 50);
			loginButton.Font = UIFont.FromName ("COPPERPLATE", 15);
			loginButton.SetTitle("Login", UIControlState.Normal);
			loginButton.TouchUpInside += delegate
			{
				navigation.PopToRootViewController(true);
				navigation.ViewControllers[0].ViewDidLoad();
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
		public async void CreateAsync(string firstName,string lastName,string email,string username,string password,string passwordCheck){

			response = await DataLayer.CreateUser(firstName,lastName,email,username,password,passwordCheck);
			Console.WriteLine (response);
		}
	}
}

