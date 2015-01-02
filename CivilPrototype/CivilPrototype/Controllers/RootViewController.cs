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
		PointF lastTouchLocation;
		float diff = 0;
		int timeToUpdate = 0;
		AddMovementButton postButton;
		public RootViewController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			ViewDidLoad ();
			NavigationController.NavigationBarHidden = true;
		}
		private void SlowMethod ()
		{
			Thread.Sleep (100);
			InvokeOnMainThread (delegate {
				postButton.Alpha = 1.0f;
				NavigationController.PushViewController(new CreateMovementController(NavigationController),true);
			});
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationItem.Title = "Back";
//			DataLayer.SendBitMap ();
			userLoggedIn = NSUserDefaults.StandardUserDefaults.BoolForKey ("userLoggedIn");
			userLoggedIn = true;
			NavigationController.NavigationBarHidden = true;
			if (userLoggedIn) {
				postButton = new AddMovementButton (View.Bounds.Width);
				postButton.ButtonTapped += delegate{
					postButton.Alpha = .5f;
					ThreadPool.QueueUserWorkItem (o => SlowMethod ());
				};
				var discoverView = new DiscoverView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height),this);
				var discoverController = new NavigableViewController (new CustomNavBar(){rightButton = postButton,header = "Discover"}) { 
					Navigation = navigation, 
					MainView = discoverView,
				};
				discoverController.touchMoved += (touches, evt) => {
					var touch = touches.AnyObject as UITouch;
					var location = touch.LocationInView (this.View);
					diff = location.X-lastTouchLocation.X;
					discoverView.MoveMovement(diff);
					lastTouchLocation= location;
				};
				discoverController.touchBegan += (touches, evt) => {
					var touch = touches.AnyObject as UITouch;
					var location = touch.LocationInView (this.View);
					lastTouchLocation = location;
				};
				discoverController.touchEnded += (touches, evt) => {
					discoverView.MoveMovement(10000);
				};
				var mainProfileView = new MainProfileView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height));
				var viewsControl = new [] {
					discoverController,
					new NavigableViewController (new CustomNavBar(){rightButton = mainProfileView.EditButton,header = "Profile"}) { Navigation = navigation, 
						MainView = mainProfileView,
					},
					new NavigableViewController (new CustomNavBar(){rightButton = postButton,header = "Settings"}) { Navigation = navigation, 
						MainView = new UIView (View.Bounds) {

						}
					},
				};
				var s = new CHeader ("Civil");
				s.Frame = new RectangleF (0, 0, 100, 80);
				navigation = new FlyoutNavigationController {
					// Create the navigation menu

					NavigationRoot = new RootElement ("") {
						new Section (s) {
							new ImageStringElement ("Discover",UIImage.FromImage(UIImage.FromFile("idea.png").CGImage,(UIImage.FromFile("idea.png").CurrentScale)*2f,UIImageOrientation.Up)),
							new ImageStringElement ("Profile",UIImage.FromImage(UIImage.FromFile("user.png").CGImage,(UIImage.FromFile("user.png").CurrentScale)*2f,UIImageOrientation.Up)),
							new ImageStringElement ("Settings",UIImage.FromImage(UIImage.FromFile("settings.png").CGImage,(UIImage.FromFile("settings.png").CurrentScale)*2f,UIImageOrientation.Up)),
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
				View.Add (new LoginView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height),this));
			}
		}
	}
}
