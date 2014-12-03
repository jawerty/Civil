using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using FlyoutNavigation;
using MonoTouch.Dialog;
using System.Drawing;

namespace CivilPrototype
{
	partial class RootViewController : UIViewController
	{
		FlyoutNavigationController navigation;
		bool userLoggedIn = false;
		UIColor teal = UIColor.FromRGB (12, 91, 108);
		UIColor peakcock = UIColor.FromRGB (7, 57, 62);
		UIColor surfer = UIColor.FromRGB (26, 149, 149);
		UIColor grey = UIColor.FromRGB (110, 115, 123);
		UIColor lgrey = UIColor.FromRGB (192, 198, 200);
		public RootViewController (IntPtr handle) : base (handle)
		{
		}
		public bool LoginUser(string username, string password){

			return true;

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			userLoggedIn = NSUserDefaults.StandardUserDefaults.BoolForKey ("userLoggedIn");
			//userLoggedIn = true;
			NavigationController.NavigationBarHidden = true;

			if (userLoggedIn) {
				var viewsControl = new [] {
					new NavigableViewController () { Navigation = navigation, 
						MainView = new UIView(View.Bounds){
							new UITextView
							{
								Text = "Discover",
								BackgroundColor = UIColor.Clear,
								Font = UIFont.FromName("Baskerville",25),
								TextAlignment = UITextAlignment.Center,
								Frame = new RectangleF(40, 10.0f, View.Bounds.Width - 80, 40.0f)
							},
						} 
					},
					new NavigableViewController () { Navigation = navigation, 
						MainView = new UIView(View.Bounds){
							new UITextView
							{
								Text = "Profile",
								Font = UIFont.FromName("Baskerville",25),
								BackgroundColor = UIColor.Clear,
								TextAlignment = UITextAlignment.Center,
								Frame = new RectangleF(40, 10.0f, View.Bounds.Width - 80, 40.0f)
							},
						}  },
					new NavigableViewController () { Navigation = navigation, 
						MainView = new UIView(View.Bounds){
							new UITextView
							{
								Text = "Settings",
								BackgroundColor = UIColor.Clear,
								Font = UIFont.FromName("Baskerville",25),
								TextAlignment = UITextAlignment.Center,
								Frame = new RectangleF(40, 10.0f, View.Bounds.Width - 80, 40.0f),
							},
						}  },
				};
				navigation = new FlyoutNavigationController {
					// Create the navigation menu
					NavigationRoot = new RootElement ("Navigation") {
						new Section ("Civil") {
							new StringElement ("Discover"),
							new StringElement ("Profile"),
							new StringElement ("Settings"),
						}
					},
					// Supply view controllers corresponding to menu items:
					ViewControllers = viewsControl
				};
				viewsControl [0].Navigation = navigation;
				viewsControl [1].Navigation = navigation;
				viewsControl [2].Navigation = navigation;

				// Show the navigation view
				View.AddSubview (navigation.View);
				navigation.View.Hidden = true;
				navigation.ToggleMenu ();
				navigation.ToggleMenu ();
				navigation.View.Hidden = false;
			} else {
					View.BackgroundColor = lgrey;
					UITextField usernameField, passwordField;
					UITextView titleView;
					// keep the code the username UITextField
					float h = 45.0f;
					float w = View.Bounds.Width;
					titleView = new UITextView {
						Text = "Civil",
						BackgroundColor = UIColor.Clear,
						Font = UIFont.FromName ("Baskerville", 25),
						TextAlignment = UITextAlignment.Center,
						Frame = new RectangleF(10, 20, w -20, h),
					};
					usernameField = new UITextField
					{
						Placeholder = "Username",
						BorderStyle = UITextBorderStyle.RoundedRect,
						Frame = new RectangleF(10, 32+(2*h), w -20, h)
					};
					passwordField = new UITextField
					{
						Placeholder = "Password",
						BorderStyle = UITextBorderStyle.RoundedRect,
						Frame = new RectangleF(10, 40+(3*h), w -20, h),
						SecureTextEntry = true
					};
					var submitButton = UIButton.FromType(UIButtonType.RoundedRect);
					submitButton.Frame = new RectangleF(10, 40+(5*h), w - 20, 50);
					submitButton.Font = UIFont.FromName ("COPPERPLATE", 30);
					submitButton.SetTitle("Login", UIControlState.Normal);
					submitButton.TouchUpInside += delegate
					{
						string username = usernameField.Text;
						string password = passwordField.Text;
						LoginUser(username,password);
					};
					var createAccountButton = UIButton.FromType(UIButtonType.RoundedRect);
					createAccountButton.Frame = new RectangleF(10, 400, w - 20, 50);
					createAccountButton.Font = UIFont.FromName ("COPPERPLATE", 15);
					createAccountButton.SetTitle("Create Account", UIControlState.Normal);
					createAccountButton.TouchUpInside += delegate
					{
					NavigationController.PushViewController(new CreateAccountController(this.NavigationController),true);
					};
					View.AddSubview(createAccountButton);
					View.AddSubview(submitButton);
					View.AddSubview(usernameField); 
					View.AddSubview(passwordField);
					View.AddSubview (titleView);
			}
		}
	}
}
