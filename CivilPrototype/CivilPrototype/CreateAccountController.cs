
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
			this.View.BackgroundColor = UIColor.White;
			UITextField firstNameField,lastNameField,emailField,usernameField, passwordField,passwordCheckField;
			UITextView titleView;
			// keep the code the username UITextField
			float h = 31.0f;
			float marginBot = 20;
			float w = View.Bounds.Width;
			titleView = new UITextView {

				Text = "Create Account",
				Frame = new RectangleF (10, h - 5, w - 20, h),
				TextAlignment = UITextAlignment.Center
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
			submitButton.Frame = new RectangleF(10, 7*(h+marginBot), w - 20, 44);
			submitButton.SetTitle("Sign up", UIControlState.Normal);
			submitButton.TouchUpInside += delegate
			{
				string firstName = firstNameField.Text;
				string lastName = lastNameField.Text;
				string username = usernameField.Text;
				string password = passwordField.Text;
				string email = emailField.Text;
				string passwordCheck = passwordCheckField.Text;
				CreateUser(firstName,lastName,email,username,password,passwordCheck);
			};
			var loginButton = UIButton.FromType(UIButtonType.RoundedRect);
			loginButton.Frame = new RectangleF(10, 8*(h+marginBot), w - 20, 44);
			loginButton.SetTitle("Login", UIControlState.Normal);
			loginButton.TouchUpInside += delegate
			{
				navigation.PopToRootViewController(true);

			};
			View.AddSubview(loginButton);
			View.AddSubview(submitButton);
			View.AddSubview (titleView);
			View.AddSubview(firstNameField); 
			View.AddSubview(lastNameField);
			View.AddSubview(emailField); 
			View.AddSubview(usernameField);
			View.AddSubview(passwordField); 
			View.AddSubview(passwordCheckField);
			// Perform any additional setup after loading the view, typically from a nib.
		}
		public bool CreateUser(string firstName,string lastName,string email,string username,string password,string passwordCheck){

			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users");

			var postData = "{\"firstName\": " + "\"" + firstName + "\"" + "," ;
			postData += "\"lastName\": " + "\"" + lastName  + "\"" + "," ;
			postData += "\"email\": " + "\"" + email  + "\"" + "," ;
			postData += "\"username\": " + "\"" + username  + "\"" + "," ;
			postData += "\"password\": " + "\"" + password  + "\"" + "," ;
			postData += "\"passwordCheck\": " + "\"" + passwordCheck  + "\"" + "}" ;
			Console.WriteLine (postData);
			var data = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			var response = (HttpWebResponse)request.GetResponse();

			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			Console.WriteLine (responseString);
			return false;
		}
	}
}

