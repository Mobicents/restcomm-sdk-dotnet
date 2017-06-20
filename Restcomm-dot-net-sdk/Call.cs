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
using Newtonsoft.Json;

//Not complete
namespace org.restcomm.connect.sdk.dotnet
{
    //In 
    public partial class Account
    {
        public makecall MakeCall(string From, string To, string Url)
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.Sid + "/Calls.json");
            RestRequest makecall = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(Properties.Sid, Properties.authtoken);
            makecall.AddParameter("From", From);
            makecall.AddParameter("To", To);
            makecall.AddParameter("Url", Url);

            return new makecall(client, makecall);

        }
        public CallFilter GetCallDetail()
        {

            RestRequest makecall = new RestRequest(Method.GET);
            return new CallFilter(makecall, Properties.Sid, Properties.authtoken);

        }



    }
    public class CallFilter
    {
        RestClient client;
        RestRequest request;
        string Sid;
        string TokenNo;
        List<string> parametername = new List<string>();
        List<string> parametervalue = new List<string>();
        public CallFilter(RestRequest request, string Sid, string TokenNo)
        {

            this.request = request;
            this.Sid = Sid;
            this.TokenNo = TokenNo;
        }
        public void AddSearchFilter(string ParameterName, string ParameterValue)
        {
            parametername.Add(ParameterName);
            parametervalue.Add(ParameterValue);
        }
        public List<Call> Search()
        {
            string clienturl = Account.baseurl + "Accounts/" + Sid + "/Calls.json";
            if (parametername.Count != 0)
            {
                clienturl += "?";
                int i = 0;
                foreach (string s in parametername)
                {
                    if (i != 0)
                        clienturl += "&";
                    clienturl += parametername[i];
                    clienturl += "=" + parametervalue[i];
                    i++;
                }


            }

            client = new RestClient(clienturl);
            client.Authenticator = new HttpBasicAuthenticator(Sid, TokenNo);
            IRestResponse response = client.Execute(request);

            List<Call> calllist = new List<Call>();
            List<string> Sidlist = response.Content.GetPropertiesJson("sid");
            if (Sidlist != null)
            {
                int count = Sidlist.Count;
                for (int j = 0; j < count; j++)
                {

                    calllist.Add(new Call(response.Content, j));

                }
                return calllist;
            }
            return null;
        }
    }

    public class makecall
    {

        RestClient Client;
        RestRequest Request;
        public makecall(RestClient client, RestRequest request)
        {
            Client = client;

            Request = request;

        }
        public void AddParameter(string ParameterName, string ParameterValue)
        {

            Request.AddParameter(ParameterName, ParameterValue);

        }
        public Call call()
        {
            IRestResponse response = Client.Execute(Request);
           
            return new Call(response.Content);
        }


    }

    public class Call
    {

        public callProperties Properties;

        public Call(string response)
        {
            Properties.Sid = response.GetPropertyJson("sid");
            Properties.ParentCallSid = response.GetPropertyJson("parent_call_sid");
            Properties.DateCreated = response.GetPropertyJson("date_created");
            Properties.DateUpdated = response.GetPropertyJson("date_updated");
            Properties.To = response.GetPropertyJson("to");
            Properties.From = response.GetPropertyJson("from");
           // Properties.PhoneNumberSid = xmlresponse.GetAccountProperty("PhoneNumberSid");
            Properties.Status = response.GetPropertyJson("status");
            Properties.StartTime =response.GetPropertyJson("start_time"); 
            Properties.EndTime = response.GetPropertyJson("end_time");
            Properties.Duration = response.GetPropertyJson("duration");
            Properties.Price = response.GetPropertyJson("price");
            Properties.Direction = response.GetPropertyJson("direction");
            Properties.AnsweredBy = response.GetPropertyJson("answered_by");
            Properties.ApiVersion = response.GetPropertyJson("api_version");
            Properties.ForwardFrom = response.GetPropertyJson("forward_from");
            Properties.CallerName = response.GetPropertyJson("caller_name");
            Properties.Uri = response.GetPropertyJson("uri");
        }
        //use this constructor when there is list of call in response
        public Call(string response, int elementunmber)
        {

            Properties.Sid = response.GetPropertiesJson("sid")[elementunmber];

           // Properties.ParentCallSid = response.GetPropertiesJson("parent_call_sid")[elementunmber];
            Properties.DateCreated = response.GetPropertiesJson("date_created")[elementunmber];
            Properties.DateUpdated =response.GetPropertiesJson("date_updated")[elementunmber];
            Properties.To = response.GetPropertiesJson("to")[elementunmber];
            Properties.From = response.GetPropertiesJson("from")[elementunmber];
        //    Properties.PhoneNumberSid = response.GetPropertiesJson("ParentCallSid")[elementunmber];
            Properties.Status = response.GetPropertiesJson("status")[elementunmber];
            Properties.StartTime = response.GetPropertiesJson("start_time")[elementunmber];
            //Properties.EndTime = response.GetPropertiesJson("end_time")[elementunmber];
            //Properties.Duration = response.GetPropertiesJson("ring_duration")==null?null: response.GetPropertiesJson("ring_duration")[elementunmber];
            //Properties.Price = response.GetPropertiesJson("price_unit")[elementunmber];
            Properties.Direction = response.GetPropertiesJson("direction")[elementunmber];
         //   Properties.AnsweredBy = response.GetPropertiesJson("answered_by")[elementunmber];
            //Properties.ApiVersion = response.GetPropertiesJson("api_version")[elementunmber];
           // Properties.ForwardFrom = response.GetPropertiesJson("forward_from")[elementunmber];
            //Properties.CallerName = response.GetPropertiesJson("caller_name")[elementunmber];
            //Properties.Uri = response.GetPropertiesJson("uri")[elementunmber];
        }
        public void ModifyCall(string ParameterName, string NewParameterValue, String AccountSId, String AuthToken)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + AccountSId + "/Calls.json/" + Properties.Sid);
            RestRequest makecallmodification = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(AccountSId, AuthToken);
            makecallmodification.AddParameter(ParameterName, NewParameterValue);
            client.Execute(makecallmodification);
        }


    }
}

