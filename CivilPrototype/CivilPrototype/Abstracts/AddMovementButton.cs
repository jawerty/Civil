using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace CivilPrototype
{
	public class AddMovementButton : TappableUIView
	{
		public AddMovementButton (float BoundsWidth) : base()
		{
			BackgroundColor = UIColor.White;
			CornerRadius = 4;
			Frame = new RectangleF (BoundsWidth - 32f, 
				20f, 
				30f, 30f);
			Add (new UIImageView {
				Image = UIImage.FromFile ("plus.png"),
				BackgroundColor = UIColor.Clear,
				Frame = new RectangleF (7, 
					5, 
					20, 20)
			});
		}
	}
}

