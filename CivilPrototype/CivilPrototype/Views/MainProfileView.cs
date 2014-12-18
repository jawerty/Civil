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
		bool editing = false;
		public async void EditProfileAsync (string id, string username, string password, string passwordCheck, string firstName, string lastName, string email, string movements = "")
		{
			await DataLayer.EditUser (id, username, password, passwordCheck, firstName, lastName, email);
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
			};
			scrollProfileView.DelaysContentTouches = false;
			scrollProfileView.CanCancelContentTouches = false;
			scrollProfileView.ContentSize = new SizeF (Bounds.Width, 825 + skillsListHeight);
			Add (scrollProfileView);
		}

		private void SlowMethod ()
		{
			Thread.Sleep (300);
			InvokeOnMainThread (delegate {
				editButton.BackgroundColor = UIColor.White;
			});
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
			var skillsList = new EditableListView (listSkills,new RectangleF (scrollViewX, 600,	scrollViewWidth, 200 ));
			skillsList.Frame = new RectangleF (scrollViewX, 600,	scrollViewWidth, skillsList.Height);
			skillsListHeight = skillsList.Height;
			var submit = UIButton.FromType (DesignConstants.ButtonType);
			submit.Frame = new RectangleF (scrollViewX, 625 + skillsListHeight, scrollViewWidth, 40);
			submit.Font = UIFont.FromName (DesignConstants.ButtonFontStyle, DesignConstants.NormalButtonFontSize);
			submit.SetTitle ("Make Changes", DesignConstants.ButtonControlState);
			submit.TouchUpInside += delegate {
				var user = u.Text;
				var email = e.Text;
				var first = fn.Text;
				var last = ln.Text;
				var pass = p.Text;
				var passCheck = pCheck.Text;
				this.EditProfileAsync (NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserID"), user, pass, passCheck, first, last, email);
				u.Text = "";
				fn.Text = "";
				ln.Text = "";
				p.Text = "";
				pCheck.Text = "";
				e.Text = "";
				u.RemoveFromSuperview ();
				fn.RemoveFromSuperview ();
				ln.RemoveFromSuperview ();
				p.RemoveFromSuperview ();
				pCheck.RemoveFromSuperview ();
				submit.RemoveFromSuperview ();
				e.RemoveFromSuperview ();
				skillsList.RemoveFromSuperview();
				editing = false;
			};
			editButton = new EditProfileButton (Bounds.Width);
			editButton.ButtonTapped += delegate(NSSet touches, UIEvent evt) {
				editButton.BackgroundColor = DesignConstants.lgrey;
				ThreadPool.QueueUserWorkItem (o => SlowMethod ());

				if (!editing) {
					editing = true;
					InvokeOnMainThread (delegate {
						scrollProfileView.Add (u);
						scrollProfileView.Add (e);
						scrollProfileView.Add (fn);
						scrollProfileView.Add (ln);
						scrollProfileView.Add (p);
						scrollProfileView.Add (pCheck);
						scrollProfileView.Add (submit);
						scrollProfileView.Add (skillsList);
					});
				} else {
					editing = false;
					InvokeOnMainThread (delegate {
						u.RemoveFromSuperview ();
						fn.RemoveFromSuperview ();
						ln.RemoveFromSuperview ();
						p.RemoveFromSuperview ();
						pCheck.RemoveFromSuperview ();
						submit.RemoveFromSuperview ();
						e.RemoveFromSuperview ();
						skillsList.RemoveFromSuperview();
					});
				}
			};
			profileView = new UITextView {
				Text = "Profile",
				Font = UIFont.FromName (DesignConstants.HeaderFontStyle, DesignConstants.HeaderLargeFontSize),
				BackgroundColor = DesignConstants.HeaderBackground,
				TextAlignment = DesignConstants.HeaderAlignment,
				Frame = new RectangleF (DesignConstants.HeaderFrameX, 
					DesignConstants.HeaderFrameY, 
					Bounds.Width + DesignConstants.HeaderFrameWidth, 
					DesignConstants.HeaderFrameHeight)
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
			};
			scrollProfileView.DelaysContentTouches = false;
			scrollProfileView.CanCancelContentTouches = false;
			scrollProfileView.ContentSize = new SizeF (Bounds.Width, 825 + skillsListHeight);
			Add (scrollProfileView);
			Add (profileView);
			Add (editButton);

		}
	}
}

