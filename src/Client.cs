// /*
//  * TeleStax, Open Source Cloud Communications
//  * Copyright 2011-2016, Telestax Inc and individual contributors
//  * by the @Paras Kumar(parasbarnwal06@gmail.com).
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
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace org.restcomm.connect.sdk.dotnet
{

    public partial class Account
    {/// <summary>
    /// Returns a list of clients associated with the account
    /// </summary>
    /// <returns> client list</returns>
        public List<Client> GetClientList()
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Clients.json");
            RestRequest request = new RestRequest(Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            List<Client> clientslist = new List<Client>();
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
            var Propertieslist = JsonConvert.DeserializeObject<List<clientProperties>>(content);
            foreach(clientProperties properties in Propertieslist)
            {
                clientslist.Add(new Client(properties));
            }
          
            return clientslist;

        }
        /// <summary>
        /// Creates a new client
        /// </summary>
        /// <param name="Login">Username of new client</param>
        /// <param name="Password">password for new client</param>
        /// <returns>returns a class makeclient</returns>
        public MakeClient makeclient(string Login, string Password)
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Clients.json");
            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("Login", Login);
            request.AddParameter("Password", Password);
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);

            return new MakeClient(client, request);

        }

    }
    /// <summary>
    /// contains methods and info related to the given client
    /// </summary>
    public class Client
    {
        /// <summary>
        /// stores client info
        /// </summary>
        public clientProperties Properties;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties">struct which contains info of the client</param>
        public Client(clientProperties properties)
        {
            Properties = properties;
        }
    /// <summary>
    ///delets the client info 
    /// </summary>
    /// <param name="sid">Sid of the parent account</param>
    /// <param name="authtoken">authentication token of parent account</param>
        public void Delete(String sid, string authtoken)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + sid + "/Clients/" + Properties.sid+".json");
            RestRequest request = new RestRequest(Method.DELETE);
            client.Authenticator = new HttpBasicAuthenticator(sid, authtoken);
           IRestResponse response= client.Execute(request);
           
        }
        /// <summary>
        /// Creates a new password for client
        /// </summary>
        /// <param name="sid">Sid of the parent account</param>
        /// <param name="authtoken">authentication token of the parent account</param>
        /// <param name="NewPassword">new password</param>
        public void ChangePassword(String sid, string authtoken, string NewPassword)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + sid + "/Clients/" + Properties.sid+".json");
            RestRequest request = new RestRequest(Method.PUT);
            client.Authenticator = new HttpBasicAuthenticator(sid, authtoken);
            request.AddParameter("Password", NewPassword);
            client.Execute(request);


        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class MakeClient
    {
        RestClient client;
        RestRequest request;
        public MakeClient(RestClient client, RestRequest request)
        {
            this.client = client;
            this.request = request;
        }
        /// <summary>
        /// add parameter to the the request
        /// </summary>
        /// <param name="ParameterName">eg "Status" </param>
        /// <param name="ParameterValue">for parameter name "status" value "0" for disabled or value "1" for enabled</param>
        public void AddParameter(string ParameterName, string ParameterValue)
        {   
            request.AddParameter(ParameterName, ParameterValue);
        }
        /// <summary>
        /// executes the request for creating client 
        /// </summary>
        /// <returns></returns>
        public Client Create()
        {

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
            var Properties = JsonConvert.DeserializeObject<clientProperties>(content);
            return new Client(Properties);
        }

    }



}

