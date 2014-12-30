﻿
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
	public class CreateAccountController : UIViewController
	{
		UINavigationController navigation;
		float skillsListHeight;
		public CreateAccountController (UINavigationController nav) : base ()
		{
			navigation = nav;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.Title = "Sign Up";
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationController.NavigationBarHidden = false;
//			NavigationController.NavigationBar.Translucent = false;
//			NavigationController.NavigationBar.BarTintColor = UIColor.White;
			NavigationItem.Title = "Sign Up";
			var j = NavigationController.NavigationBar.BackIndicatorTransitionMaskImage;
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem ("\U000025C0\U0000FE0E Back", UIBarButtonItemStyle.Done, delegate {NavigationController.PopViewControllerAnimated(true);
			});
			NSDictionary attributes = NSDictionary.FromObjectAndKey(UIFont.FromName("GeosansLight",30),UIStringAttributeKey.Font);
			NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes (attributes);
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
			UITextView header, subHeader;
			UIView vLine;
			UITextField emailField;
			RoundableUILabel emailLabel;
			UIButton nextButton;
			header = new CHeader("Enter an email") {

				Frame = new RectangleF (DesignConstants.GetMiddleX (View.Bounds.Width, View.Bounds.Width - 20), 25, View.Bounds.Width - 20, 160),
			};
			subHeader = new CSubHeader("We promise no spam!"){
				Frame = new RectangleF (DesignConstants.GetMiddleX (View.Bounds.Width, View.Bounds.Width - 20), header.Frame.Y + header.Frame.Height, View.Bounds.Width - 20, 30),
			};
			emailField = new CTextFieldWLabel(new RectangleF(DesignConstants.GetMiddleX(View.Bounds.Width,300),subHeader.Frame.Y + subHeader.Frame.Height + 20,300,50), "Email")
			{
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(DesignConstants.GetMiddleX(View.Bounds.Width,300),subHeader.Frame.Y + subHeader.Frame.Height + 20,300,50),
				LeftViewMode = UITextFieldViewMode.Always,
				BackgroundColor = UIColor.White
			};
			nextButton = new CGreenButton ("Next"){
				Frame = new RectangleF (DesignConstants.GetMiddleX(View.Bounds.Width,View.Bounds.Width-100),
					subHeader.Frame.Y + subHeader.Frame.Height + emailField.Bounds.Height + 50,
					View.Bounds.Width - 100,
					DesignConstants.ButtonHeight),
			};
			nextButton.TouchUpInside += delegate {
				navigation.PushViewController (new CreateAccountControllerUsername(), true);
				NSUserDefaults.StandardUserDefaults.SetBool(true,"creatingAccount");
				NSUserDefaults.StandardUserDefaults.SetString(emailField.Text,"createAccountEmail");
			};

			View.AddSubview (header);
			View.AddSubview (subHeader);
			View.AddSubview(emailField);
			View.AddSubview (nextButton);
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

