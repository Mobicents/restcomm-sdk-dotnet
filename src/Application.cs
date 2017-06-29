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
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace org.restcomm.connect.sdk.dotnet
{
    //this class contains all Acccount info and methods .
    public partial class Account
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns a list of application associated with this account</returns>
        public List<Application> GetApplicationList()
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Applications.json");
            RestRequest login = new RestRequest(Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            IRestResponse response = client.Execute(login);

            string content = response.Content;

            List<string> Sidlist = content.GetPropertiesJson("sid");
            if (Sidlist != null)
            {
                List<Application> applist = new List<Application>();

                foreach (string s in Sidlist)
                {

                    applist.Add(new Application(Properties.sid, Properties.auth_token, s));

                }


                return applist;
            }
            else
                return null;

        }

        /// <summary>
        /// Creates a new application .
        /// </summary>
        /// <param name="FriendlyName">Friendly Name of the application</param>
        /// <param name="ApiVersion">Api Version(optional)</param>
        /// <param name="HasVoiceCallerIdLookup">If true look up the caller’s caller-ID name from the CNAM database.  </param>
        /// <param name="RcmlUrl">The HTTP address that RestComm will use to get the RCML of this Application.</param>
        /// <param name="Kind">The kind of this Application. (Supported values: voice, sms or ussd)</param>
        /// <returns></returns>
        public Application CreateApplication(string FriendlyName, string ApiVersion = null, bool HasVoiceCallerIdLookup = false, string RcmlUrl = null, String Kind = null)
        {

            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Applications.json");
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            RestRequest sendreq = new RestRequest(Method.POST);
            sendreq.AddParameter("FriendlyName", FriendlyName);
            if (ApiVersion != null)
                sendreq.AddParameter("ApiVersion", ApiVersion);
            if (HasVoiceCallerIdLookup != false)
                sendreq.AddParameter("HasVoiceCallerIdLookup", "true");
            if (RcmlUrl != null)
                sendreq.AddParameter("RcmlUrl", RcmlUrl);
            if (Kind != null)
            {
                sendreq.AddParameter("Kind", Kind);
            }

            IRestResponse response = client.Execute(sendreq);
         
            return new Application(Properties.sid, Properties.auth_token, response.Content.GetPropertyJson("sid"));

        }
       


    }

    /// <summary>
    /// Application Class
    /// </summary>
    public class Application
    {
        string auth_token;
        /// <summary>
        /// This struct stores all the info related to the given application
        /// </summary>
        public applicationProperties Properties;
        /// <summary>
        /// Stores application info in the class
        /// </summary>
        /// <param name="accountsid">sid of account associated with this application</param>
        /// <param name="tokenno">authentication token of the account </param>
        /// <param name="ApplicationSid">Sid of this application</param>
        public Application(String accountsid, String tokenno, string ApplicationSid)
        {
            auth_token = tokenno;
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + accountsid + "/Applications/" + ApplicationSid+".json");
            RestRequest login = new RestRequest(Method.GET);

            client.Authenticator = new HttpBasicAuthenticator(accountsid, tokenno);

            IRestResponse response = client.Execute(login);
            var content = response.Content;

           
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
            Properties = JsonConvert.DeserializeObject<applicationProperties>(content);
        }
        /// <summary>
        /// Changes the old parameter value with the given value
        /// </summary>
        /// <param name="parameters">A dictionary with key=parameter type ,value=parameter value</param>
        public void Update(Dictionary<string,string> parameters)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + Properties.account_sid + "/Applications/" + Properties.sid +Properties.sid+ ".json");
            RestRequest login = new RestRequest(Method.POST);

            foreach (var pair in parameters)
            {
                login.AddParameter(pair.Value,pair.Value);
            }
            IRestResponse response = client.Execute(login);
            var content = response.Content;
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
            Properties = JsonConvert.DeserializeObject<applicationProperties>(content);

        }
        /// <summary>
        /// Deletes Application 
        /// </summary>
        public void Delete()
        {

            RestClient client = new RestClient(Account.baseurl + "Accounts/" + Properties.account_sid + "/Applications/" + Properties.sid+".json");
            RestRequest login = new RestRequest(Method.DELETE);
            client.Authenticator = new HttpBasicAuthenticator(Properties.account_sid, auth_token);
            client.Execute(login);
          

        }
       

    }

}

