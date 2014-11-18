// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace CivilPrototype
{
	[Register ("InfoViewController")]
	partial class InfoViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton DocumentsButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton JoinButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LocationLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TitleLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DocumentsButton != null) {
				DocumentsButton.Dispose ();
				DocumentsButton = null;
			}
			if (JoinButton != null) {
				JoinButton.Dispose ();
				JoinButton = null;
			}
			if (LocationLabel != null) {
				LocationLabel.Dispose ();
				LocationLabel = null;
			}
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
		}
	}
}
