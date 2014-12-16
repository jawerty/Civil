using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace CivilPrototype
{
	public class DiscoverView : UIView
	{
		UIView[] movementRects = new UIView[5];
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
			for (int i = 0; i < 5; i++) {
				movementRects [i] = new RoundableUIView {
						CornerRadius = 7,
						BackgroundColor = UIColor.White,
						Frame = new RectangleF (originMovement, sizeMovement)
				};
				movementRects[i].Add (new UITextView {
					Text = "Movement",
					Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 0, sizeMovement.Width * .8f, 40),
					TextAlignment = UITextAlignment.Center
				});
			}
			Add (header);
			Add (movementRects[0]);
		}
		public void MoveMovement(float diff){
			if(diff == 10000){//if touches ended
				var middleX = originMovement.X + (sizeMovement.Width / 2);
				var widthdiveight = Bounds.Width / 16;
				var widthseveneight = Bounds.Width * 15 / 16;
				if ((middleX > widthdiveight) && (middleX < widthseveneight)) {
					originMovement = new PointF ((Bounds.Width / 2) - (sizeMovement.Width / 2), (Bounds.Height / 2) - (sizeMovement.Height / 2));
					rotationAngle = 0;
					UIView.Animate (.1,
						delegate {
							movementRects[0].Frame = new RectangleF (originMovement, sizeMovement);
							movementRects[0].Transform = CGAffineTransform.MakeRotation (rotationAngle);
						},
						delegate {
							//Console.WriteLine ("Animation completed");
						}
					);
				} else {
					if (middleX < widthdiveight) {
						AnimateNextMovement (true);
					} else if (middleX > widthseveneight) {
						AnimateNextMovement (false);
					}
				}
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
						movementRects[0].Frame = new RectangleF (originMovement,sizeMovement);
						movementRects[0].Transform = CGAffineTransform.MakeRotation(rotationAngle);
					},
					delegate {
						//Console.WriteLine ("Animation completed");
					}
				);
			}

		}
		public void AnimateNextMovement(bool left){
			if (left) {
				originMovement = new PointF (-400, 0);
				movementRects [1].Frame = new RectangleF (Bounds.Width + 400, 0, sizeMovement.Width, sizeMovement.Height);
			} else {
				originMovement = new PointF (Bounds.Width + 400, 0);
				movementRects [1].Frame = new RectangleF (-400, 0, sizeMovement.Width, sizeMovement.Height);
			}
			UIView.Animate(1,
				delegate {
					movementRects[0].Frame = new RectangleF (originMovement, sizeMovement);
					movementRects[0].Transform = CGAffineTransform.MakeRotation (rotationAngle);
				},
				delegate {
					movementRects[0].RemoveFromSuperview();
					for(int i =0;i<5;i++){
						if(i==4){
							movementRects[i] = new RoundableUIView {
								CornerRadius = 7,
								BackgroundColor = UIColor.White,
								Frame = new RectangleF (originMovement, sizeMovement)
							};
							movementRects[i].Add (new UITextView {
								Text = "Movement",
								Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 0, sizeMovement.Width * .8f, 40),
								TextAlignment = UITextAlignment.Center
							}); 
						}
						else{
							movementRects[i] = movementRects[i+1];
						}

					}
				}
			);
			Add (movementRects [1]);
			rotationAngle = rotationAngle + .005f;
			originMovement = new PointF ((Bounds.Width / 2) - (sizeMovement.Width / 2), (Bounds.Height / 2) - (sizeMovement.Height / 2));
			rotationAngle = 0;
			UIView.AnimateAsync(1,
				delegate {
					movementRects[1].Frame = new RectangleF (originMovement, sizeMovement);
					movementRects[1].Transform = CGAffineTransform.MakeRotation (rotationAngle);
				}
			);

		}

	}
}

