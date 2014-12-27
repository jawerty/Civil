using System;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace CivilPrototype
{
	public class RoundableUILabel : UILabel
	{
		public RoundableUILabel () : base()
		{
			ClipsToBounds = true;
		}
		public float CornerRadius{

			get{ return this.Layer.CornerRadius;}
			set{ this.Layer.CornerRadius = value;}

		}
		public CGColor BorderColor{

			get{ return this.Layer.BorderColor;}
			set{ this.Layer.BorderColor = value;}

		}
		public float BorderWidth{

			get{ return this.Layer.BorderWidth;}
			set{ this.Layer.BorderWidth = value;}

		}
	}
}

