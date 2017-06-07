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
	
	public partial class Account{
		public List<Client>  GetClientList(){
			RestClient client = new RestClient (baseurl+"Accounts/"+Properties.Sid+"/Clients");
			RestRequest request = new RestRequest (Method.GET);
			client.Authenticator=new HttpBasicAuthenticator (Properties.Sid, Properties.authtoken);
			IRestResponse response = client.Execute (request);
			List<Client> clientslist = new List<Client> ();

			List<string> sidlist = response.Content.GetAccountsProperty ("Sid");
			int i = sidlist.Count;
			for (int j = 0; j < i; j++) {
				clientslist.Add (new Client (response.Content, j));

			}
			return clientslist;

		}
		public MakeClient makeclient(string Login,string Password){
			RestClient client = new RestClient (baseurl+"Accounts/"+Properties.Sid+"/Clients");
			RestRequest request = new RestRequest (Method.POST);
			request.AddParameter ("Login",Login);
			request.AddParameter ("Password", Password);
			client.Authenticator=new HttpBasicAuthenticator (Properties.Sid, Properties.authtoken);

			return new MakeClient (client, request);

		}

	}
	public class Client
	{
		
		public clientProperties Properties;
		
		public Client (string responsecontent,int  clientno)
		{
			Properties.Sid = responsecontent.GetAccountsProperty ("Sid")[clientno];
			Properties.DateCreated = responsecontent.GetAccountsProperty ("DateCreated")[clientno];
			Properties.DateUpdated = responsecontent.GetAccountsProperty ("DateUpdated")[clientno];
			Properties.FriendlyName = responsecontent.GetAccountsProperty ("FriendlyName")[clientno];
			Properties.AccountSid = responsecontent.GetAccountsProperty ("AccountSid")[clientno];
			Properties.ApiVersion = responsecontent.GetAccountsProperty ("ApiVersion")[clientno];
			Properties.Login = responsecontent.GetAccountsProperty ("Login")[clientno];
			Properties.Password = responsecontent.GetAccountsProperty ("Password")[clientno];
			Properties.Status = responsecontent.GetAccountsProperty ("Status")[clientno];
			Properties.VoiceUrl = responsecontent.GetAccountsProperty ("VoiceUrl")[clientno];
			Properties.VoiceMethod = responsecontent.GetAccountsProperty ("VoiceMethod")[clientno];
			Properties.VoiceFallbackUrl = responsecontent.GetAccountsProperty ("VoiceFallbackUrl")[clientno];
			Properties.	VoiceFallbackMethod = responsecontent.GetAccountsProperty ("VoiceFallbackMethod")[clientno];
			Properties.VoiceApplication = responsecontent.GetAccountsProperty ("VoiceApplication")[clientno];
			Properties.Uri = responsecontent.GetAccountsProperty ("Uri")[clientno];
		}
		public Client (string responsecontent)
		{
			Properties.Sid = responsecontent.GetAccountProperty ("Sid");
			Properties.DateCreated = responsecontent.GetAccountProperty ("DateCreated");
			Properties.DateUpdated = responsecontent.GetAccountProperty ("DateUpdated");
			Properties.FriendlyName = responsecontent.GetAccountProperty ("FriendlyName");
			Properties.AccountSid = responsecontent.GetAccountProperty ("AccountSid");
			Properties.ApiVersion = responsecontent.GetAccountProperty ("ApiVersion");
			Properties.Login = responsecontent.GetAccountProperty ("Login");
			Properties.Password = responsecontent.GetAccountProperty ("Password");
			Properties.Status = responsecontent.GetAccountProperty ("Status");
			Properties.VoiceMethod = responsecontent.GetAccountProperty ("VoiceMethod");
			Properties.VoiceFallbackMethod = responsecontent.GetAccountProperty ("VoiceFallbackMethod");
			Properties.Uri = responsecontent.GetAccountProperty ("Uri");
		}
		public void Delete(String sid, string authtoken){
			RestClient client = new RestClient (Account.baseurl+"Accounts/"+sid+"/Clients/"+Properties.Sid);
			RestRequest request = new RestRequest (Method.DELETE);
			client.Authenticator = new HttpBasicAuthenticator (sid, authtoken);
			client.Execute (request);

		}
		public void ChangePassword(String sid, string authtoken,string NewPassword){
			RestClient client = new RestClient (Account.baseurl+"Accounts/"+sid+"/Clients/"+Properties.Sid);
			RestRequest request = new RestRequest (Method.PUT);
			client.Authenticator = new HttpBasicAuthenticator (sid, authtoken);
			request.AddParameter ("Password", NewPassword);
			client.Execute (request);


		}
	}
	public class MakeClient{
		RestClient client;
		RestRequest request;
		public MakeClient(RestClient client,RestRequest request){
			this.client = client;
			this.request = request;
		}
		public void AddParameter(string ParameterName,string ParameterValue){
			request.AddParameter (ParameterName, ParameterValue);
		}
		public Client Create(){

			IRestResponse response = client.Execute (request);
			return new Client (response.Content);

		}

	}



}

