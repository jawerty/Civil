using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace CivilPrototype
{
	public static class DataLayer
	{
		public async static Task<string> CreateUser(string firstName,string lastName,string email,string username,string password,string passwordCheck){

			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users");

			var postData = "{\"firstName\": " + "\"" + firstName + "\"" + "," ;
			postData += "\"lastName\": " + "\"" + lastName  + "\"" + "," ;
			postData += "\"email\": " + "\"" + email  + "\"" + "," ;
			postData += "\"username\": " + "\"" + username  + "\"" + "," ;
			postData += "\"password\": " + "\"" + password  + "\"" + "," ;
			postData += "\"passwordCheck\": " + "\"" + passwordCheck  + "\"" + "}" ;
			Console.WriteLine (postData);
			var data = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			var response = (HttpWebResponse) await request.GetResponseAsync();

			var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
			var responseObj = Newtonsoft.Json.Linq.JObject.Parse (responseString);
			if (((String)responseObj ["message"]).Contains("Created")) {
				await LoginUser(username, password);
			}
			return responseString;
		}
		public async static Task<string> LoginUser(string username, string password){
			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/usersAuth");

			var postData = "{\"username\": " + "\"" + username + "\"" + "," ;
			postData += "\"password\": " + "\"" + password  + "\"" + "}" ;
			var data = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			var response = (HttpWebResponse) await request.GetResponseAsync();

			var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
			var responseObj = Newtonsoft.Json.Linq.JObject.Parse (responseString);
			if ((string)responseObj ["message"] == "User authenticated") {
				NSUserDefaults.StandardUserDefaults.SetBool (true, "userLoggedIn");
				NSUserDefaults.StandardUserDefaults.SetString ((string)responseObj["userID"], "currentUserID");
				NSUserDefaults.StandardUserDefaults.SetString ((string)responseObj["token"], "currentUserToken");
				var s = (string)NSUserDefaults.StandardUserDefaults.StringForKey ("currentUserID");
				await GetUserData(s);
			}
			return responseString;
		}
		public async static Task<string> GetUserData(string id){
			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users/"+id);
			request.Method = "GET";
			var response = (HttpWebResponse) await request.GetResponseAsync();

			var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
			responseString = responseString.Insert (39, '\''.ToString ()).Insert (responseString.Length-1, '\''.ToString ());
			Console.WriteLine (responseString);
			var responseObj = Newtonsoft.Json.Linq.JObject.Parse (responseString);
			var document = Newtonsoft.Json.Linq.JObject.Parse ((string)responseObj ["document"]);
			if ((string)responseObj ["message"] == "User found") {
				NSUserDefaults.StandardUserDefaults.SetString ((string)document["firstName"], "currentUserFirstName");
				NSUserDefaults.StandardUserDefaults.SetString ((string)document["lastName"], "currentUserLastName");
				NSUserDefaults.StandardUserDefaults.SetString ((string)document["username"], "currentUserUsername");
				NSUserDefaults.StandardUserDefaults.SetString ((string)document["email"], "currentUserEmail");
			}
			return responseString;
		}
		public async static Task<string> SendBitMap(){
			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users");
			string firstName = "jeff";
			string lastName = "chester";
			string email = "carter@mail.com";
			string username = "jeff";
			string password = "testtest";
			string passwordCheck = "testtest";
			var bites = ToBytes (UIImage.FromFile ("edit.png"));
			var str = Encoding.UTF8.GetString(bites, 0, bites.Length);
			var postData = "{\"firstName\": " + "\"" + firstName + "\"" + "," ;
			postData += "\"lastName\": " + "\"" + lastName  + "\"" + "," ;
			postData += "\"email\": " + "\"" + email  + "\"" + "," ;
			postData += "\"username\": " + "\"" + username  + "\"" + "," ;
			postData += "\"password\": " + "\"" + password  + "\"" + "," ;
			postData += "\"avatar\": " + "\"" + str  + "\"" + "," ;
			postData += "\"passwordCheck\": " + "\"" + passwordCheck  + "\"" + "}" ;
			Console.WriteLine (postData);
			var data = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			var response = (HttpWebResponse) await request.GetResponseAsync();

			var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
			var responseObj = Newtonsoft.Json.Linq.JObject.Parse (responseString);
			if (((String)responseObj ["message"]).Contains("Created")) {
				await LoginUser(username, password);
			}
			return responseString;
		}
		public static UIImage ToImage(byte[] data)
		{
			if (data==null) {
				return null;
			}
			UIImage image = null;
			try {

				image = new UIImage(NSData.FromArray(data));
				data = null;
			} catch (Exception ) {
				return null;
			}
			return image;
		}
		public static byte[] ToBytes(UIImage image){
			byte[] data = null;
			if (image == null) {
				data = null;
			} else {
				NSData nsdata = null;

				try {
					nsdata = image.AsPNG ();
					data = nsdata.ToArray();
				} catch (Exception) {

				} finally {
					if (image != null) {
						image.Dispose ();
						image = null;
					}
					if (nsdata != null) {
						nsdata.Dispose ();
						nsdata = null;
					}
				}
			}
			return data;
		}
		public async static Task<UIImage> GetBitMap(){
			return new UIImage ();
		}
		public async static Task<string> EditUser(string id, string username, string password,string passwordCheck, string firstName, string lastName, string email, string movements=""){

			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users/" + id);
			var editableUser = new EditableUserObject ();

			if (!String.IsNullOrEmpty (firstName))
				editableUser.firstName = firstName;
			if(!String.IsNullOrEmpty(lastName))
				editableUser.lastName = lastName;
			if(!String.IsNullOrEmpty(email))
				editableUser.email = email;
			if(!String.IsNullOrEmpty(username))
				editableUser.username = username;
			if(!String.IsNullOrEmpty(password))
				editableUser.password = password;
			if(!String.IsNullOrEmpty(passwordCheck))
				editableUser.passwordCheck = passwordCheck;
			var postData = Newtonsoft.Json.JsonConvert.SerializeObject (editableUser);
			Console.WriteLine (postData);
			var data = Encoding.ASCII.GetBytes(postData);

			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			var response = (HttpWebResponse) await request.GetResponseAsync();

			var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
			var responseObj = Newtonsoft.Json.Linq.JObject.Parse (responseString);
			if (((String)responseObj ["message"]).Contains("success")) {
				await GetUserData (id);
			}
			return responseString;
		}
		public async static Task<string> DeleteUser(string id){

			return "";

		}
	}
	public class UserObject : NSObject{

		public string userId { get; set; }
		public string token { get; set; }

	}
	public class EditableUserObject : NSObject{

		public string firstName { get; set;}
		public string lastName {get;set;}
		public string email { get; set;}
		public string password { get; set;}
		public string passwordCheck{ get; set; }
		public string username{ get; set;}
		public string skills { get; set;}

	}
}

