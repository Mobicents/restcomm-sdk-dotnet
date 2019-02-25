// /*
//  * TeleStax, Open Source Cloud Communications
//  * Copyright 2011-2016, Telestax Inc and individual contributors
//  * by the @Paras Kumar(parasbarnwal06@gmail.com)
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
    {
        /// <summary>
        /// use this method to intiate sending sms
        /// </summary>
        /// <param name="From">The phone number or identifier that originated this sms.</param>
        /// <param name="To">The phone number or identifier that will be the recipient of this sms.</param>
        /// <param name="Body">Type message here</param>
        /// <returns>a class sendsmsthat contains method to add parameter and execute the request </returns>
        public sendSMS SendSMS(string From, string To, string Body)
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/SMS/Messages.json");
            RestRequest request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            request.AddParameter("From", From);
            request.AddParameter("To", To);
            request.AddParameter("Body", Body);

            return new sendSMS(client, request);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>SMSFilter which contain method for filtering searh result and finally executing request</returns>
        public SMSFilter GetSMSList()
        {

            RestRequest request = new RestRequest(Method.GET);
            return new SMSFilter(request, Properties.sid, Properties.auth_token);

        }



    }
    /// <summary>
    /// SMSFilter contains method for filtering out searh result and finally executing request
    /// </summary>
    public class SMSFilter
    {
        RestClient client;
        RestRequest request;
        string Sid;
        string TokenNo;
        List<string> parametername = new List<string>();
        List<string> parametervalue = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Sid"></param>
        /// <param name="TokenNo"></param>
        public SMSFilter(RestRequest request, string Sid, string TokenNo)
        {

            this.request = request;
            this.Sid = Sid;
            this.TokenNo = TokenNo;
        }
        /// <summary>
        /// add filter to your search
        /// </summary>
        /// <param name="SMSFIlter">name of the parameter eg. "Status"</param>
        /// <param name="Value">parameter value eg. "sending" for parameter "Status" </param>
        public void AddSearchFilter(string SMSFilter, string Value)
        {
            parametername.Add(SMSFilter);
            parametervalue.Add(Value);
        }
        /// <summary>
        /// Execute the searh request
        /// </summary>
        /// <returns>list of calls </returns>
        /// 
        public List<SMS> Search()
        {
            string clienturl = Account.baseurl + "Accounts/" + Sid + "/SMS/Messages.json";
            int i = 0;
            foreach(string parameter in parametername)
            {
                request.AddQueryParameter(parameter, parametervalue[i]);
                i++;
            }

            client = new RestClient(clienturl);    
            client.Authenticator = new HttpBasicAuthenticator(Sid, TokenNo);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            
            content = "[" + content.Split('[', ']')[1] + "]";
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
            var SMSproperties = JsonConvert.DeserializeObject<List<SMSProperties>>(content);
            List<SMS> SMSlist = new List<SMS>();
            foreach (SMSProperties c in SMSproperties)
            {
                SMSlist.Add(new SMS(c));
            }
            return SMSlist;
            
        }
        

    }


    public class sendSMS
    {

        RestClient Client;
        RestRequest Request;
        public sendSMS(RestClient client, RestRequest request)
        {
            Client = client;

            Request = request;

        }
        /// <summary>
        /// adds parameter to the request
        /// </summary>
        /// <param name="ParameterName">Name of the parameter </param>
        /// <param name="ParameterValue">value of the given parameter</param>
        public void AddParameter(string ParameterName, string ParameterValue)
        {

            Request.AddParameter(ParameterName, ParameterValue);

        }
        /// <summary>
        /// Executes the call request
        /// </summary>
        /// <returns>Call </returns>
        public SMS send()
        {
            IRestResponse response = Client.Execute(Request);
            var content = response.Content;
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
            SMSProperties properties =JsonConvert.DeserializeObject<SMSProperties>(content);
            SMS newSMS = new SMS(properties);
              return newSMS;
        }
    }
    /// <summary>
    /// Stores all info of the SMS with given sid
    /// </summary>
    public class SMS
    {

        public SMSProperties Properties;
        public SMS(SMSProperties properties)
        {
            Properties = properties;
        }
        
       
       

    }


   


}
