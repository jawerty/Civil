using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using FlyoutNavigation;
using MonoTouch.Dialog;
using System.Drawing;
using System.Threading;

namespace CivilPrototype
{
	partial class RootViewController : UIViewController
	{
		FlyoutNavigationController navigation;
		bool userLoggedIn = false;
		EditProfileButton editButton;
		UITextView profileView;
		UIView mainProfileView;
		UIScrollView scrollProfileView;
		public RootViewController (IntPtr handle) : base (handle)
		{
		}

		//Async
		public async void LoginUserAsync(string username,string password){

			await DataLayer.LoginUser(username,password);
			this.ViewDidLoad ();
		}

		private void SlowMethod ()
		{
			Thread.Sleep (300);
			InvokeOnMainThread (delegate {
				editButton.BackgroundColor = UIColor.White;
			});
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			userLoggedIn = NSUserDefaults.StandardUserDefaults.BoolForKey ("userLoggedIn");
			NavigationController.NavigationBarHidden = true;
			if (userLoggedIn) {

				string userFirstName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserFirstName");
				string userLastName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserLastName");
				string userEmail = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserEmail");
				string userName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserUsername");
				editButton = new EditProfileButton (View.Bounds.Width);
				editButton.ButtonTapped += delegate(NSSet touches, UIEvent evt) {
					editButton.BackgroundColor = DesignConstants.lgrey;
					ThreadPool.QueueUserWorkItem (o => SlowMethod ());
					InvokeOnMainThread(delegate{
						scrollProfileView.Add(new UITextField{
							Placeholder = "Username",
							Frame = new RectangleF((View.Bounds.Width/2) - 100,200,200,40),
							BackgroundColor = UIColor.White,
							BorderStyle = DesignConstants.TextFieldBorderStyle,
						});
						scrollProfileView.Add(new UITextField{
								Placeholder = "Email",
								Frame = new RectangleF((View.Bounds.Width/2) - 100,300,200,40),
								BackgroundColor = UIColor.White,
								BorderStyle = DesignConstants.TextFieldBorderStyle,
						});
						scrollProfileView.Add(new UITextField{
								Placeholder = "First Name",
								Frame = new RectangleF((View.Bounds.Width/2) - 100,400,200,40),
								BackgroundColor = UIColor.White,
								BorderStyle = DesignConstants.TextFieldBorderStyle,
							});
						scrollProfileView.Add(new UITextField{
								Placeholder = "Last Name",
								Frame = new RectangleF((View.Bounds.Width/2) - 100,500,200,40),
								BackgroundColor = UIColor.White,
								BorderStyle = DesignConstants.TextFieldBorderStyle,
							}		);
					
					});

				};
				profileView = new UITextView {
					Text = "Profile",
					Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderFontSize),
					BackgroundColor = DesignConstants.HeaderBackground,
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (DesignConstants.HeaderFrameX, 
						DesignConstants.HeaderFrameY, 
						View.Bounds.Width + DesignConstants.HeaderFrameWidth, 
						DesignConstants.HeaderFrameHeight)
				};
				scrollProfileView = new UIScrollView (new RectangleF (0, 50, View.Bounds.Width, 500)) {
					new UITextView {
						Text = "Username: " + NSUserDefaults.StandardUserDefaults.StringForKey("currentUserUsername"),
						Frame = new RectangleF ((View.Bounds.Width / 2) - 100, 150, 200, 40),
						BackgroundColor = UIColor.Clear,
					},
					new UITextView {
						Text = "Email: " + NSUserDefaults.StandardUserDefaults.StringForKey("currentUserEmail"),
						Frame = new RectangleF ((View.Bounds.Width / 2) - 100, 250, 200, 40),
						BackgroundColor = UIColor.Clear,
					},
					new UITextView {
						Text = "First Name: " + NSUserDefaults.StandardUserDefaults.StringForKey("currentUserFirstName"),
						Frame = new RectangleF ((View.Bounds.Width / 2) - 100, 350, 200, 40),
						BackgroundColor = UIColor.Clear,
					},
					new UITextView {
						Text = "Last Name: " + NSUserDefaults.StandardUserDefaults.StringForKey("currentUserLastName"),
						Frame = new RectangleF ((View.Bounds.Width / 2) - 100, 450, 200, 40),
						BackgroundColor = UIColor.Clear,
					},
				};
				scrollProfileView.ContentSize = new SizeF (View.Bounds.Width, 650);
				mainProfileView = new UIView (new RectangleF(0,0,View.Bounds.Width,View.Frame.Height)) {
					scrollProfileView,
					profileView,
					editButton,
				};
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
						MainView = mainProfileView,
					},
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
							LoginUserAsync(username,password);
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
