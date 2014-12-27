
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationController.NavigationBarHidden = false;
			NavigationController.NavigationBar.Translucent = false;
			NavigationController.NavigationBar.BarTintColor = UIColor.White;
			NavigationController.NavigationBar.BackItem.Title = "Back";
			NSDictionary attributes = NSDictionary.FromObjectAndKey(UIFont.FromName("GeosansLight",30),UIStringAttributeKey.Font);
			NavigationController.NavigationBar.TopItem.Title = "Sign up";
			NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes (attributes);
			var s = new UIBarButtonItem (new UIImageView{Image = UIImage.FromFile("leaf.png"),Frame = new RectangleF(0,0,20,25)});
			s.Width = 50;
			this.NavigationItem.SetRightBarButtonItem (s,true);
			CAGradientLayer gradient = new CAGradientLayer ();
			gradient.Frame = View.Bounds;
			gradient.Colors = new CGColor[]{UIColor.FromRGB(122,255,145).CGColor, UIColor.FromRGB(68,213,80).CGColor};
			View.Layer.InsertSublayer (gradient, 0);
			UITextView header, subHeader;
			UIView vLine;
			UITextField emailField;
			RoundableUILabel emailLabel;
			UIButton nextButton;
			header = new UITextView {

				Frame = new RectangleF (DesignConstants.GetMiddleX (View.Bounds.Width, View.Bounds.Width - 20), 25, View.Bounds.Width - 20, 40),
				Text = "Enter an email",
				Font = UIFont.FromName("GeosansLight",30),
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Center,
				TextColor = UIColor.White
			};
			subHeader = new UITextView{

				Frame = new RectangleF (DesignConstants.GetMiddleX (View.Bounds.Width, View.Bounds.Width - 20), header.Frame.Y + header.Frame.Height, View.Bounds.Width - 20, 30),
				Text = "We won't spam you!",
				Font = UIFont.FromName("GeosansLight",20),
				BackgroundColor = UIColor.Clear,
				TextAlignment = UITextAlignment.Center,
				TextColor = UIColor.White
			};
			emailField = new UITextField
			{
				BorderStyle = UITextBorderStyle.RoundedRect,
				Frame = new RectangleF(DesignConstants.GetMiddleX(View.Bounds.Width,300),subHeader.Frame.Y + subHeader.Frame.Height + 20,300,50),
				LeftViewMode = UITextFieldViewMode.Always,
				BackgroundColor = UIColor.White
			};
			emailLabel = new RoundableUILabel {
				BackgroundColor = UIColor.FromRGB (230, 230, 230),
				Frame = new RectangleF (0, 0, 70, 48),
				Text = "Email",
				CornerRadius = 5,
				Font = UIFont.FromName("GeosansLight",23),
				TextAlignment = UITextAlignment.Center,
			};
			emailField.LeftView = emailLabel;
			vLine = new UIView {
				Frame = new RectangleF (67, 1, 3, 48),
				BackgroundColor = UIColor.FromRGB (230, 230, 230),

			};
			nextButton = UIButton.FromType (DesignConstants.ButtonType);

			nextButton.SetTitleColor (DesignConstants.dgrey, UIControlState.Normal);

			UIImage grayButtonImage = UIImage.FromFile ("createAcc.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			nextButton.SetBackgroundImage (grayButtonImage, UIControlState.Normal);

			UIImage grayButtonImageDark = UIImage.FromFile ("createAccDark.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			nextButton.SetBackgroundImage (grayButtonImageDark, UIControlState.Highlighted);
			nextButton.Frame = new RectangleF (DesignConstants.GetMiddleX(View.Bounds.Width,View.Bounds.Width-100),
				subHeader.Frame.Y + subHeader.Frame.Height + emailField.Bounds.Height + 50,
				View.Bounds.Width - 100,
				DesignConstants.ButtonHeight);

			nextButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			nextButton.SetTitle ("Next", UIControlState.Normal);
			nextButton.TouchUpInside += delegate {
//				navigation.PushViewController (new CreateAccountController2(navigation), true);

			};
			emailField.AddSubview(vLine);

			View.AddSubview (header);
			View.AddSubview (subHeader);
			View.AddSubview(emailField);
			View.AddSubview (nextButton);
			// Perform any additional setup after loading the view, typically from a nib.
		}


		//Async
		public async void CreateAsync(string firstName,string lastName,string email,string username,string password,string passwordCheck){
			await DataLayer.CreateUser(firstName,lastName,email,username,password,passwordCheck);
			navigation.PopToRootViewController(true);
			navigation.ViewControllers[0].ViewDidLoad();
		}
	}
}

