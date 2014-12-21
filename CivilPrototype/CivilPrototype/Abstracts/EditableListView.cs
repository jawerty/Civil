using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;

namespace CivilPrototype
{
	public class EditableListView : RoundableUIView
	{
		List<string> items;
		public delegate void SourceRowInsert();
		public event SourceRowInsert sourceRowInsert = delegate {};
		public delegate void SourceRowDelete();
		public event SourceRowDelete sourceRowDelete = delegate {};
		UITableView list;
		EditableTableViewSource tableSource;
		public EditableListView (List<string> items, RectangleF frame) : base()
		{
			this.items = items;
			this.Frame = frame;
			list = new UITableView ();
			CornerRadius = 5;
			var headerView = new UITextView {
				Text = "Skills",
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (0, 0, Bounds.Width, 44),
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
				BackgroundColor = UIColor.Clear,
			};
			tableSource = new EditableTableViewSource (items);
			tableSource.rowInsert += delegate {
				sourceRowInsert ();
			};
			tableSource.rowDelete += delegate {
				sourceRowDelete ();
			};
			list.Source = tableSource;
			list.Frame = new RectangleF (0, 44, Bounds.Width, 1000);
			list.SeparatorColor = UIColor.Clear;
			list.BackgroundColor = UIColor.Clear;
			BackgroundColor = UIColor.White;
			Add (headerView);
			Add (list);
		}
		public int Height{
			get{ return ((this.items.ToArray ().Length + 2) * 45); }
		}
		public void SetEditing(bool edit,bool eraseAdd){
			list.SetEditing (edit, true);
			if (eraseAdd) {
				if (edit)
					tableSource.WillBeginTableEditing (list);
				else
					tableSource.DidFinishTableEditing (list);
			}
		}
	}
}

