
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace CivilPrototype
{
	public class CreateMovementController : UIViewController
	{
		UINavigationController navigation;
		float skillsListHeight;
		public CreateMovementController(UINavigationController nav) : base ()
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
			this.View.BackgroundColor = DesignConstants.lgrey;
			UITextField titleField;
			UITextView descriptionField;
			UITextView titleView;
			float marginAdjustment = 7.0f;
			float heightAdjustment = 7.0f;
			// keep the code the username UITextField
			titleView = new UITextView {
				Text = "Create a Movement",
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.NormalButtonFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF(-DesignConstants.HeaderFrameX, 
					DesignConstants.HeaderFrameY, 
					View.Bounds.Height + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight),
			};
			titleField = new UITextField
			{
				Placeholder = "Title",
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, (2*(DesignConstants.TextFieldMarginBottom-marginAdjustment)), View.Bounds.Width + DesignConstants.TextFieldWidth, DesignConstants.TextFieldHeight-heightAdjustment),
				SecureTextEntry = true
			};
			descriptionField = new RoundableUITextView
			{
				CornerRadius = 10,
				Editable = true,
				Placeholder = "Description",
				UserInteractionEnabled = true,
				BackgroundColor = UIColor.White,
				Frame = new RectangleF(DesignConstants.TextFieldFrameX, 4*(DesignConstants.TextFieldMarginBottom-marginAdjustment), View.Bounds.Width + DesignConstants.TextFieldWidth, 200)
			};
			var submitButton = UIButton.FromType(DesignConstants.ButtonType);
			submitButton.Frame = new RectangleF(DesignConstants.ButtonFrameX, 375, View.Bounds.Width + DesignConstants.ButtonWidth, DesignConstants.ButtonHeight);
			submitButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.LargeButtonFontSize);
			submitButton.SetTitle("Create Movement!", UIControlState.Normal);
			submitButton.TouchUpInside += delegate
			{
				string title = titleField.Text;
				string description = descriptionField.Text;
				CreateAsync(title,description);
			};
			var discoverButton = UIButton.FromType(DesignConstants.ButtonType);
			discoverButton.Frame = new RectangleF(DesignConstants.ButtonFrameX, 435, View.Bounds.Width + DesignConstants.ButtonWidth, DesignConstants.ButtonHeight);
			discoverButton.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			discoverButton.SetTitle("Back", UIControlState.Normal);
			discoverButton.TouchUpInside += delegate
			{
				navigation.PopToRootViewController(true);
			};
			View.AddSubview(discoverButton);
			View.AddSubview(submitButton);
			View.AddSubview(titleView);
			View.AddSubview(titleField); 
			View.AddSubview(descriptionField);
			// Perform any additional setup after loading the view, typically from a nib.
		}


		//Async
		public async void CreateAsync(string title,string description){
			await DataLayer.CreateMovement(title,description);
			navigation.PopToRootViewController(true);
			navigation.ViewControllers[0].ViewDidLoad();
		}
	}
}

