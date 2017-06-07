// /*
//  * TeleStax, Open Source Cloud Communications
//  * Copyright 2011-2016, Telestax Inc and individual contributors
//  * by the @authors tag.
//  *
//  * This is free software; you can redistribute it and/or modify it
//  * under the terms of the GNU Lesser General Public License as
//  * published by the Free Software Foundation; either version 2.1 of
//  * the License, or (at your option) any later version.
//  *
//  * This software is distributed in the hope that it will be useful,
//  * but WITHOUT ANY WARRANTY; without even the implied warranty of
//  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  * Lesser General Public License for more details.
//  *
//  * You should have received a copy of the GNU Lesser General Public
//  * License along with this software; if not, write to the Free
//  * Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
//  * 02110-1301 USA, or see the FSF site: http://www.fsf.org.
//  */
//
using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace RestComm
{
	//this class contains all Acccount info and methods .
	public partial class Account{
		public List<Application> GetApplicationList(){
			RestClient client = new RestClient (baseurl + "Accounts/" +Properties. Sid + "/Applications");
			RestRequest login = new RestRequest (Method.GET);
			client.Authenticator = new HttpBasicAuthenticator (Properties.Sid, Properties.authtoken);
			IRestResponse response = client.Execute (login);

			string content = response.Content;

				List<string> Sidlist = content.GetAccountsProperty (Property.Sid);
		if (Sidlist != null) {
				List<Application> applist = new List<Application> ();
				
				foreach (string s in Sidlist) {
					
					applist.Add (new Application (Properties.Sid, Properties.authtoken, s));

				}

	
				return applist;
			} else
				return null;

		}


		public Application CreateApplication(string FriendlyName,string ApiVersion=null,bool HasVoiceCallerIdLookup=false,string RcmlUrl=null,String Kind=null){

			RestClient client = new RestClient (baseurl + "Accounts/" + Properties.Sid + "/Applications");
			client.Authenticator = new HttpBasicAuthenticator (Properties.Sid, Properties.authtoken);
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
			return new Application (Properties.Sid, Properties.authtoken, response.Content.GetAccountProperty (Property.Sid));

		}


	}

	public class Application
	{	
		public applicationProperties Properties;
		public Application(String accountsid,String tokenno,string ApplicationSid){

				RestClient client = new RestClient(Account.baseurl+"Accounts/"+accountsid+"/Applications/"+ApplicationSid);
			RestRequest login = new RestRequest(Method.GET);

			client.Authenticator = new HttpBasicAuthenticator(accountsid, tokenno);

			IRestResponse response = client.Execute(login);
			var content = response.Content;
			Properties.Sid = content.GetAccountProperty (Property.Sid);
			Properties.AccountSid=content.GetAccountProperty (Property.AccountSid);
			Properties.FriendlyName = content.GetAccountProperty (Property.FriendlyName);
			Properties.DateUpdated = content.GetAccountProperty (Property.DateUpdated);
			Properties.DateCreated = content.GetAccountProperty (Property.DateCreated);
			Properties.APIversion = content.GetAccountProperty (Property.ApiVersion);
			Properties.Kind = content.GetAccountProperty (Property.Kind);
			Properties.authtoken = tokenno;
			

		}
		public void Delete(){

			RestClient client = new RestClient(Account.baseurl+"Accounts/"+Properties.AccountSid+"/Applications/"+Properties.Sid);
			RestRequest login = new RestRequest(Method.DELETE);
			client.Authenticator = new HttpBasicAuthenticator(Properties.AccountSid,Properties.authtoken);
			 client.Execute(login);


		}

	}

}

