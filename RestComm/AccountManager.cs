using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;



namespace RestComm
{
	
	public partial class Account
		{
		
		public string Sid;
		public string friendlyname;
		public string status;
		public string dateupdated;
		public string datecreated;
		public string authtoken;
		public string uri;
			

		public static string baseurl="https://cloud.restcomm.com/restcomm/2012-04-24/";

		public void ChangePassword(string NewPassword){

			RestClient client = new RestClient(baseurl+"Accounts/"+Sid);
			RestRequest login = new RestRequest(Method.POST);

			client.Authenticator = new HttpBasicAuthenticator(Sid, authtoken);
			login.AddParameter ("Password", NewPassword);
			IRestResponse response = client.Execute(login);
			var content = response.Content;


			dateupdated = content.GetAccountProperty ("DateUpdated");

			authtoken=content.GetAccountProperty("AuthToken");



		}	



					


		/// <summary>
		/// This method will load all the info about the account in the class's variables like sid , authtoken .
		/// </summary>
		public Account(string sid,string Tokenno)
		{
			

			RestClient client = new RestClient(baseurl+"Accounts/"+sid);
			RestRequest login = new RestRequest(Method.GET);
		
			client.Authenticator = new HttpBasicAuthenticator(sid, Tokenno);

			IRestResponse response = client.Execute(login);
			var content = response.Content;
			Sid = content.GetAccountProperty (Property.Sid);
			friendlyname = content.GetAccountProperty ("FriendlyName");
			status = content.GetAccountProperty ("Status");
			dateupdated = content.GetAccountProperty ("DateUpdated");
			datecreated =content.GetAccountProperty ("DateCreated");
			authtoken=content.GetAccountProperty("AuthToken");


			}
		public string GetAccountDetail(){

			RestClient client = new RestClient(baseurl+"Accounts/"+Sid);
			RestRequest login = new RestRequest(Method.GET);
			client.Authenticator = new HttpBasicAuthenticator(Sid, authtoken);
			IRestResponse response = client.Execute(login);
			return response.Content;

		}


		/// <summary>
		/// this will create a subaccount .
		/// </summary>
		public subaccount CreateSubAccount(string FriendlyName,string emailid,string password){
			
			RestClient client = new RestClient(baseurl+"Accounts/");
			RestRequest login = new RestRequest(Method.POST);
			login.AddParameter("FriendlyName",FriendlyName);
			login.AddParameter("EmailAddress",emailid);
			login.AddParameter ("Password", password);
			client.Authenticator = new HttpBasicAuthenticator(Sid,authtoken);
			IRestResponse response = client.Execute (login);
			var content = response.Content;
			subaccount newaccount = new subaccount (content.GetAccountProperty(Property.Sid),content.GetAccountProperty(Property.AuthToken));
			return newaccount;


		}

		public List<subaccount> GetSubAccountList(){

			RestClient client = new RestClient(baseurl+"Accounts/");
			RestRequest login = new RestRequest(Method.GET);
			client.Authenticator = new HttpBasicAuthenticator(Sid,authtoken);
			IRestResponse response = client.Execute (login);
			var content = response.Content;
			List<string> sidlist = content.GetAccountsProperty (Property.Sid);
			List<string> authtokenlist=content.GetAccountsProperty (Property.AuthToken);

			List<subaccount> subaccountlist = new List<subaccount> ();
			for(int i=0;i<sidlist.Count;i++){
				subaccountlist.Add (new subaccount (sidlist [i], authtokenlist [i]));

			}
			return subaccountlist;

		}


			



		}
	public class subaccount:Account
	{
		public subaccount(string Sid,string Tokenno):base(Sid,Tokenno){

		}
		public void createsubaccount(string sid, string tokenno,string FriendlyName,string emailid,string password){
			
			RestClient client = new RestClient(baseurl+"Accounts/");
			RestRequest login = new RestRequest(Method.POST);
			login.AddParameter("FriendlyName",FriendlyName);
			login.AddParameter("EmailAddress",emailid);
			login.AddParameter ("Password", password);

			client.Authenticator = new HttpBasicAuthenticator(sid,tokenno);
			IRestResponse response = client.Execute (login);
			var content = response.Content;
			Sid = content.GetAccountProperty ("Sid");
			friendlyname = content.GetAccountProperty ("FriendlyName");
			status = content.GetAccountProperty ("Status");
			dateupdated = content.GetAccountProperty ("DateUpdated");
			datecreated = content.GetAccountProperty ("DateCreated");
			authtoken = content.GetAccountProperty ("AuthToken");



		}
		public string CloseSubAccount(){
			RestClient client = new RestClient(baseurl+"Accounts/"+Sid);
			RestRequest login = new RestRequest(Method.PUT);
			client.Authenticator = new HttpBasicAuthenticator(Sid,authtoken);

			login.AddParameter("Status","closed");
			IRestResponse response = client.Execute(login);

			return response.Content;
		}

	}
}

