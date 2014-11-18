// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace CivilPrototype
{
	[Register ("LoginController")]
	partial class LoginController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton LoginButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView LoginView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Password { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField Username { get; set; }

		[Action ("LoginButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void LoginButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
			}
			if (LoginView != null) {
				LoginView.Dispose ();
				LoginView = null;
			}
			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}
			if (Username != null) {
				Username.Dispose ();
				Username = null;
			}
		}
	}
}
