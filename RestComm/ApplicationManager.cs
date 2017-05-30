using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace RestComm
{
	public partial class Account{
		public List<Application> GetApplicationList(){
			RestClient client = new RestClient (baseurl + "Accounts/" + Sid + "/Applications");
			RestRequest login = new RestRequest (Method.GET);
			client.Authenticator = new HttpBasicAuthenticator (Sid, authtoken);
			IRestResponse response = client.Execute (login);

			string content = response.Content;

				List<string> Sidlist = content.GetAccountsProperty (Property.Sid);
				List<Application> applist = new List<Application> ();
				
				foreach (string s in Sidlist) {
					
				applist.Add (new Application(Sid,authtoken,s));

				}

	
				return applist;

		}
		public Application CreateApplication(string FriendlyName,string ApiVersion=null,bool HasVoiceCallerIdLookup=false,string RcmlUrl=null,String Kind=null){

			RestClient client = new RestClient (baseurl + "Accounts/" + Sid + "/Applications");
			client.Authenticator = new HttpBasicAuthenticator (Sid, authtoken);
			RestRequest sendreq = new RestRequest (Method.POST);
			sendreq.AddParameter ("FriendlyName", FriendlyName);
			if (ApiVersion != null)
				sendreq.AddParameter ("ApiVersion", ApiVersion);
			if(HasVoiceCallerIdLookup!=false)
				sendreq.AddParameter ("HasVoiceCallerIdLookup", "true");
			if(RcmlUrl!=null)
				sendreq.AddParameter ("RcmlUrl",RcmlUrl);
			if (Kind != null) {
				sendreq.AddParameter ("Kind", Kind);
			}
			
			IRestResponse response= client.Execute (sendreq);
			return new Application (Sid, authtoken, response.Content.GetAccountProperty (Property.Sid));

		}


	}

	public class Application
	{	
		public String Sid;
		public String AccountSid;
		public String FriendlyName;
		public String DateUpdated;
		public String DateCreated;
		public String APIversion;
		public String Kind;
		public String authtoken;
		public Application(String accountsid,String tokenno,string ApplicationSid){

				RestClient client = new RestClient(Account.baseurl+"Accounts/"+accountsid+"/Applications/"+ApplicationSid);
			RestRequest login = new RestRequest(Method.GET);

			client.Authenticator = new HttpBasicAuthenticator(accountsid, tokenno);

			IRestResponse response = client.Execute(login);
			var content = response.Content;
			Sid = content.GetAccountProperty (Property.Sid);
			AccountSid=content.GetAccountProperty (Property.AccountSid);
			FriendlyName = content.GetAccountProperty (Property.FriendlyName);
			DateUpdated = content.GetAccountProperty (Property.DateUpdated);
			DateCreated = content.GetAccountProperty (Property.DateCreated);
			APIversion = content.GetAccountProperty (Property.ApiVersion);
			//Kind = content.GetAccountProperty (Property.Kind);
			authtoken = tokenno;
			

		}
		public void Delete(){

			RestClient client = new RestClient(Account.baseurl+"Accounts/"+AccountSid+"/Applications/"+Sid);
			RestRequest login = new RestRequest(Method.DELETE);
			client.Authenticator = new HttpBasicAuthenticator(AccountSid,authtoken);
			 client.Execute(login);


		}

	}

}

