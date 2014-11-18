using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Net;
using System.IO;


namespace CivilPrototype
{
	partial class HomeController : UIViewController
	{
		public HomeController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			WebRequest request = WebRequest.Create ("http://127.0.0.1:3000");
			// If required by the server, set the credentials.
			// Get the response.
			HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
			// Display the status.
			Console.WriteLine (response.StatusDescription);
			// Get the stream containing content returned by the server.
			Stream dataStream = response.GetResponseStream ();
			// Open the stream using a StreamReader for easy access.
			StreamReader reader = new StreamReader (dataStream);
			// Read the content. 
			string responseFromServer = reader.ReadToEnd ();
			// Display the content.
			Console.WriteLine (responseFromServer);
			// Cleanup the streams and the response.
			reader.Close ();
			dataStream.Close ();
			response.Close ();
			//SessionTest.Text = NSUserDefaults.StandardUserDefaults.BoolForKey ("isLoggedIn").ToString();
			SessionTest.Text = responseFromServer;
		}
	}
}
