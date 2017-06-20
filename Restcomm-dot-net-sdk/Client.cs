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

namespace org.restcomm.connect.sdk.dotnet
{

    public partial class Account
    {
        public List<Client> GetClientList()
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.Sid + "/Clients.json");
            RestRequest request = new RestRequest(Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(Properties.Sid, Properties.authtoken);
            IRestResponse response = client.Execute(request);
            List<Client> clientslist = new List<Client>();

            List<string> sidlist = response.Content.GetPropertiesJson("sid");
            int i = sidlist.Count;
            for (int j = 0; j < i; j++)
            {
                clientslist.Add(new Client(response.Content, j));

            }
            return clientslist;

        }
        public MakeClient makeclient(string Login, string Password)
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.Sid + "/Clients.json");
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("Login", Login);
            request.AddParameter("Password", Password);
            client.Authenticator = new HttpBasicAuthenticator(Properties.Sid, Properties.authtoken);

            return new MakeClient(client, request);

        }

    }
    public class Client
    {

        public clientProperties Properties;

        public Client(string responsecontent, int clientno)
        {
            Properties.Sid = responsecontent.GetPropertiesJson("sid")[clientno];
            Properties.DateCreated = responsecontent.GetPropertiesJson("date_created")[clientno];
            Properties.DateUpdated = responsecontent.GetPropertiesJson("date_updated")[clientno];
            Properties.FriendlyName = responsecontent.GetPropertiesJson("friendly_name")[clientno];
            Properties.AccountSid = responsecontent.GetPropertiesJson("account_sid")[clientno];
            Properties.ApiVersion = responsecontent.GetPropertiesJson("api_version")[clientno];
            Properties.Login = responsecontent.GetPropertiesJson("login")[clientno];
            Properties.Password = responsecontent.GetPropertiesJson("password")[clientno];
            Properties.Status = responsecontent.GetPropertiesJson("status")[clientno];
            
            Properties.VoiceMethod = responsecontent.GetPropertiesJson("voice_method")[clientno];
           
            Properties.VoiceFallbackMethod = responsecontent.GetPropertiesJson("voice_fallback_method")[clientno];
           
            Properties.Uri = responsecontent.GetPropertiesJson("uri")[clientno];
        }
        public Client(string responsecontent)
        {
            Properties.Sid = responsecontent.GetPropertyJson("sid");
            Properties.DateCreated = responsecontent.GetPropertyJson("date_created");
            Properties.DateUpdated = responsecontent.GetPropertyJson("date_updated");
            Properties.FriendlyName = responsecontent.GetPropertyJson("friendly_name");
            Properties.AccountSid = responsecontent.GetPropertyJson("account_sid");
            Properties.ApiVersion = responsecontent.GetPropertyJson("api_version");
            Properties.Login = responsecontent.GetPropertyJson("login");
            Properties.Password = responsecontent.GetPropertyJson("password");
            Properties.Status = responsecontent.GetPropertyJson("status");
            Properties.VoiceMethod = responsecontent.GetPropertyJson("voice_method");
            Properties.VoiceFallbackMethod = responsecontent.GetPropertyJson("voice_fallback_method");
            Properties.Uri = responsecontent.GetPropertyJson("uri");
        }
        public void Delete(String sid, string authtoken)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + sid + "/Clients/" + Properties.Sid+".json");
            RestRequest request = new RestRequest(Method.DELETE);
            client.Authenticator = new HttpBasicAuthenticator(sid, authtoken);
           IRestResponse response= client.Execute(request);
           
        }
        public void ChangePassword(String sid, string authtoken, string NewPassword)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + sid + "/Clients/" + Properties.Sid+".json");
            RestRequest request = new RestRequest(Method.PUT);
            client.Authenticator = new HttpBasicAuthenticator(sid, authtoken);
            request.AddParameter("Password", NewPassword);
            client.Execute(request);


        }
    }
    public class MakeClient
    {
        RestClient client;
        RestRequest request;
        public MakeClient(RestClient client, RestRequest request)
        {
            this.client = client;
            this.request = request;
        }
        public void AddParameter(string ParameterName, string ParameterValue)
        {
            request.AddParameter(ParameterName, ParameterValue);
        }
        public Client Create()
        {

            IRestResponse response = client.Execute(request);
            
            return new Client(response.Content);
        }

    }



}

