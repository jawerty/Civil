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
		public RootViewController (IntPtr handle) : base (handle)
		{
		}
		public bool LoginUser(string username, string password){



		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			if (userLoggedIn) {
				var viewsControl = new [] {
					new NavigableViewController () { Navigation = navigation },
					new NavigableViewController () { Navigation = navigation },
					new NavigableViewController () { Navigation = navigation },
				};
				navigation = new FlyoutNavigationController {
					// Create the navigation menu
					NavigationRoot = new RootElement ("Navigation") {
						new Section ("Pages") {
							new StringElement ("Animals"),
							new StringElement ("Vegetables"),
							new StringElement ("Minerals"),
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
					UITextField usernameField, passwordField;
					// keep the code the username UITextField
					float h = 31.0f;
					float w = View.Bounds.Width;
					Title = "Login";
					usernameField = new UITextField
					{
						Placeholder = "Username",
						BorderStyle = UITextBorderStyle.RoundedRect,
						Frame = new RectangleF(10, 32, w -20, h)
					};
					passwordField = new UITextField
					{
						Placeholder = "Password",
						BorderStyle = UITextBorderStyle.RoundedRect,
						Frame = new RectangleF(10, 35+h, w -20, h),
						SecureTextEntry = true
					};
					var submitButton = UIButton.FromType(UIButtonType.RoundedRect);
					submitButton.Frame = new RectangleF(10, 120, w - 20, 44);
					submitButton.SetTitle("Login", UIControlState.Normal);
					submitButton.TouchUpInside += delegate
					{
						string username = usernameField.Text;
						string password = passwordField.Text;
						
					};
					View.AddSubview(submitButton);
					View.AddSubview(usernameField); 
					View.AddSubview(passwordField);
			}
		}
	}
}
