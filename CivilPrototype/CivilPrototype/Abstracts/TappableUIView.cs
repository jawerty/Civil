using System;
using MonoTouch.UIKit;

namespace CivilPrototype
{
	public class TappableUIView : RoundableUIView
	{
		public delegate void Tapped();
		public event Tapped ButtonTapped = delegate {};
		public TappableUIView ()
		{
		}
		public void TapButton(){
			ButtonTapped ();

		}
	}
}

