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

			var response = (HttpWebResponse)request.GetResponse();

			var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
			NSUserDefaults.StandardUserDefaults.SetBool (true, "userLoggedIn");
			return responseString;
		}
		public async static Task<string> LoginUser(string username, string password){

			return "";
		}
		public async static Task<string> GetUserData(string id){

			return "";
		}
		public async static Task<string> EditUser(string username, string password, string firstName, string lastName, string movements, string email){

			return "";

		}
		public async static Task<string> DeleteUser(string id){

			return "";

		}
	}
}

