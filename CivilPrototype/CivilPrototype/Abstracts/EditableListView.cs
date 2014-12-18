using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;

namespace CivilPrototype
{
	public class EditableListView : RoundableUIView
	{
		List<string> items;
		public EditableListView (List<string> items, RectangleF frame) : base()
		{
			this.items = items;
			this.Frame = frame;
			var list = new UITableView ();
			CornerRadius = 5;
			var headerView = new UITextView {
				Text = "Skills",
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (0, 0, Bounds.Width, 44),
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
				BackgroundColor = UIColor.Clear,
			};
			list.Source = new EditableTableViewSource (items);
			list.Frame = new RectangleF (0, 44, Bounds.Width, Bounds.Height);
			list.SeparatorColor = UIColor.Clear;
			list.BackgroundColor = UIColor.Clear;
			list.SetEditing (true, true);
			BackgroundColor = UIColor.White;
			Add (headerView);
			Add (list);
		}
		public int Height{
			get{ return ((this.items.ToArray ().Length + 1) * 44); }
		}
	}
}

