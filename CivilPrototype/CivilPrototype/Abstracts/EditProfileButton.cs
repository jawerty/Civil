using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace CivilPrototype
{
	public class EditProfileButton : RoundableUIView
	{
		public delegate void Tapped(MonoTouch.Foundation.NSSet touches, UIEvent evt);
		public event Tapped ButtonTapped = delegate {};
		public EditProfileButton (float BoundsWidth) : base()
		{
			BackgroundColor = UIColor.White;
			CornerRadius = 4;
			Frame = new RectangleF (BoundsWidth - 32f, 
				20f, 
				30f, 30f);
			Add (new UIImageView {
				Image = UIImage.FromFile ("edit.png"),
				BackgroundColor = UIColor.Clear,
				Frame = new RectangleF (7, 
					5, 
					20, 20)
			});
		}
		public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			ButtonTapped (touches,evt);
		}

	}
}

