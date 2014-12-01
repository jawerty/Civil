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
		public RootViewController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
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
				ViewControllers = new [] {
					new NavigableViewController() { Navigation = navigation },
					new NavigableViewController() { Navigation = navigation },
					new NavigableViewController() { Navigation = navigation },
				},
			};
			// Show the navigation view
			View.AddSubview (navigation.View);
			navigation.View.Hidden = true;
			navigation.ToggleMenu ();
			navigation.ToggleMenu ();
			navigation.View.Hidden = false;
		}

	}
}
