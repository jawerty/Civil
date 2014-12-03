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

			return true;

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			userLoggedIn = NSUserDefaults.StandardUserDefaults.BoolForKey ("userLoggedIn");
			NavigationController.NavigationBarHidden = true;

			if (userLoggedIn) {
				var viewsControl = new [] {
					new NavigableViewController () { Navigation = navigation, 
						MainView = new UIView(View.Bounds){
							new UITextView
							{
								Text = "Discover",
								BackgroundColor = DesignConstants.HeaderBackground,
								Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderFontSize),
								TextAlignment = DesignConstants.HeaderAlignment,
								Frame = new RectangleF(DesignConstants.HeaderFrameX, 
									DesignConstants.HeaderFrameY, 
									View.Bounds.Width + DesignConstants.HeaderFrameWidth, 
									DesignConstants.HeaderFrameHeight)
							},
						} 
					},
					new NavigableViewController () { Navigation = navigation, 
						MainView = new UIView(View.Bounds){
							new UITextView
							{
								Text = "Profile",
								Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderFontSize),
								BackgroundColor = DesignConstants.HeaderBackground,
								TextAlignment = DesignConstants.HeaderAlignment,
								Frame = new RectangleF(DesignConstants.HeaderFrameX, 
									DesignConstants.HeaderFrameY, 
									View.Bounds.Width + DesignConstants.HeaderFrameWidth, 
									DesignConstants.HeaderFrameHeight)
							},
						}  },
					new NavigableViewController () { Navigation = navigation, 
						MainView = new UIView(View.Bounds){
							new UITextView
							{
								Text = "Settings",
								BackgroundColor = DesignConstants.HeaderBackground,
								Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderFontSize),
								TextAlignment = DesignConstants.HeaderAlignment,
								Frame = new RectangleF(DesignConstants.HeaderFrameX, 
									DesignConstants.HeaderFrameY, 
									View.Bounds.Width + DesignConstants.HeaderFrameWidth, 
									DesignConstants.HeaderFrameHeight),
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
					View.BackgroundColor = DesignConstants.lgrey;
					UITextField usernameField, passwordField;
					UITextView titleView;
					// keep the code the username UITextField
					titleView = new UITextView {
						Text = "Civil",
						BackgroundColor = DesignConstants.HeaderBackground,
						Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderFontSize),
						TextAlignment = DesignConstants.HeaderAlignment,
						Frame = new RectangleF(DesignConstants.HeaderFrameX, 
						DesignConstants.HeaderFrameY + 10,
						View.Bounds.Width + DesignConstants.HeaderFrameWidth, 
						DesignConstants.HeaderFrameHeight),
					};
					usernameField = new UITextField
					{
						Placeholder = "Username",
						BorderStyle = DesignConstants.TextFieldBorderStyle,
					Frame = new RectangleF(DesignConstants.TextFieldFrameX, 
							(4*DesignConstants.TextFieldMarginBottom), 
							View.Bounds.Width + DesignConstants.TextFieldWidth, 
							DesignConstants.TextFieldHeight)
					};
					passwordField = new UITextField
					{
						Placeholder = "Password",
						BorderStyle = DesignConstants.TextFieldBorderStyle,
					Frame = new RectangleF(DesignConstants.TextFieldFrameX, 
							(6*DesignConstants.TextFieldMarginBottom), 
							View.Bounds.Width + DesignConstants.TextFieldWidth, 
							DesignConstants.TextFieldHeight),
						SecureTextEntry = true
					};
				var submitButton = UIButton.FromType(DesignConstants.ButtonType);
						submitButton.Frame = new RectangleF(DesignConstants.ButtonFrameX, 
							340, 
							View.Bounds.Width + DesignConstants.ButtonWidth, 
							DesignConstants.ButtonHeight);
						submitButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.HeaderFontSize);
						submitButton.SetTitle("Login", DesignConstants.ButtonControlState);
						submitButton.TouchUpInside += delegate
						{
							string username = usernameField.Text;
							string password = passwordField.Text;
							LoginUser(username,password);
						};
					var createAccountButton = UIButton.FromType(DesignConstants.ButtonType);
						createAccountButton.Frame = new RectangleF(DesignConstants.ButtonFrameX, 
							400, 
							View.Bounds.Width + DesignConstants.ButtonWidth, 
							DesignConstants.ButtonHeight);
						createAccountButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.HeaderFontSize);
						createAccountButton.SetTitle("Create Account", DesignConstants.ButtonControlState);
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
