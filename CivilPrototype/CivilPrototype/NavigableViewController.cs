using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using FlyoutNavigation;
using MonoTouch.Dialog;
using System.Drawing;

namespace CivilPrototype
{
	public partial class NavigableViewController : UIViewController
	{
		RoundableUIView sliderMenuButton;
		RoundableUIView navigationView;
		RectangleF navRect;
		RectangleF sliderRect;
		FlyoutNavigationController navControl;
		public NavigableViewController () : base ()
		{

		}
		public RoundableUIView SliderMenuButton{

			get{ return sliderMenuButton; }
			set{ sliderMenuButton = value; }

		}
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			var touch = touches.AnyObject as UITouch;
			var something = touch.LocationInView (this.View);
			if(navRect.Contains(something)){
				if(new RectangleF(new PointF(sliderRect.X+navRect.X,sliderRect.Y+navRect.Y),sliderRect.Size).Contains(something)){
					sliderMenuButton.Alpha = .5f;
					navControl.ToggleMenu ();
				}
			}
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			float screenWidth = this.View.Bounds.Width;
			navRect= new RectangleF(new PointF(0,20),new SizeF(screenWidth,30f));
			sliderRect = new RectangleF (new PointF (2, 0), new SizeF (30f, 30f));
			navigationView = new RoundableUIView {
				BackgroundColor = UIColor.Gray,
				Frame = navRect,
				CornerRadius = 2
			};
			sliderMenuButton = new RoundableUIView
			{
				BackgroundColor = UIColor.Black,
				Frame = sliderRect,
				CornerRadius = 4,
				Alpha = 1.0f
			};
			var miniViews = new RoundableUIView{new RoundableUIView{
					Frame = new RectangleF(new PointF(5f,7.5f),new SizeF(20f,3f)),
					BackgroundColor = UIColor.Purple,
					CornerRadius = 2
				},	
				new RoundableUIView{
					Frame = new RectangleF(new PointF(5f,13.5f),new SizeF(20f,3f)),
					BackgroundColor = UIColor.Purple,
					CornerRadius = 2
				},
				new RoundableUIView{
					Frame = new RectangleF(new PointF(5f,20f),new SizeF(20f,3f)),
					BackgroundColor = UIColor.Purple,
					CornerRadius = 2

				}
			};
			sliderMenuButton.AddSubviews (miniViews);
			navigationView.AddSubview (sliderMenuButton);
			View.AddSubview (navigationView);
		}
		public FlyoutNavigationController Navigation{

			get{ return navControl;}
			set{ navControl = value;}

		}
	}
}
