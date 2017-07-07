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
        /// use this to search available phone number
        /// </summary>
        /// <param name="IsoCountryCode">ISo country code eg. SOM for somalia </param>
        /// <returns> numberfilter :contains method to apply searh filter and execute search request</returns>
        public NumberFilter SearchPhoneNumbers(string IsoCountryCode)
        {

            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/AvailablePhoneNumbers/" + IsoCountryCode + "/Local.json");

            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            RestRequest req = new RestRequest(Method.GET);
            return new NumberFilter(client, req);

        }


    }
    /// <summary>
    /// contains method to apply searh filter and execute search request
    /// </summary>
    public class NumberFilter
    {
        private RestClient Client;
        private RestRequest Request;
       

        public NumberFilter(RestClient client, RestRequest request)
        {
            Client = client;
            Request = request;
        }
        /// <summary>
        /// Adds search filter to your request
        /// </summary>
        /// <param name="AvailablePhoneFilter">name of the search filter eg. "AreaCode"</param>
        /// <param name="Value">value of parameter .</param>
        public void AddSearchParameter(string AvailablePhoneFilter, string Value)
        {
            Request.AddQueryParameter(AvailablePhoneFilter, Value);
        }
       /// <summary>
       /// executes the request 
       /// </summary>
       /// <returns>search result</returns>
        public List<PhoneNumber> Search()
        {

            IRestResponse res = Client.Execute(Request);
            
            var content = res.Content;
 
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);

           
          var  Propertieslist = JsonConvert.DeserializeObject<List<numberProperties>>(content);
          
            List<PhoneNumber> phonenumberlist = new List<PhoneNumber>();
                foreach(numberProperties property in Propertieslist)
            {
                phonenumberlist.Add(new PhoneNumber(property));
            }
            return phonenumberlist;

        }





    }
    /// <summary>
    /// stores phone number info such as sid, friendly name,price
    /// </summary>
    public class PhoneNumber
    {


        public numberProperties Properties;

  
        public PhoneNumber(numberProperties properties)
        {
            Properties = properties;
        }

    }
}

