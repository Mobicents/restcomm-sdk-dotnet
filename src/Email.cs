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
namespace org.restcomm.connect.sdk.dotnet
{
    public partial class Account
    {   /// <summary>
        /// to send a new email 
        /// </summary>
        /// <param name="From">The Email address that initiated the message.</param>
        /// <param name="To">The Email address of the recipient.</param>
        /// <param name="Body">The text body of the email message.</param>
        /// <param name="Subject">The subject of the email message.</param>
        /// <returns>returns a class Email </returns>
        public Email SendEmail(string From, string To, string Body, string Subject)
        {

            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Email" + "/Messages.json");
            RestRequest request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            request.AddParameter("From", From);
            request.AddParameter("To", To);
            request.AddParameter("Body", Body);
            request.AddParameter("Subject", Subject);
            return new Email(client, request);
        }
    }
    /// <summary>
    /// contains method to send mail and add cc/bcc
    /// </summary>
    public class Email
    {

        private RestClient client;
        private RestRequest request;

        public Email(RestClient client, RestRequest request)
        {

            this.request = request;
            this.client = client;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CC">The Carbon Copy(Cc). (The list of CCed recipients is visible to all other recipients of the message.)</param>
        public void AddCC(string CC)
        {
            request.AddParameter("CC", CC);
        }
        /// <summary>
        /// The Blind Carbon Copy feature (Bcc).  (Hide addresses when sending an Email to various recipients)
        /// </summary>
        /// <param name="BCC"></param>
        public void AddBCC(string BCC)
        {
            request.AddParameter("BCC", BCC);
        }
        /// <summary>
        /// executes the request to send email
        /// </summary>
        public void Send()
        {
            IRestResponse response = client.Execute(request);
            response.Content.GetPropertyJson("from");//will throw an error if email is not sent ie. xml response is not in proper format
        }
    }
}

