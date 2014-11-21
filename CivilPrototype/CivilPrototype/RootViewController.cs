using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using FlyoutNavigation;
using MonoTouch.Dialog;

namespace CivilPrototype
{
	partial class RootViewController : UIViewController
	{
		public RootViewController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var navigation = new FlyoutNavigationController {
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
					new UIViewController { View = new UILabel{ Text = "Something" } },
					new UIViewController { View = new UILabel { Text = "Vegetables (drag right)" } },
					new UIViewController { View = new UILabel { Text = "Minerals (drag right)" } },
				},
			};
			// Show the navigation view
			navigation.ToggleMenu ();
			View.AddSubview (navigation.View);
		}
	}
}
