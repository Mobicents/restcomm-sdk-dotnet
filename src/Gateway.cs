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
namespace org.restcomm.connect.sdk.dotnet
{
    public partial class Account
    {
        /// <summary>
        ///didnt have sample response so could
        /// </summary>
        /// <returns></returns>
        public List<Gateway> GetGatewayList()
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Management/Gateways.json");
            RestRequest request = new RestRequest(Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            content = content.Split('[', ']')[1];
            var gatewayplist = new List<gatewayproperties>();
            gatewayplist = JsonConvert.DeserializeObject<List<gatewayproperties>>(content);
            var gatewaylist = new List<Gateway>();
            foreach (gatewayproperties Property in gatewayplist)
            {
                gatewaylist.Add(new Gateway(Property));
            }
            return gatewaylist;

        }
        public string CreateGateway(string FriendlyName,string UserName,string Password,string Proxy,string Register,string TTL)
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Management/Gateways.json");
            RestRequest request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            request.AddParameter("FriendlyName", FriendlyName);
            request.AddParameter("UserName", UserName);
            request.AddParameter("Password", Password);
            request.AddParameter("Proxy", Proxy);
            request.AddParameter("Register", Register);
            request.AddParameter("TTL", TTL);
        IRestResponse response = client.Execute(request);
        return response.Content;
        }


    }
    public class Gateway
    {
        public gatewayproperties Properties;

        public Gateway(gatewayproperties properties)
        {
            this.Properties = properties;
          
        }
        public void Update(Dictionary<string,string> parameter,string account_sid,string auth_token)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + auth_token + "/Management/Gateways/"+Properties.sid+".json");
            RestRequest request = new RestRequest(Method.POST);
            client.Authenticator=new HttpBasicAuthenticator(account_sid, auth_token);

            foreach (var pair in parameter)
            {
                request.AddParameter(pair.Key, pair.Value);
            }

           var content =client.Execute(request).Content;
            Properties = JsonConvert.DeserializeObject<gatewayproperties>(content);
        }
        public void Delete(string account_sid,string auth_token)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + auth_token + "/Management/Gateways" + Properties.sid+".json");
            RestRequest request = new RestRequest(Method.DELETE);
            client.Authenticator = new HttpBasicAuthenticator(account_sid, auth_token);
            var content = client.Execute(request);
        }
    }
}

