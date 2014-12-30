
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace CivilPrototype
{
	public class CreateAccountControllerReview: UIViewController
	{
		float skillsListHeight;
		public CreateAccountControllerReview () : base ()
		{

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationItem.Title = "Sign Up";
			NSDictionary attributes = NSDictionary.FromObjectAndKey(UIFont.FromName("GeosansLight",30),UIStringAttributeKey.Font);
			NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes (attributes);
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem ("\U000025C0\U0000FE0E Back", UIBarButtonItemStyle.Done, delegate {NavigationController.PopViewControllerAnimated(true);
			});
			var s = new UIBarButtonItem (new UIImageView{Image = UIImage.FromFile("civil.png"),Frame = new RectangleF(0,0,35,40)});
			s.Width = 50;
			this.NavigationItem.SetRightBarButtonItem (s,true);
			CAGradientLayer gradient = new CAGradientLayer ();
			gradient.Frame = View.Bounds;
			gradient.Colors = new CGColor[]{UIColor.FromRGB(122,255,145).CGColor, UIColor.FromRGB(68,213,80).CGColor};
			//View.Layer.InsertSublayer (gradient, 0);
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("crossword.png"));
			//View.BackgroundColor = UIColor.White;
			View.ClipsToBounds = true;
			UITextView header;
			UIButton nextButton;
			header = new CHeader("Create Account") {

				Frame = new RectangleF (DesignConstants.GetMiddleX (View.Bounds.Width, View.Bounds.Width - 20), 25, View.Bounds.Width - 20, 200)
			};

			nextButton = new CGreenButton ("Sign up!"){
				Frame = new RectangleF (DesignConstants.GetMiddleX(View.Bounds.Width,View.Bounds.Width-100),
					header.Frame.Y + header.Frame.Height + 50,
					View.Bounds.Width - 100,
					DesignConstants.ButtonHeight),
			};
			nextButton.TouchUpInside += delegate {
				CreateAsync();
			};

			View.AddSubview (header);
			View.AddSubview (nextButton);
			// Perform any additional setup after loading the view, typically from a nib.
		}


		//Async
		public async void CreateAsync(){

			await DataLayer.CreateUser("","",NSUserDefaults.StandardUserDefaults.StringForKey("createAccountEmail"),NSUserDefaults.StandardUserDefaults.StringForKey("createAccountUsername"),NSUserDefaults.StandardUserDefaults.StringForKey("createAccountPassword"),NSUserDefaults.StandardUserDefaults.StringForKey("createAccountConfirmPassword"));
			NavigationController.PopToRootViewController(true);
		}
	}
}

