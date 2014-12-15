using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MonoTouch.Foundation;

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
			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users/auth");

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
		public async static Task<string> EditUser(string id, string username, string password,string passwordCheck, string firstName, string lastName, string email, string movements=""){

			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users/" + id);
			var postData = "{";

			if(!String.IsNullOrEmpty(firstName))
				postData += "\"firstName\": " + "\"" + firstName + "\"" + "," ;
			if(!String.IsNullOrEmpty(lastName))
				postData += "\"lastName\": " + "\"" + lastName  + "\"" + "," ;
			if(!String.IsNullOrEmpty(email))
				postData += "\"email\": " + "\"" + email  + "\"" + "," ;
			if(!String.IsNullOrEmpty(username))
				postData += "\"username\": " + "\"" + username  + "\"" + "," ;
			if(!String.IsNullOrEmpty(password))
				postData += "\"password\": " + "\"" + password  + "\"" + "," ;
			if(!String.IsNullOrEmpty(passwordCheck))
				postData += "," + "\"passwordCheck\": " + "\"" + passwordCheck  + "\"";
			postData += "}";
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
}

