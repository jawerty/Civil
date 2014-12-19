using System;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using System.Drawing;

namespace CivilPrototype
{
	public class RoundableUITextView : UITextView
	{
		public RoundableUITextView ()
		{
			Initialize ();
		}

		public RoundableUITextView (RectangleF frame)
			: base(frame)
		{
			Initialize ();
		}

		public RoundableUITextView (IntPtr handle)
			: base(handle)
		{
			Initialize ();
		}

		private void Initialize()
		{
			Placeholder = "Please enter text";

			ShouldBeginEditing = t => {
				if (Text == Placeholder) {
					Text = string.Empty;
				}
				return true;
			};
			ShouldEndEditing = t => {
				if (string.IsNullOrEmpty (Text)) {
					Text = Placeholder;
				}
				return true;
			};
		}

		/// <summary>
		/// Gets or sets the placeholder to show prior to editing - doesn't exist on UITextView by default
		/// </summary>
		public string Placeholder {
			get;
			set;
		}
		public float CornerRadius{

			get{ return this.Layer.CornerRadius;}
			set{ this.Layer.CornerRadius = value;}

		}
	}
}

