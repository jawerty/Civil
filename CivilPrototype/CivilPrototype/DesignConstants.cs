using System;
using MonoTouch.UIKit;

namespace CivilPrototype
{
	public static class DesignConstants
	{
		public static UIColor teal = UIColor.FromRGB (12, 91, 108);
		public static UIColor peakcock = UIColor.FromRGB (7, 57, 62);
		public static UIColor surfer = UIColor.FromRGB (26, 149, 149);
		public static UIColor grey = UIColor.FromRGB (110, 115, 123);
		public static UIColor lgrey = UIColor.FromRGB (192, 198, 200);

		//Header Styles
		public static int HeaderFontSize = 25;
		public static string HeaderFontStyle = "Baskerville";
		public static float HeaderFrameX = 40.0f;
		public static float HeaderFrameY = 10.0f;
		public static float HeaderFrameWidth =  -80.0f; //Added to Bounds.Width
		public static float HeaderFrameHeight = 40.0f;
		public static UITextAlignment HeaderAlignment = UITextAlignment.Center;
		public static UIColor HeaderBackground = UIColor.Clear;

		//Text Field Styles
		public static UITextBorderStyle TextFieldBorderStyle = UITextBorderStyle.RoundedRect;
		public static float TextFieldMarginBottom = 35.0f;
		public static float TextFieldWidth = -20.0f; //Added to Bounds.Width
		public static float TextFieldHeight = 45.0f;
		public static float TextFieldFrameX = 10.0f;

		//Button Styles
		public static float ButtonWidth = -20.0f; //Added to Bounds.Width
		public static float ButtonHeight = 50.0f;
		public static float ButtonFrameX = 10.0f;
		public static UIButtonType ButtonType = UIButtonType.RoundedRect;
		public static float LargeButtonFontSize = 30.0f;
		public static float NormalButtonFontSize = 15.0f;
		public static string ButtonFontStyle = "Baskerville";
		public static UIControlState ButtonControlState = UIControlState.Normal;
	}
}

