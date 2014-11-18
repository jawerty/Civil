using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace CivilPrototype
{
	partial class LoginController : UIViewController
	{
		public LoginController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Username.Text = "Hello";
		}
		partial void LoginButton_TouchUpInside (UIButton sender)
		{
			LoginUser();
			var controller = this.Storyboard.InstantiateViewController("HomeViewController") as HomeController;
			NavigationController.PushViewController(controller,true);

		}

		public void LoginUser(){

			string username = Username.Text;
			string password = Password.Text;
			//TODO login user code
			//TODO int userid = GetId()
			NSUserDefaults.StandardUserDefaults.SetBool (true, "isLoggedIn");
			NSUserDefaults.StandardUserDefaults.SetInt (5, "currentUserID");
		}


	}
}
