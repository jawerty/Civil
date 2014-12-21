using System;
using System.Collections.Generic;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CivilPrototype {
	public class EditableTableViewSource : UITableViewSource {
		List<string> tableItems;
		string cellIdentifier = "TableCell";
		public delegate void RowInsert();
		public event RowInsert rowInsert = delegate {};
		public delegate void RowDelete();
		public event RowDelete rowDelete = delegate {};
		public EditableTableViewSource (List<string> items)
		{
			tableItems = items;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection (UITableView tableview, int section)
		{
			return tableItems.Count;
		}

		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			new UIAlertView("Row Selected"
				, tableItems[indexPath.Row], null, "OK", null).Show();
			tableView.DeselectRow (indexPath, true);
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

			// UNCOMMENT one of these to use that style
			//			var cellStyle = UITableViewCellStyle.Default;
			var cellStyle = UITableViewCellStyle.Default;
			//			var cellStyle = UITableViewCellStyle.Value1;
			//			var cellStyle = UITableViewCellStyle.Value2;

			// if there are no cells to reuse, create a new one
			if (cell == null) {
				cell = new UITableViewCell (cellStyle, cellIdentifier);
			}

			cell.TextLabel.Text = tableItems[indexPath.Row];
			return cell;
		}

		#region -= editing methods =-

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				// remove the item from the underlying data source
				tableItems.RemoveAt (indexPath.Row);
				// delete the row from the table
				tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				rowDelete ();
				break;

			case UITableViewCellEditingStyle.Insert:
				//---- create a new item and add it to our underlying data
				var av = new UIAlertView {
					Title = "Skills",
					Message = "Enter a Skill",
					AlertViewStyle = UIAlertViewStyle.PlainTextInput,

				};
				av.AddButton ("Okay");
				av.AddButton ("Cancel");
				av.DismissWithClickedButtonIndex (1, true);
				av.Clicked += delegate(object a, UIButtonEventArgs b) {
					if (b.ButtonIndex == 0) {
						tableItems.Insert (indexPath.Row, av.GetTextField (0).Text);
						rowInsert();
						//---- insert a new row in the table
						tableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
					}
					Console.WriteLine ("Button " + b.ButtonIndex.ToString () + " clicked"); 
				};
				av.Show ();
				break;

			case UITableViewCellEditingStyle.None:
				Console.WriteLine ("CommitEditingStyle:None called");
				break;
			}
		}

		/// <summary>
		/// Called by the table view to determine whether or not the row is editable
		/// </summary>
		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}

		/// <summary>
		/// Called by the table view to determine whether or not the row is moveable
		/// </summary>
		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return false;
		}

		/// <summary>
		/// Custom text for delete button
		/// </summary>
		public override string TitleForDeleteConfirmation (UITableView tableView, NSIndexPath indexPath)
		{
			return "Trash (" + tableItems[indexPath.Row] + ")";
		}

		/// <summary>
		/// Called by the table view to determine whether the editing control should be an insert
		/// or a delete.
		/// </summary>
		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.Editing) {
				if (indexPath.Row == tableView.NumberOfRowsInSection (0)-1)
					return UITableViewCellEditingStyle.Insert;
				else
					return UITableViewCellEditingStyle.Delete;
			} else  // not in editing mode, enable swipe-to-delete for all rows
				return UITableViewCellEditingStyle.Delete;
		}
		public override NSIndexPath CustomizeMoveTarget (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath proposedIndexPath)
		{
			var numRows = tableView.NumberOfRowsInSection (0) - 1; // less the (add new) one
			Console.WriteLine (proposedIndexPath.Row + " " + numRows);
			if (proposedIndexPath.Row >= numRows)
				return NSIndexPath.FromRowSection(numRows-1, 0);
			else
				return proposedIndexPath;
		}
		/// <summary>
		/// called by the table view when a row is moved.
		/// </summary>
		/// <summary>
		/// Called manually when the table goes into edit mode
		/// </summary>
		public void WillBeginTableEditing (UITableView tableView)
		{
			//---- start animations
			tableView.BeginUpdates ();

			//---- insert a new row in the table
			tableView.InsertRows (new NSIndexPath[] { 
				NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0), 0) 
			}, UITableViewRowAnimation.Fade);
			//---- create a new item and add it to our underlying data
			tableItems.Add ("(Add New)");

			//---- end animations
			tableView.EndUpdates ();
		}

		/// <summary>
		/// Called manually when the table leaves edit mode
		/// </summary>
		public void DidFinishTableEditing (UITableView tableView)
		{
			//---- start animations
			tableView.BeginUpdates ();
			//---- remove our row from the underlying data
			tableItems.RemoveAt (tableView.NumberOfRowsInSection (0) - 1); // zero based :)
			//---- remove the row from the table
			tableView.DeleteRows (new NSIndexPath[] { NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0) - 1, 0) }, UITableViewRowAnimation.Fade);
			//---- finish animations
			tableView.EndUpdates ();
		}

		#endregion
	}
}