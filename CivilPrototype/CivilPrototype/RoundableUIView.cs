using System;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;

namespace CivilPrototype
{
	public class RoundableUIView : UIView
	{
		public RoundableUIView () : base()
		{

		}
		public float CornerRadius{

			get{ return this.Layer.CornerRadius;}
			set{ this.Layer.CornerRadius = value;}

		}
	}
}

