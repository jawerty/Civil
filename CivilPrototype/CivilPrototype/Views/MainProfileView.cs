using System;
using MonoTouch.UIKit;
using System.Threading;
using System.Drawing;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Linq;

namespace CivilPrototype
{
	public class MainProfileView : UIView
	{
		EditProfileButton editButton;
		UITextView profileView;
		UIView mainProfileView;
		UIScrollView scrollProfileView;
		float scrollViewWidth;
		float scrollViewX;
		float skillsListHeight;
		List<string> listSkills;
		EditableListView skillsList;
		bool editing = false;
		public EditProfileButton EditButton{
			get{ return editButton; }
			set { editButton = value; }

		}
		public async void EditProfileAsync (string id, string username, string password, string passwordCheck, string firstName, string lastName, string email, string movements = "")
		{
			await DataLayer.EditUser (id, username, password, passwordCheck, firstName, lastName, email, listSkills);
			skillsList.SetEditing (false,false);
			scrollProfileView.RemoveFromSuperview ();
			scrollProfileView = new UIScrollView (new RectangleF (0, 50, Bounds.Width, 500)) {
				new UITextView {
					Text = "Username: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserUsername"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 100, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Email: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserEmail"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 200, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "First Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserFirstName"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 300, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Last Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserLastName"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 400, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				skillsList
			};
			scrollProfileView.DelaysContentTouches = false;
			scrollProfileView.CanCancelContentTouches = false;
			scrollProfileView.ContentSize = new SizeF (Bounds.Width, 825 + skillsList.Height);
			Add (scrollProfileView);
		}
		public MainProfileView (RectangleF frame) :  base()
		{
			this.Frame = frame;
			string userFirstName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserFirstName");
			string userLastName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserLastName");
			string userEmail = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserEmail");
			string userName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserUsername");
			string skills = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserSkills");
			listSkills = (Newtonsoft.Json.Linq.JArray.Parse(skills)).Select(t =>(string)t).ToList();
			this.scrollViewWidth = Bounds.Width - 20;
			this.scrollViewX = (Bounds.Width / 2) - (scrollViewWidth / 2);
			var u = new UITextField {
				Placeholder = "Username",
				Frame = new RectangleF (scrollViewX, 150, scrollViewWidth, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
			};
			var e = new UITextField {
				Placeholder = "Email",
				Frame = new RectangleF (scrollViewX, 250, scrollViewWidth, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
			};
			var fn = new UITextField {
				Placeholder = "First Name",
				Frame = new RectangleF (scrollViewX, 350, scrollViewWidth, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
			};
			var ln = new UITextField {
				Placeholder = "Last Name",
				Frame = new RectangleF (scrollViewX, 450, scrollViewWidth, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
			};
			var p = new UITextField {
				Placeholder = "Password",
				Frame = new RectangleF (scrollViewX, 500, scrollViewWidth, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
			};
			var pCheck = new UITextField {
				Placeholder = "Password Check",
				Frame = new RectangleF (scrollViewX, 550, scrollViewWidth, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
				Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
			};
			var submit = UIButton.FromType (DesignConstants.ButtonType);
			skillsList = new EditableListView (listSkills,new RectangleF (scrollViewX, 600,	scrollViewWidth, 200 ));
			skillsList.sourceRowDelete += delegate {
				UIView.Animate(.1,delegate {
					skillsList.Frame = new RectangleF (scrollViewX, 600,	scrollViewWidth, skillsList.Height);
					submit.Frame = new RectangleF (scrollViewX, 625 + skillsList.Height, scrollViewWidth, 40);
					scrollProfileView.ContentSize = new SizeF (Bounds.Width, 825 + skillsList.Height);
				});
			};
			skillsList.sourceRowInsert += delegate {
				UIView.Animate(.1,delegate {
					skillsList.Frame = new RectangleF (scrollViewX, 600,	scrollViewWidth, skillsList.Height);
					scrollProfileView.ContentSize = new SizeF (Bounds.Width, 825 + skillsList.Height);
					submit.Frame = new RectangleF (scrollViewX, 625 + skillsList.Height, scrollViewWidth, 40);
				});			
			};
			skillsList.Frame = new RectangleF (scrollViewX, 600,	scrollViewWidth, skillsList.Height);
			skillsListHeight = skillsList.Height;
			submit.Frame = new RectangleF (scrollViewX, 625 + skillsList.Height, scrollViewWidth, 40);
			submit.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			submit.SetTitle ("Make Changes", UIControlState.Normal);
			submit.TouchUpInside += delegate {
				var user = u.Text;
				var email = e.Text;
				var first = fn.Text;
				var last = ln.Text;
				var pass = p.Text;
				var passCheck = pCheck.Text;
				this.EditProfileAsync (NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserID"), user, pass, passCheck, first, last, email);
				editing = false;
			};
			editButton = new EditProfileButton (Bounds.Width);
			editButton.ButtonTapped += delegate{

				if (!editing) {
					editing = true;
					editButton.Alpha = .5f;
					InvokeOnMainThread (delegate {
						scrollProfileView.Add (u);
						scrollProfileView.Add (e);
						scrollProfileView.Add (fn);
						scrollProfileView.Add (ln);
						scrollProfileView.Add (p);
						scrollProfileView.Add (pCheck);
						scrollProfileView.Add (submit);
						skillsList.SetEditing(true,true);
					});
				} else {
					editing = false;
					editButton.Alpha = 1f;
					InvokeOnMainThread (delegate {
						u.RemoveFromSuperview ();
						fn.RemoveFromSuperview ();
						ln.RemoveFromSuperview ();
						p.RemoveFromSuperview ();
						pCheck.RemoveFromSuperview ();
						submit.RemoveFromSuperview ();
						e.RemoveFromSuperview ();
						skillsList.SetEditing(false,true);
					});
				}
			};
			scrollProfileView = new UIScrollView (new RectangleF (0, 50, Bounds.Width, 500)) {
				new UITextView {
					Text = "Username: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserUsername"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 100, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Email: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserEmail"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 200, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "First Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserFirstName"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 300, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Last Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserLastName"),
					TextAlignment = DesignConstants.HeaderAlignment,
					Frame = new RectangleF (scrollViewX, 400, scrollViewWidth, 40),
					Font = UIFont.FromName(DesignConstants.HeaderFontStyle,DesignConstants.HeaderSmallFontSize),
					BackgroundColor = UIColor.Clear,
				},
				skillsList
			};
			scrollProfileView.DelaysContentTouches = false;
			scrollProfileView.CanCancelContentTouches = false;
			scrollProfileView.ContentSize = new SizeF (Bounds.Width, 825 + skillsListHeight);
			Add (scrollProfileView);

		}
	}
}

