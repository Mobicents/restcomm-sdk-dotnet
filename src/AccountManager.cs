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
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;



namespace RestComm
{
	
	public partial class Account
		{
		
		public accountProperties Properties;


		public static string baseurl="https://cloud.restcomm.com/restcomm/2012-04-24/";

		//constructor method to instantiate account 

		public Account(string sid,string Tokenno)
		{


			RestClient client = new RestClient(baseurl+"Accounts/"+sid);
			RestRequest login = new RestRequest(Method.GET);

			client.Authenticator = new HttpBasicAuthenticator(sid, Tokenno);

			IRestResponse response = client.Execute(login);
			var content = response.Content;
			Properties.Sid = content.GetAccountProperty (Property.Sid);
			Properties.friendlyname = content.GetAccountProperty ("FriendlyName");
			Properties.status = content.GetAccountProperty ("Status");
			Properties.dateupdated = content.GetAccountProperty ("DateUpdated");
			Properties.datecreated =content.GetAccountProperty ("DateCreated");
			Properties.authtoken=content.GetAccountProperty("AuthToken");


		}

		public void ChangePassword(string NewPassword){

			RestClient client = new RestClient(baseurl+"Accounts/"+Properties.Sid);
			RestRequest login = new RestRequest(Method.POST);

			client.Authenticator = new HttpBasicAuthenticator(Properties.Sid, Properties.authtoken);
			login.AddParameter ("Password", NewPassword);
			IRestResponse response = client.Execute(login);
			var content = response.Content;


			Properties.dateupdated = content.GetAccountProperty ("DateUpdated");

			Properties.authtoken=content.GetAccountProperty("AuthToken");



		}	









		/// <summary>
		/// this will create a subaccount .
		/// </summary>
		public SubAccount CreateSubAccount(string FriendlyName,string emailid,string password){
			
			RestClient client = new RestClient(baseurl+"Accounts/");
			RestRequest login = new RestRequest(Method.POST);
			login.AddParameter("FriendlyName",FriendlyName);
			login.AddParameter("EmailAddress",emailid);
			login.AddParameter ("Password", password);
			client.Authenticator = new HttpBasicAuthenticator(Properties.Sid,Properties.authtoken);
			IRestResponse response = client.Execute (login);
			var content = response.Content;
			SubAccount newaccount = new SubAccount (content.GetAccountProperty(Property.Sid),content.GetAccountProperty(Property.AuthToken));
			return newaccount;


		}

		public List<SubAccount> GetSubAccountList(){

			RestClient client = new RestClient(baseurl+"Accounts/");
			RestRequest login = new RestRequest(Method.GET);
			client.Authenticator = new HttpBasicAuthenticator(Properties.Sid,Properties.authtoken);
			IRestResponse response = client.Execute (login);
			var content = response.Content;
			List<string> sidlist = content.GetAccountsProperty (Property.Sid);
			List<string> authtokenlist=content.GetAccountsProperty (Property.AuthToken);

			List<SubAccount> subaccountlist = new List<SubAccount> ();
			for(int i=0;i<sidlist.Count;i++){
				subaccountlist.Add (new SubAccount (sidlist [i], authtokenlist [i]));

			}
			return subaccountlist;

		}




	}



	public class SubAccount:Account
	{
		public SubAccount(string Sid,string Tokenno):base(Sid,Tokenno){

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
			Properties.Sid = content.GetAccountProperty ("Sid");
			Properties.friendlyname = content.GetAccountProperty ("FriendlyName");
			Properties.status = content.GetAccountProperty ("Status");
			Properties.dateupdated = content.GetAccountProperty ("DateUpdated");
			Properties.datecreated = content.GetAccountProperty ("DateCreated");
			Properties.authtoken = content.GetAccountProperty ("AuthToken");



		}
		public string CloseSubAccount(){
			RestClient client = new RestClient(baseurl+"Accounts/"+Properties.Sid);
			RestRequest login = new RestRequest(Method.PUT);
			client.Authenticator = new HttpBasicAuthenticator(Properties.Sid,Properties.authtoken);

			login.AddParameter("Status","closed");
			IRestResponse response = client.Execute(login);

			return response.Content;
		}

	}
}



