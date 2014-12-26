﻿using System;
using MonoTouch.UIKit;

namespace CivilPrototype
{
	public static class DesignConstants
	{
		public static UIColor teal = UIColor.FromRGB (77, 166, 235);
		public static UIColor peakcock = UIColor.FromRGB (7, 57, 62);
		public static UIColor surfer = UIColor.FromRGB (26, 149, 149);
		public static UIColor grey = UIColor.FromRGB (230, 230, 230);
		public static UIColor dgrey = UIColor.FromRGB(75,75,75);
		public static UIColor lgrey = UIColor.FromRGB(241,241,241);
		//Header Styles
		public static int HeaderLargeFontSize = 50;
		public static int HeaderSmallFontSize = 15;
		public static string HeaderFontStyle = "GeosansLight";
		public static float HeaderFrameX = 40.0f;
		public static float HeaderFrameY = 0.0f;
		public static float HeaderFrameWidth =  -80.0f; //Added to Bounds.Width
		public static float HeaderFrameHeight = 55.0f;
		public static UITextAlignment HeaderAlignment = UITextAlignment.Center;
		public static UIColor HeaderBackground = UIColor.Clear;

		//Text Field Styles
		public static UITextBorderStyle TextFieldBorderStyle = UITextBorderStyle.None;
		public static string TextFieldFontStyle = "HelveticaNeue";
		public static int TextFieldFontSize = 15;
		public static float TextFieldMarginBottom = 35.0f;
		public static float TextFieldWidth = -20.0f; //Added to Bounds.Width
		public static float TextFieldHeight = 45.0f;
		public static float TextFieldFrameX = 10.0f;

		//Button Styles
		public static float ButtonWidth = -20.0f; //Added to Bounds.Width
		public static float ButtonHeight = 40.0f;
		public static float ButtonFrameX = 10.0f;
		public static UIButtonType ButtonType = UIButtonType.Custom;
		public static float LargeButtonFontSize = 23.0f;
		public static float NormalButtonFontSize = 18.0f;
		public static string ButtonFontStyle = "GeosansLight";

		public static float GetMiddleX(float parentWidth, float thisWidth){

			return (parentWidth / 2) - (thisWidth / 2);

		}
	}
}

