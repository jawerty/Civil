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
		public override void ViewWillAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			ViewDidLoad ();
			NavigationController.NavigationBarHidden = true;
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
				var discoverView = new DiscoverView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height),this);
				var discoverController = new NavigableViewController () { 
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
								Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize),
								TextAlignment = DesignConstants.HeaderAlignment,
								Frame = new RectangleF (DesignConstants.HeaderFrameX, 
									DesignConstants.HeaderFrameY, 
									View.Bounds.Width + DesignConstants.HeaderFrameWidth, 
									DesignConstants.HeaderFrameHeight),
							},
						}
					},
				};
				var s = new CHeader ("Civil");
				s.Frame = new RectangleF (0, 0, 100, 80);
				navigation = new FlyoutNavigationController {
					// Create the navigation menu

					NavigationRoot = new RootElement ("") {
						new Section (s) {
							new ImageStringElement ("Discover",UIImage.FromFile("discoverImage.png").Scale(new SizeF(20,20))),
							new ImageStringElement ("Profile",UIImage.FromFile("profileImage.png").Scale(new SizeF(20,20))),
							new ImageStringElement ("Settings",UIImage.FromFile("settingsImage.png").Scale(new SizeF(20,20))),
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
