using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using FlyoutNavigation;
using MonoTouch.Dialog;
using System.Drawing;
using System.Threading;

namespace CivilPrototype
{
	public partial class NavigableViewController : UIViewController
	{
		RoundableUIView navigationView;
		RectangleF navRect;
		CustomNavBar navBar;
		TappableUIView sliderMenuButton;
		UIView mainView;
		RectangleF sliderRect;
		FlyoutNavigationController navControl;
		public delegate void MovedTouches(MonoTouch.Foundation.NSSet touches, UIEvent evt);
		public event MovedTouches touchMoved = delegate {};
		public delegate void BeganTouches(MonoTouch.Foundation.NSSet touches, UIEvent evt);
		public event BeganTouches touchBegan = delegate {};
		public delegate void EndedTouches(MonoTouch.Foundation.NSSet touches, UIEvent evt);
		public event EndedTouches touchEnded = delegate {};
		public NavigableViewController (CustomNavBar navBar) : base ()
		{
			this.navBar = navBar;
		}
		public UIView MainView{
			get{ return mainView; }
			set{ mainView = value; }
		}
		private void SlowMethod ()
		{
			Thread.Sleep (150);
			InvokeOnMainThread (delegate {
				sliderMenuButton.Alpha = 1.0f;
			});
		}
		public void TapSlider(){

			sliderMenuButton.TapButton ();
		}
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			var touch = touches.AnyObject as UITouch;
			var something = touch.LocationInView (this.View);
			if(navRect.Contains(something)){
				if(new RectangleF(new PointF(sliderRect.X+navRect.X,sliderRect.Y+navRect.Y),sliderRect.Size).Contains(something)){
					TapSlider ();
				}
				else if(new RectangleF(new PointF(navBar.rightButton.Frame.X+navRect.X,navBar.rightButton.Frame.Y+navRect.Y),navBar.rightButton.Frame.Size).Contains(something)){
					navBar.rightButton.TapButton ();
				}
			}
			touchBegan (touches, evt);
		}
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);
			touchMoved (touches, evt);
		}
		public override void TouchesEnded(NSSet touches,UIEvent evt){
			base.TouchesEnded (touches, evt);
			touchEnded (touches, evt);

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			float screenWidth = this.View.Bounds.Width;
			navRect= new RectangleF(new PointF(0,0),new SizeF(screenWidth,60f));
			navigationView = new RoundableUIView {
				BackgroundColor = UIColor.White,
				Frame = navRect,
				CornerRadius = 5
			};
			navigationView.Add (new UIView (new RectangleF (0, navigationView.Frame.Height, screenWidth, .5f)){BackgroundColor = UIColor.Black});
			sliderRect = new RectangleF (new PointF (2, 20), new SizeF (30f, 30f));
			sliderMenuButton = new CSliderButton (sliderRect);
			sliderMenuButton.ButtonTapped += delegate {
				sliderMenuButton.Alpha = .5f;
				navControl.ToggleMenu ();
				ThreadPool.QueueUserWorkItem (o => SlowMethod ());
			};
			var headerView = new CHeader (navBar.header) {
				Frame = new RectangleF (DesignConstants.GetMiddleX(screenWidth,200), 10, 200, 50),
				Font = UIFont.FromName ("GeosansLight", 30),

			};
			navigationView.AddSubview (sliderMenuButton);
			navigationView.AddSubview (headerView);
			navigationView.AddSubview (navBar.rightButton);
			View.AddSubview (navigationView);
			View.AddSubview (mainView);
			View.BringSubviewToFront (mainView);
			View.BackgroundColor = DesignConstants.lgrey;
		}
		public FlyoutNavigationController Navigation{

			get{ return navControl;}
			set{ navControl = value;}

		}
	}
	public class CustomNavBar{
		public string header{get;set;}
		public TappableUIView rightButton{ get; set;}
	}
}
