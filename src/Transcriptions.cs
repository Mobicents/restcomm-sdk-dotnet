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
        /// A transcription is a text version of a recording produced using automatic speech recognition.
        /// </summary>
        /// <returns> TranscriptionsFilter :contains method to apply searh filter and execute search request</returns>
        public TranscriptionsFilter GetTranscriptionList()
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Transcriptions.json");

            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            RestRequest req = new RestRequest(Method.GET);
            return new TranscriptionsFilter(client, req);

        }
            

    }
    /// <summary>
    /// contains method to apply search filter and execute search request
    /// </summary>
    public class TranscriptionsFilter
    {
        private RestClient Client;
        private RestRequest Request;


        public TranscriptionsFilter(RestClient client, RestRequest request)
        {
            Client = client;
            Request = request;
        }
        /// <summary>
        /// Adds search filter to your request
        /// </summary>
        /// <param name="NotificationFilter">name of the search filter eg. "AreaCode"</param>
        /// <param name="Value">value of parameter .</param>
        public void AddSearchParameter(string NotificationFilter, string Value)
        {
            Request.AddQueryParameter(NotificationFilter, Value);
        }
        /// <summary>
        /// executes the request 
        /// </summary>
        /// <returns>search result</returns>
        public List<Transcription> Search()
        {

            IRestResponse res = Client.Execute(Request);
            
            var content = res.Content;
            content ="["+ content.Split('[', ']')[1]+"]";
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);

          
            var Propertieslist = JsonConvert.DeserializeObject<List<transcriptionProperties>>(content);

            List<Transcription> TranscriptionList = new List<Transcription>();
            foreach (transcriptionProperties property in Propertieslist)
            {
                TranscriptionList.Add(new Transcription(property));
            }
            return TranscriptionList;

        }





    }
    /// <summary>
    /// stores Transcription info such as sid,price
    /// </summary>
    public class Transcription
    {


        public transcriptionProperties Properties;


        public Transcription(transcriptionProperties properties)
        {
            Properties = properties;
        }

    }
}

