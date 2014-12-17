using System;
using MonoTouch.UIKit;
using System.Threading;
using System.Drawing;
using MonoTouch.Foundation;

namespace CivilPrototype
{
	public class MainProfileView : UIView
	{
		EditProfileButton editButton;
		UITextView profileView;
		UIView mainProfileView;
		UIScrollView scrollProfileView;
		bool editing = false;
		public async void EditProfileAsync (string id, string username, string password, string passwordCheck, string firstName, string lastName, string email, string movements = "")
		{
			await DataLayer.EditUser (id, username, password, passwordCheck, firstName, lastName, email);
			scrollProfileView.RemoveFromSuperview ();
			scrollProfileView = new UIScrollView (new RectangleF (0, 50, Bounds.Width, 500)) {
				new UITextView {
					Text = "Username: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserUsername"),
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 100, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Email: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserEmail"),
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 200, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "First Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserFirstName"),
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 300, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Last Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserLastName"),
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 400, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
			};
			scrollProfileView.DelaysContentTouches = false;
			scrollProfileView.CanCancelContentTouches = false;
			scrollProfileView.ContentSize = new SizeF (Bounds.Width, 800);
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
			var u = new UITextField {
				Placeholder = "Username",
				Frame = new RectangleF ((Bounds.Width / 2) - 100, 150, 200, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
			};
			var e = new UITextField {
				Placeholder = "Email",
				Frame = new RectangleF ((Bounds.Width / 2) - 100, 250, 200, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
			};
			var fn = new UITextField {
				Placeholder = "First Name",
				Frame = new RectangleF ((Bounds.Width / 2) - 100, 350, 200, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
			};
			var ln = new UITextField {
				Placeholder = "Last Name",
				Frame = new RectangleF ((Bounds.Width / 2) - 100, 450, 200, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
			};
			var p = new UITextField {
				Placeholder = "Password",
				Frame = new RectangleF ((Bounds.Width / 2) - 100, 500, 200, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
			};
			var pCheck = new UITextField {
				Placeholder = "Password Check",
				Frame = new RectangleF ((Bounds.Width / 2) - 100, 550, 200, 40),
				BackgroundColor = UIColor.White,
				BorderStyle = DesignConstants.TextFieldBorderStyle,
			};
			var skillsList = new EditableListView (new string[]{"hello","hi"});
			skillsList.Frame = new RectangleF ((Bounds.Width / 2) - 100, 600, 200, 200);
			var submit = UIButton.FromType (DesignConstants.ButtonType);
			submit.Frame = new RectangleF ((Bounds.Width / 2) - 100, 600, 200, 40);
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
				editing = false;
			};
			string userFirstName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserFirstName");
			string userLastName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserLastName");
			string userEmail = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserEmail");
			string userName = NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserUsername");
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
						//scrollProfileView.Add (submit);
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
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 100, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Email: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserEmail"),
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 200, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "First Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserFirstName"),
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 300, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
				new UITextView {
					Text = "Last Name: " + NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserLastName"),
					Frame = new RectangleF ((Bounds.Width / 2) - 100, 400, 200, 40),
					BackgroundColor = UIColor.Clear,
				},
			};
			scrollProfileView.DelaysContentTouches = false;
			scrollProfileView.CanCancelContentTouches = false;
			scrollProfileView.ContentSize = new SizeF (Bounds.Width, 800);
			Add (scrollProfileView);
			Add (profileView);
			Add (editButton);

		}
	}
}

