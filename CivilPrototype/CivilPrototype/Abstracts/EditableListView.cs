using System;
using MonoTouch.UIKit;

namespace CivilPrototype
{
	public class EditableListView : UITableView
	{
		public EditableListView (string[] items) : base()
		{
			Source = new EditableTableViewSource (items);
		}
	}
}

