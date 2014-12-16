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
		public RootViewController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			userLoggedIn = NSUserDefaults.StandardUserDefaults.BoolForKey ("userLoggedIn");
			NavigationController.NavigationBarHidden = true;
			if (userLoggedIn) {
				var discoverView = new DiscoverView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height));
				var discoverController = new NavigableViewController () { 
					Navigation = navigation, 
					MainView = discoverView,
				};
				discoverController.touchMoved += (touches, evt) => {
					var touch = touches.AnyObject as UITouch;
					var location = touch.LocationInView (this.View);
					diff = location.X-lastTouchLocation.X;
					discoverView.MoveMovement(diff);
					Console.WriteLine("diff: " +diff);
					Console.WriteLine("this touch: " +location);
					Console.WriteLine("last touch: " +lastTouchLocation);
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
				var viewsControl = new [] {
					discoverController,
					new NavigableViewController () { Navigation = navigation, 
						MainView = new MainProfileView(new RectangleF(0,0,View.Bounds.Width,View.Bounds.Height)),
					},
					new NavigableViewController () { Navigation = navigation, 
						MainView = new UIView (View.Bounds) {
							new UITextView {
								Text = "Settings",
								BackgroundColor = DesignConstants.HeaderBackground,
								Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderFontSize),
								TextAlignment = DesignConstants.HeaderAlignment,
								Frame = new RectangleF (DesignConstants.HeaderFrameX, 
									DesignConstants.HeaderFrameY, 
									View.Bounds.Width + DesignConstants.HeaderFrameWidth, 
									DesignConstants.HeaderFrameHeight),
							},
						}
					},
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
				View.Add (new LoginView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height),this));
			}
		}
	}
}
