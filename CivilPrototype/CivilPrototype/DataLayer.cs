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
				LoginUser (username, password);
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
				GetUserData(s);
			}
			return responseString;
		}
		public async static Task<string> GetUserData(string id){
			var request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3000/users/"+id);
			request.Method = "GET";
			var response = (HttpWebResponse) await request.GetResponseAsync();

			var responseString = new StreamReader (response.GetResponseStream ()).ReadToEnd ();
			Console.WriteLine (responseString);
			var responseObj = Newtonsoft.Json.Linq.JObject.Parse (responseString);
			if ((string)responseObj ["message"] == "User found") {
				NSUserDefaults.StandardUserDefaults.SetString ((string)responseObj["firstName"], "currentUserFirstName");
				NSUserDefaults.StandardUserDefaults.SetString ((string)responseObj["lastName"], "currentUserLastName");
				NSUserDefaults.StandardUserDefaults.SetString ((string)responseObj["username"], "currentUserUsername");
				NSUserDefaults.StandardUserDefaults.SetString ((string)responseObj["email"], "currentUserEmail");
				NSUserDefaults.StandardUserDefaults.SetString ((string)responseObj["movements"], "currentUserMovements");
			}
			return responseString;
		}
		public async static Task<string> EditUser(string username, string password, string firstName, string lastName, string movements, string email){

			return "";

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

