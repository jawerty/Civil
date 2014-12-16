using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace CivilPrototype
{
	public class DiscoverView : UIView
	{
		UIView movementRect;
		PointF originMovement;
		SizeF sizeMovement;
		float rotationAngle;
		public DiscoverView (RectangleF frame) : base()
		{
			Frame = frame;
			rotationAngle = 0;
			sizeMovement = new SizeF (250, 225);
			originMovement = new PointF ((Bounds.Width / 2) - (sizeMovement.Width / 2), (Bounds.Height / 2) - (sizeMovement.Height / 2));
			var header = new UITextView {
				Text = "Discover",
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (DesignConstants.HeaderFrameX, 
					DesignConstants.HeaderFrameY, 
					Bounds.Width + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight)
			};
			movementRect = new RoundableUIView {
				CornerRadius = 7,
				BackgroundColor = UIColor.White,
				Frame = new RectangleF (originMovement,sizeMovement)


			};
			movementRect.Add (new UITextView {
				Text = "Movement",
				Frame = new RectangleF(sizeMovement.Width-(sizeMovement.Width*.9f),0,sizeMovement.Width*.8f,40),
				TextAlignment = UITextAlignment.Center
			});
			Add (header);
			Add (movementRect);
		}
		public void MoveMovement(float diff){
			if(diff == 10000){
				originMovement = new PointF ((Bounds.Width / 2) - (sizeMovement.Width / 2), (Bounds.Height / 2) - (sizeMovement.Height / 2));
				rotationAngle = 0;
				UIView.Animate (.1,
					delegate {
						movementRect.Frame = new RectangleF (originMovement,sizeMovement);
						movementRect.Transform = CGAffineTransform.MakeRotation(rotationAngle);
					},
					delegate {
						//Console.WriteLine ("Animation completed");
					}
				);
			}
			else{
				originMovement.X = originMovement.X + diff;
				if (originMovement.X + (sizeMovement.Width/2) > Bounds.Width / 2) {
					if (diff < 0) {
						originMovement.Y = originMovement.Y + Math.Abs((diff / 4));
					} else {
						originMovement.Y = originMovement.Y - Math.Abs((diff / 4));
					}
				} else if (originMovement.X + (sizeMovement.Width/2) == Bounds.Width / 2) {


				} else {
					if (diff < 0) {
						originMovement.Y = originMovement.Y - Math.Abs((diff / 4));
					} else {
						originMovement.Y = originMovement.Y + Math.Abs((diff / 4));
					}
				}
				if (diff > 0) 
					rotationAngle = rotationAngle + -1*.001f;
				
				else
					rotationAngle = rotationAngle + .001f;
				UIView.Animate (.1,
					delegate {
						movementRect.Frame = new RectangleF (originMovement,sizeMovement);
						movementRect.Transform = CGAffineTransform.MakeRotation(rotationAngle);
					},
					delegate {
						//Console.WriteLine ("Animation completed");
					}
				);
			}

		}

	}
}

