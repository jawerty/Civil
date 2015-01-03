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
		int currentMovementIndex = -1;
		int numMovementsReturned;
		bool fullMovementReturn;
		int idealMovementsReturn = 25;
		int numQueuedMovements = 5;
		int movementRectsCount;
		bool reachedEndOfMovements = false;
		List<MovementID> movementIds;
		EditProfileButton postButton;
		UIViewController rootControl;
		private async void Initialize(){
			movementIds = await DataLayer.GetMovements ("pop",0);
			numMovementsReturned = movementIds.Count;
			fullMovementReturn = (numMovementsReturned == idealMovementsReturn);
			List<Movement> movements = new List<Movement>{ };

			movementRects = new List<UIView>{};
			if (numMovementsReturned >= numQueuedMovements) {

				for (int i = 0; i < numQueuedMovements; i++) {
					currentMovementIndex++;
 					var movement = await DataLayer.GetMovement (movementIds [i].id);
					movements.Add (movement);
				}
				for (int i = 0; i < movements.Count; i++) {
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
				}
			} else {
				for (int i = 0; i < numMovementsReturned; i++) {
					currentMovementIndex++;
					var movement = await DataLayer.GetMovement (movementIds [i].id);
					movements.Add (movement);
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
			var checkImg = new UIImageView (new RectangleF (originMovement.X + 5, originMovement.Y + sizeMovement.Height+ 10, 50, 50)){

				Image = UIImage.FromFile("checkImg.png"),
			};
			var exImg = new UIImageView (new RectangleF (checkImg.Frame.X +sizeMovement.Width - 50, checkImg.Frame.Y, 50, 50)){

				Image = UIImage.FromFile("exImg.png"),
			};
			Add (checkImg);
			Add (exImg);
		}
		public async void storeNextMovement(int h){
			var i = currentMovementIndex;
			if (i >= movementIds.Count) {
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
				movementRects [h] = view;
			} else {
				var s = await DataLayer.GetMovement (movementIds [i].id);
				var view = new RoundableUIView {
					CornerRadius = 7,
					BackgroundColor = UIColor.White,
					Frame = new RectangleF (originMovement, sizeMovement)
				};
				view.Add (new UITextView {
					Text = s.title,
					Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 0, sizeMovement.Width * .8f, 40),
					TextAlignment = UITextAlignment.Center
				});
				view.Add (new UITextView {
					Text = s.description,
					Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 45, sizeMovement.Width * .8f, 40),
					TextAlignment = UITextAlignment.Center
				});
				view.Add (new UITextView {
					Text = s.founder,
					Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 90, sizeMovement.Width * .8f, 40),
					TextAlignment = UITextAlignment.Center
				});
				view.Add (new UITextView {
					Text = s.dateCreated.ToString (),
					Frame = new RectangleF (sizeMovement.Width - (sizeMovement.Width * .9f), 150, sizeMovement.Width * .8f, 40),
					TextAlignment = UITextAlignment.Center
				});
				movementRects [h] = view;
			}
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
							currentMovementIndex++;
							storeNextMovement(i);
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

