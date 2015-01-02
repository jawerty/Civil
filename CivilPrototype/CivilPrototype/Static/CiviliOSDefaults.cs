using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace CivilPrototype
{
	public class CHeader : UITextView
	{
		public CHeader (string text)
		{
			Text = text;
			Editable = false;
			TextColor = DesignConstants.dgrey;
			BackgroundColor = DesignConstants.HeaderBackground;
			Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize);
			TextAlignment = DesignConstants.HeaderAlignment;
		}
	}
	public class CHR : UIView
	{
		public CHR(){
			BackgroundColor = DesignConstants.grey;
		}
	}
	public class CSliderButton : TappableUIView{

		public CSliderButton(RectangleF frame){
			Frame = frame;
			CornerRadius = 4;
			Alpha = 1.0f;
			var miniViews = new RoundableUIView{new RoundableUIView{
					Frame = new RectangleF(new PointF(5f,7.5f),new SizeF(20f,3f)),
					BackgroundColor = UIColor.FromRGB(0,255,0),
					CornerRadius = 2
				},	
				new RoundableUIView{
					Frame = new RectangleF(new PointF(5f,13.5f),new SizeF(20f,3f)),
					BackgroundColor = UIColor.FromRGB(0,255,0),
					CornerRadius = 2
				},
				new RoundableUIView{
					Frame = new RectangleF(new PointF(5f,20f),new SizeF(20f,3f)),
					BackgroundColor = UIColor.FromRGB(0,255,0),
					CornerRadius = 2

				}
			};
			AddSubviews (miniViews);
		}

	}
	public class CSubHeader : UITextView
	{
		public CSubHeader (string text)
		{
			Text = text;
			Editable = false;
			Font = UIFont.FromName ("GeosansLight", 20);
			BackgroundColor = UIColor.Clear;
			TextAlignment = UITextAlignment.Center;
			TextColor = DesignConstants.dgrey;
		}
	}
	public class CButton {
		UIButton button;
		public CButton(string title)
		{
			button = UIButton.FromType (UIButtonType.System);
			button.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			button.SetTitle (title, UIControlState.Normal);
		}
		public UIButton Button{

			get{ return button; }
			set{ button = value; }
		}
	}
	public class CGreenButton : UIButton
	{
		public CGreenButton (string title)
		{
			var loginImage = UIImage.FromFile ("login.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			var loginImageDark = UIImage.FromFile ("loginDark.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			SetTitleColor (UIColor.White, UIControlState.Normal);
			SetBackgroundImage (loginImage, UIControlState.Normal);
			SetBackgroundImage (loginImageDark, UIControlState.Highlighted);
			Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.LargeButtonFontSize);
			SetTitle (title, UIControlState.Normal);
		}
	}
	public class CWhiteButton : UIButton
	{
		public CWhiteButton (string title)
		{
			var createImage = UIImage.FromFile ("createAcc.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			var createImageDark = UIImage.FromFile ("createAccDark.png").CreateResizableImage (new UIEdgeInsets (18, 18, 18, 18));
			SetTitleColor (DesignConstants.dgrey, UIControlState.Normal);
			SetBackgroundImage (createImage, UIControlState.Normal);
			SetBackgroundImage (createImageDark, UIControlState.Highlighted);
			Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			SetTitle (title, UIControlState.Normal);
		}
	}
	public class CTextFieldWLabel : UITextField
	{
		public CTextFieldWLabel (RectangleF frame, string labelText)
		{
			BorderStyle = UITextBorderStyle.RoundedRect;
			Layer.BorderColor = UIColor.FromRGB (200, 200, 200).CGColor;
			Layer.BorderWidth = 1;
			Layer.CornerRadius = 5;
			LeftViewMode = UITextFieldViewMode.Always;
			BackgroundColor = UIColor.White;
			Frame = frame;
			var label = new RoundableUILabel {
				BackgroundColor = UIColor.FromRGB (230, 230, 230),
				Frame = new RectangleF (0, 0, Bounds.Width*.3f, Bounds.Height-2),
				Text = labelText,
				CornerRadius = 5,
				Font = UIFont.FromName("GeosansLight",23),
				TextAlignment = UITextAlignment.Center,
			};
			LeftView = label;
			var vLine = new UIView {
				Frame = new RectangleF ((Bounds.Width*.3f) - 3, 1, 3, Bounds.Height - 2),
				BackgroundColor = UIColor.FromRGB (230, 230, 230),

			};
			AddSubview(vLine);
		}
	}
	public class CTextField : UITextField
	{
		public CTextField(string place, bool secure){
			Placeholder = place;
			BorderStyle = DesignConstants.TextFieldBorderStyle;
			Font = UIFont.FromName (DesignConstants.TextFieldFontStyle, DesignConstants.TextFieldFontSize);
			SecureTextEntry = secure;

		}

	}
	public class CTextFieldBundle : RoundableUIView
	{
		public CTextFieldBundle (RectangleF frame, string p1, bool secure1, string p2, bool secure2, RectangleF parentBounds)
		{
			Frame = frame;
			CornerRadius = 5;
			BorderColor = UIColor.FromRGB(200,200,200).CGColor;
			BorderWidth = 1.0f;
			BackgroundColor = UIColor.White;
			var field1 = new CTextField ("Username", false) {
				Frame = new RectangleF ((Bounds.Width / 2) - ((Bounds.Width - (.0625f * parentBounds.Width)) / 2),
										0,
										Bounds.Width - (.0625f * parentBounds.Width),
										DesignConstants.TextFieldHeight)
			};
			var field2 = new CTextField ("Password", true) {
				Frame = new RectangleF ((Bounds.Width / 2) - ((Bounds.Width - (.0625f * parentBounds.Width)) / 2),
					DesignConstants.TextFieldHeight,
					Bounds.Width - (.0625f * parentBounds.Width),
					DesignConstants.TextFieldHeight)
			};
			var contentHR = new CHR {
				Frame = new RectangleF    ((Bounds.Width / 2) - ((Bounds.Width - (.0625f*Bounds.Width)) / 2),
											DesignConstants.TextFieldHeight + (.00416f*parentBounds.Height),
											Bounds.Width - (.0625f*Bounds.Width),
											1),
				};
			Add (field1);
			Add (contentHR);
			Add (field2);
		}
	}

}

