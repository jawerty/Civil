using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using System.Threading;
using System.Collections.Generic;

namespace CivilPrototype
{
	public class DiscoverView : UIView
	{
		List<UIView> movementRects;
		PointF originMovement;
		SizeF sizeMovement;
		float rotationAngle;
		int currentMovementIndex;
		int numMovementsReturned;
		bool fullMovementReturn;
		int idealMovementsReturn = 25;
		int numQueuedMovements = 5;
		int movementRectsCount;
		bool reachedEndOfMovements = false; 
		EditProfileButton postButton;
		UIViewController rootControl;
		private void SlowMethod ()
		{
			Thread.Sleep (300);
			InvokeOnMainThread (delegate {
				postButton.BackgroundColor = UIColor.White;
				rootControl.NavigationController.PushViewController(new CreateMovementController(rootControl.NavigationController),true);
			});
		}
		private async void Initialize(){
			var movementIds = await DataLayer.GetMovements ("pop",0);
			numMovementsReturned = movementIds.Count;
			fullMovementReturn = (numMovementsReturned == idealMovementsReturn);
			List<Movement> movements = new List<Movement>{ };

			movementRects = new List<UIView>{};
			if (numMovementsReturned >= numQueuedMovements) {

				for (int i = 0; i < numQueuedMovements; i++) {
					var movement = await DataLayer.GetMovement (movementIds [i].id);
					movements.Add (movement);
				}
			} else {
				for (int i = 0; i < numMovementsReturned; i++) {
					var movement = await DataLayer.GetMovement (movementIds [i].id);
					movements.Add (movement);
				}
			}

			for (int i = 0; i < movements.Count + 1; i++) {
				if (i < movements.Count) {
					var view = new RoundableUIView {
						CornerRadius = 7,
						BackgroundColor = UIColor.White,
						Frame = new RectangleF (originMovement, sizeMovement)
					};
					view.Add (new UITextView {
						Text = movements [i].title,
						Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 0, sizeMovement.Width * .8f, 40),
						TextAlignment = UITextAlignment.Center
					});
					view.Add (new UITextView {
						Text = movements [i].description,
						Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 45, sizeMovement.Width * .8f, 40),
						TextAlignment = UITextAlignment.Center
					});
					view.Add (new UITextView {
						Text = movements [i].founder,
						Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 90, sizeMovement.Width * .8f, 40),
						TextAlignment = UITextAlignment.Center
					});
					view.Add (new UITextView {
						Text = movements [i].dateCreated.ToString (),
						Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 150, sizeMovement.Width * .8f, 40),
						TextAlignment = UITextAlignment.Center
					});
					movementRects.Add (view);
				} else {

					var view = new RoundableUIView {
						CornerRadius = 7,
						BackgroundColor = UIColor.White,
						Frame = new RectangleF (originMovement, sizeMovement)
					};
					view.Add (new UITextView {
						Text = "No Movements Available",
						Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 0, sizeMovement.Width * .8f, 40),
						TextAlignment = UITextAlignment.Center
					});
					reachedEndOfMovements = true;
					movementRects.Add (view);
				}
			}
			movementRectsCount = movementRects.Count;
			Add (movementRects[0]);
		}
		public DiscoverView (RectangleF frame,UIViewController rootControl) : base()
		{
			Frame = frame;
			Initialize ();
			this.rootControl = rootControl;
			rotationAngle = 0;
			sizeMovement = new SizeF (250, 225);
			originMovement = new PointF ((Bounds.Width / 2) - (sizeMovement.Width / 2), (Bounds.Height / 2) - (sizeMovement.Height / 2));
			postButton = new EditProfileButton (Bounds.Width);
			postButton.ButtonTapped += (touches, evt) => {
				postButton.BackgroundColor = DesignConstants.lgrey;
				ThreadPool.QueueUserWorkItem (o => SlowMethod ());
			};
			var header = new UITextView {
				Text = "Discover",
				BackgroundColor = DesignConstants.HeaderBackground,
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize),
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (DesignConstants.HeaderFrameX, 
					DesignConstants.HeaderFrameY, 
					Bounds.Width + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight)
			};
			Add (header);
			Add (postButton);
		}
		public void MoveMovement(float diff){
			if ((movementRects[0].Subviews.GetLength(0)) == 1) {
			} else {
				if (diff == 10000) {//if touches ended
					var middleX = originMovement.X + (sizeMovement.Width / 2);
					var widthdiveight = Bounds.Width / 16;
					var widthseveneight = Bounds.Width * 15 / 16;
					if ((middleX > widthdiveight) && (middleX < widthseveneight)) {
						originMovement = new PointF ((Bounds.Width / 2) - (sizeMovement.Width / 2), (Bounds.Height / 2) - (sizeMovement.Height / 2));
						rotationAngle = 0;
						UIView.Animate (.1,
							delegate {
								movementRects [0].Frame = new RectangleF (originMovement, sizeMovement);
								movementRects [0].Transform = CGAffineTransform.MakeRotation (rotationAngle);
							},
							delegate {
							}
						);
					} else {
						if (middleX < widthdiveight) {
							AnimateNextMovement (true);
						} else if (middleX > widthseveneight) {
							AnimateNextMovement (false);
						}
					}
				} else {
					originMovement.X = originMovement.X + diff;
					if (originMovement.X + (sizeMovement.Width / 2) > Bounds.Width / 2) {
						if (diff < 0) {
							originMovement.Y = originMovement.Y + Math.Abs ((diff / 4));
						} else {
							originMovement.Y = originMovement.Y - Math.Abs ((diff / 4));
						}
					} else if (originMovement.X + (sizeMovement.Width / 2) == Bounds.Width / 2) {


					} else {
						if (diff < 0) {
							originMovement.Y = originMovement.Y - Math.Abs ((diff / 4));
						} else {
							originMovement.Y = originMovement.Y + Math.Abs ((diff / 4));
						}
					}
					if (diff > 0)
						rotationAngle = rotationAngle + -1 * .001f;
					else
						rotationAngle = rotationAngle + .001f;
					UIView.Animate (.1,
						delegate {
							movementRects [0].Frame = new RectangleF (originMovement, sizeMovement);
							movementRects [0].Transform = CGAffineTransform.MakeRotation (rotationAngle);
						},
						delegate {
							//Console.WriteLine ("Animation completed");
						}
					);
				}
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
					for(int i =0; i< movementRectsCount;i++){
						if(i==(movementRectsCount-1)){
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

