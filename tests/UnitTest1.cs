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
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using org.restcomm.connect.sdk.dotnet;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;



namespace Test
{//In Credentials.txt(Project/bin/Debug)
 //First Line : Account Sid
 //Second Line :Authentication Token
 //Third Line :Login Password
    [TestFixture]
    public class NUnitTestClass
    {
#pragma warning disable

        string Sid = "test";
        string authtoken = "testpassword";

        Account akount;
        string baseurl;
        string loginresponse= "{\n  \"sid\": \"test\",\n  \"friendly_name\": \"DummyAccount\",\n  \"email_address\": \"account@localhost.com\",\n  \"type\": \"Full\",\n  \"status\": \"active\",\n  \"role\": \"Administrator\",\n  \"date_created\": \"Tue, 16 May 2017 07:20:44 +0000\",\n  \"date_updated\": \"Mon, 12 Jun 2017 11:45:43 +0000\",\n  \"auth_token\": \"testpassword\",\n  \"uri\": \"/2012-04-24/Accounts.json/AC43b4d94a9b2\",\n  \"subresource_uris\": {\n    \"available_phone_numbers\": \"/2012-04-24/Accounts/AC43b4d94a9b2/AvailablePhoneNumbers.json\",\n    \"calls\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Calls.json\",\n    \"conferences\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Conferences.json\",\n    \"incoming_phone_numbers\": \"/2012-04-24/Accounts/AC43b4d94a9b2/IncomingPhoneNumbers.json\",\n    \"notifications\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Notifications.json\",\n    \"outgoing_caller_ids\": \"/2012-04-24/Accounts/AC43b4d94a9b2/OutgoingCallerIds.json\",\n    \"recordings\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Recordings.json\",\n    \"sandbox\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Sandbox.json\",\n    \"sms_messages\": \"/2012-04-24/Accounts/AC43b4d94a9b2/SMS/Messages.json\",\n    \"transcriptions\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Transcriptions.json\"\n  }\n}";
        string changepasswordresponse = "{\n  \"sid\": \"test\",\n  \"friendly_name\": \"DummyAccount\",\n  \"email_address\": \"account@localhost.com\",\n  \"type\": \"Full\",\n  \"status\": \"active\",\n  \"role\": \"Administrator\",\n  \"date_created\": \"Tue, 16 May 2017 07:20:44 +0000\",\n  \"date_updated\": \"Mon, 12 Jun 2017 11:45:43 +0000\",\n  \"auth_token\": \"newpassword\",\n  \"uri\": \"/2012-04-24/Accounts.json/AC43b4d94a9b2\",\n  \"subresource_uris\": {\n    \"available_phone_numbers\": \"/2012-04-24/Accounts/AC43b4d94a9b2/AvailablePhoneNumbers.json\",\n    \"calls\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Calls.json\",\n    \"conferences\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Conferences.json\",\n    \"incoming_phone_numbers\": \"/2012-04-24/Accounts/AC43b4d94a9b2/IncomingPhoneNumbers.json\",\n    \"notifications\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Notifications.json\",\n    \"outgoing_caller_ids\": \"/2012-04-24/Accounts/AC43b4d94a9b2/OutgoingCallerIds.json\",\n    \"recordings\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Recordings.json\",\n    \"sandbox\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Sandbox.json\",\n    \"sms_messages\": \"/2012-04-24/Accounts/AC43b4d94a9b2/SMS/Messages.json\",\n    \"transcriptions\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Transcriptions.json\"\n  }\n}";
        string callresponse = "{\n  \"sid\": \"testcall\",\n  \"InstanceId\": \"ID189\",\n  \"date_created\": \"Mon, 19 Jun 2017 17:55:34 +0000\",\n  \"date_updated\": \"Mon, 19 Jun 2017 17:55:34 +0000\",\n  \"account_sid\": \"AC43b4d94a9b2\",\n  \"to\": \"To\",\n  \"from\": \"From\",\n  \"status\": \"QUEUED\",\n  \"start_time\": \"2017-06-19T17:55:34.000Z\",\n  \"price_unit\": \"USD\",\n  \"direction\": \"outbound-api\",\n  \"api_version\": \"2012-04-24\",\n  \"caller_name\": \"From\",\n  \"uri\": \"/2012-04-24/Accounts/A1862583c8638b26f2/Calls/CA57c2cb6154854e73b.json\",\n  \"subresource_uris\": {\n    \"notifications\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Calls/CA57c2cb6154854e73be/Notifications.json\",\n    \"recordings\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Calls/CA57c2cb6154854587ef/Recordings.json\"\n  }\n}";
        string clientresponse = "{\n  \"sid\": \"dummyclient\",\n  \"date_created\": \"Mon, 19 Jun 2017 19:50:19 +0000\",\n  \"date_updated\": \"Mon, 19 Jun 2017 19:50:19 +0000\",\n  \"account_sid\": \"AC43b4d94a9b2b1862583c8638b70d26f2\",\n  \"api_version\": \"2012-04-24\",\n  \"friendly_name\": \"DemoClient\",\n  \"login\": \"DemoClient\",\n  \"password\": \"Demo@1234\",\n  \"status\": \"1\",\n  \"voice_method\": \"POST\",\n  \"voice_fallback_method\": \"POST\",\n  \"uri\": \"/2012-04-24/Accounts/AC43b4d94a9b2b1862583c8638b70d26f2/Clients/CL0e6aa31b0c36401ea5abd728187f3623.json\"\n}";
        string appresponse = "{\n  \"sid\": \"dummyapp\",\n  \"date_created\": \"Mon, 19 Jun 2017 20:45:35 +0000\",\n  \"date_updated\": \"Mon, 19 Jun 2017 20:45:35 +0000\",\n  \"friendly_name\": \"testappps\",\n  \"account_sid\": \"AC43b4d94a9b2\",\n  \"api_version\": \"2012-04-24\",\n  \"voice_caller_id_lookup\": false,\n  \"uri\": \"/2012-04-24/Accounts/AC43b4d94a9b2/Applications/APdb12dfe7961d4a1b8e6b.json\"\n}";
        string emailresponse = "{\n  \"date_sent\": \"2017-06-19T21:04:56.040Z\",\n  \"account_sid\": \"AC43b4d94a9b2\",\n  \"from\": \"demo123@localhost.com\",\n  \"to\": \"demo345@localhost.com\",\n  \"body\": \"This is a test email\",\n  \"subject\": \"Test\"\n}";
        string numberresponse = "[{ \"friendlyName\": \"+12034848530\", \"phoneNumber\": \"12034848530\", \"isoCountry\": \"US\", \"cost\": \"0.67\", \"voiceCapable\": \"true\", \"smsCapable\": \"true\" }]";
        string notificationresponse = "[\n    {\n      \"sid\": \"NOa6b821987c1e47b4b91d2678fdndjdn\",\n      \"date_created\": \"Wed, 17 May 2017 11:09:40 +0000\",\n      \"date_updated\": \"Wed, 17 May 2017 11:09:40 +0000\",\n      \"account_sid\": \"AC43b4d94a9b2\",\n      \"api_version\": \"2012-04-24\",\n      \"log\": 0,\n      \"error_code\": 11001,\n      \"more_info\": \"/restcomm/errors/11001.html\",\n      \"message_text\": \"Cannot Connect to Client: bob : Make sure the Client exist or is registered with Restcomm\",\n      \"message_date\": \"2017-05-17T11:09:40.000Z\",\n      \"request_url\": \"\",\n      \"request_method\": \"\",\n      \"request_variables\": \"\",\n      \"uri\": \"/2012-04-24/Accounts/AC13b4372c/Notifications/NOa6b82198.json\"\n    }\n   ]";
        string SMSresponse = "[\n    {\n      \"sid\": \"SMade2570e7f554578a\",\n      \"date_created\": \"Wed, 28 Jun 2017 06:30:32 +0000\",\n      \"date_updated\": \"Wed, 28 Jun 2017 06:30:32 +0000\",\n      \"account_sid\": \"AC13b4372c92\",\n      \"from\": \"+1654123987\",\n      \"to\": \"+1321654879\",\n      \"body\": \"This is a test message\",\n      \"status\": \"sending\",\n      \"direction\": \"outbound-api\",\n      \"price\": \"0\",\n      \"price_unit\": \"USD\",\n      \"api_version\": \"2012-04-24\",\n      \"uri\": \"/2012-04-24/Accounts/AC13b4372c92ed5c07d951cf842e2664ff/SMS/Messages/SMade2570e7f554578ac590311085f53e2.json\"\n    }]";
        string transcriptionResponse = " {\"page\":0,\"num_pages\":0,\"page_size\":50,\"total\":13,\"start\":\"0\",\"end\":\"13\",\"uri\":\"/restcomm/2012-04-24/Accounts/ACae6e420f425248d6a26948c17a9e2acf/Transcriptions.json\",\"first_page_uri\":\"/restcomm/2012-04-24/Accounts/ACae6e420f425248d6a26948c17a9e2acf/Transcriptions.json?Page=0&PageSize=50\",\"previous_page_uri\":\"null\",\"next_page_uri\":\"null\",\"last_page_uri\":\"/restcomm/2012-04-24/Accounts/ACae6e420f425248d6a26948c17a9e2acf/Transcriptions.json?Page=0&PageSize=50\",\"transcriptions\":\n    [\n        {\n            \"sid\": \"RF20000000000000000000000000000001\",\n            \"date_created\":\"Wed, 30 Oct 2013 16:28:33 +0900\",\n            \"date_updated\":\"Wed, 30 Oct 2013 16:28:33 +0900\",\n            \"account_sid\":\"ACae6e420f425248d6a26948c17a9e2acf\",\n            \"status\":\"completed\",\n            \"recording_sid\":\"CA5FB00000000000000000000000000002\",\n            \"duration\":\"14.70275\",\n            \"transcription_text\":\"Hello, Welcome to RestComm Connect\",\n            \"price\":\"0.0\",\n            \"uri\":\"/2012-04-24/Accounts/ACae6e420f425248d6a26948c17a9e2acf/Transcriptions/NOb88ccff6c9e04f989de9415a555ad84d.json.json\"\n        }\n    ]\n}";
        string conferenceResponse = "{\n  \"page\": 0,\n  \"num_pages\": 2,\n  \"page_size\": 50,\n  \"total\": 123,\n  \"start\": \"0\",\n  \"end\": \"49\",\n  \"uri\": \"/2012-04-24/Accounts/AC23f1b11bbb/Conferences.json\",\n  \"first_page_uri\": \"/2012-04-24/Accounts/AC23f1b11bbb/Conferences.json?Page=0&PageSize=50\",\n  \"previous_page_uri\": \"null\",\n  \"next_page_uri\": \"/2012-04-24/Accounts/AC23f1b11bbb/Conferences.json?Page=1&PageSize=50&AfterSid=CF5f25a49df5844\",\n  \"last_page_uri\": \"/2012-04-24/Accounts/AC23f1b11bbb/Conferences.json?Page=2&PageSize=50\",\n  \"conferences\": [\n    {\n      \"sid\": \"CF00a13f9e9\",\n      \"date_created\": \"Thu, 21 Jul 2016 13:02:45 +0000\",\n      \"date_updated\": \"Thu, 21 Jul 2016 13:02:52 +0000\",\n      \"account_sid\": \"AC23f1b11bbb\",\n      \"status\": \"FORCED_COMPLETED\",\n      \"api_version\": \"2012-04-24\",\n      \"friendly_name\": \"amits-conf\",\n      \"uri\": \"/2012-04-24/Accounts/AC23f1b11bbb/Conferences/CF00a13f9e9.json\",\n      \"subresource_uris\": {\n        \"participants\": \"/2012-04-24/Accounts/AC23f1b11bbb/Conferences/CF00a13f9e9/Participants.json\"\n      }\n    }\n]";
        string participantsResponse = "{\n [\n {\n \"sid\": \"CA04a5c14f4ccc\",\n \"date_created\": \"Fri, 30 Jun 2017 11:52:10 +0000\",\n \"date_updated\": \"Fri, 30 Jun 2017 11:52:15 +0000\",\n \"account_sid\": \"AC23f1b11bbb\",\n \"muted\": false,\n \"hold\": false,\n \"start_conference_on_enter\": true,\n \"end_conference_on_enter\": false,\n \"uri\": \"/2012-04-24/Accounts/AC23f1b11bbb/Calls/CA04a5c14f4ccc.json\"\n }\n ]\n}";
        [SetUp]
       public void Login()
        {
            //MockServer.AddGetRequest
            

                baseurl = MockServer.hostaddress + "restcomm/2012-04-24/";
                MockServer.AddAuthentication(Sid, authtoken);
                MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts.json/" + Sid, loginresponse);
                Thread.Sleep(20);
          
               akount = new Account(Sid, authtoken, baseurl);

            
        }
        [Test]
        public void AccountLogin()
        {
            Assert.AreEqual( "test", akount.Properties.sid);

        }
        [Test]
        public void ChangePassword()
        {

            var paradictionary = new Dictionary<string, string>();
            paradictionary.Add("Password", "newpassword");
            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts.json/" + Sid, paradictionary, changepasswordresponse);
            string oldauthtoken = akount.Properties.auth_token;
                akount.ChangePassword("newpassword");

                string newauthtoken = akount.Properties.auth_token;
                Assert.AreNotEqual(newauthtoken, oldauthtoken);

            
        }

        	[Test]
        public void  CreateSubAccount()
        {
            var paradictionary = new Dictionary<string, string>();
            paradictionary.Add("FriendlyName", "password");
            paradictionary.Add("EmailAddress", "b");
            paradictionary.Add("Password", "c");

            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts.json/", paradictionary, loginresponse);
            
            SubAccount a = akount.CreateSubAccount ("password", "b", "c");
          
            Assert.IsNotNull(a.Properties.friendly_name);
            }

          
            [Test]
            public void SubAccountListing(){
            string subaccountlistresponse = "[" + loginresponse + "]";
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts.json", subaccountlistresponse);
                List<SubAccount> subaccountlist= akount.GetSubAccountList ();
                    Assert.AreEqual (subaccountlist[0].Properties.sid, "test");
                }
        
        [Test]
        public void MakeCall()
        {
        
            var paradictionary = new Dictionary<string, string>();
            paradictionary.Add("From", "Client1");
           paradictionary.Add("To", "Client2");
            paradictionary.Add("Url", "site.com");
            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts/" +akount.Properties.sid + "/Calls.json", paradictionary, callresponse);
            
           Call calldetail= akount.MakeCall("Client1", "Client2", "site.com").call();
            Assert.AreEqual("testcall", calldetail.Properties.sid);
        }
        [Test]
        public void GetCallDetail()
        {
            string calldetailresponse = "[" + callresponse + "]";
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/"+akount.Properties.sid+"/Calls.json", calldetailresponse);
           List<Call> calllist= akount.GetCallDetail().Search();
            Assert.AreEqual(1, calllist.Count);
        }
        [Test]
        public void CreateClient()
        {
            var paradictionary = new Dictionary<string, string>();
            paradictionary.Add("Login", "username");
            paradictionary.Add("Password", "password");
            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Clients.json", paradictionary, clientresponse);
           Client c= akount.makeclient("username", "password").Create();
            Assert.AreEqual("dummyclient", c.Properties.sid);
        }
        [Test]
        public void ClientList()
        {
        string    clientlistresponse = "["+clientresponse+ "]";
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Clients.json",clientlistresponse);
            List<Client> clientlist = akount.GetClientList();
            Assert.AreEqual(1, clientlist.Count);
        }
        [Test]
        public void makeapp()
        {
            var paradictionary = new Dictionary<string, string>();
            paradictionary.Add("FriendlyName", "appname");
            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Applications.json", paradictionary, appresponse);
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Applications" + "/dummyapp.json", appresponse);
            Application a= akount.CreateApplication("appname");
            Assert.AreEqual("dummyapp", a.Properties.sid);

        }
        [Test]
        public void applist()
        {
            string applistresponse = "[" + appresponse + "]";
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Applications.json", applistresponse);
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Applications"+"/dummyapp.json", appresponse);
            List<Application> a = akount.GetApplicationList();
            Assert.AreEqual(1, a.Count);
        }
        [Test]
        public void SendMail()
        {
            var paradictionary = new Dictionary<string, string>();
            paradictionary.Add("From", "emailid1");
            paradictionary.Add("To", "emailid2");
            paradictionary.Add("Body", "emailbody");
            paradictionary.Add("Subject", "Subject");
           
            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Email/Messages.json", paradictionary, emailresponse);
            akount.SendEmail("emailid1", "emailid2", "emailbody","Subject").Send();
            //will throw a error if something goes wrong
        }
        [Test]
        public void NumberSearch()
        {
            numberresponse=numberresponse.Replace((char)39,'"');
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/AvailablePhoneNumbers/US/Local.json", numberresponse);
           var numberlist= akount.SearchPhoneNumbers("US").Search();
            Assert.AreEqual("0.67", numberlist[0].Properties.cost); 
        }
        [Test]
        public void NotificationTest()
        {
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Notifications.json",notificationresponse);
           
            var parameter = new Dictionary<string, string>();
          
            List<Notification> NotificationList = akount.GetNotificationList(parameter);
            Assert.AreEqual(NotificationList[0].Properties.log, "0");
           
        }
        [Test]
        public void SMSListingTest()
        {
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/SMS/Messages.json", SMSresponse);
            var SMSlist = akount.GetSMSList().Search();
            Assert.AreEqual(SMSlist[0].Properties.price, "0");
            
        }
        [Test]
        public void SendingSMSTest()
        {
            string newSMSresponse = SMSresponse.Split('[',']')[1];
            var parameters= new Dictionary<string, string>();
            parameters.Add("From", "From");
            parameters.Add("To","To");
            parameters.Add("Body", "Body");
            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/SMS/Messages.json",parameters, newSMSresponse);
            var newSMS = akount.SendSMS("From", "To", "Body").send();
            Assert.AreEqual(newSMS.Properties.price, "0");

        }
        [Test]
        public void TranscriptionListTest()
        {
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Transcriptions.json",transcriptionResponse);
            var transcriptionlist= akount.GetTranscriptionList().Search();
            Assert.AreEqual(transcriptionlist[0].Properties.sid, "RF20000000000000000000000000000001");

        }
        [Test]
        public void GetConferenceList()
        {
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Conferences.json", conferenceResponse);
            var conferenceList = akount.GetConferenceList();
            Assert.AreEqual(conferenceList[0].properties.api_version, "2012-04-24");
           
        }
        [Test]
        public void GetparticipantList()
        {
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Conferences.json", conferenceResponse);
            var conferencelist = akount.GetConferenceList();
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Conferences/" + conferencelist[0].properties.sid + "/Participants.json",participantsResponse);
           var participantsList= conferencelist[0].GetParticipantList(akount.Properties.sid,akount.Properties.auth_token);
            Assert.AreEqual(participantsList[0].properties.muted, "false");
        }
        [Test]
        public void MuteParticipant()
        {
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Conferences.json", conferenceResponse);
            var conferencelist = akount.GetConferenceList();
            //  Assert.AreEqual(conferenceList[0].properties.api_version, "2012-04-24");
            var newparticipantResponse = participantsResponse.Split('[', ']')[1];
            MockServer.AddGetRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Conferences/" + conferencelist [0].properties.sid + "/Participants.json",participantsResponse);
            var participantsList= conferencelist [0].GetParticipantList(akount.Properties.sid,akount.Properties.auth_token);
            Assert.AreEqual(participantsList [0].properties.muted, "false");
            var parameter = new Dictionary<string, string>();
            parameter.Add("Mute", "false");
            MockServer.AddPostRequest("/restcomm/2012-04-24/Accounts/" + akount.Properties.sid + "/Conferences/" + conferencelist[0].properties.sid + "/Participants/"+participantsList[0].properties.sid+".json",parameter, newparticipantResponse);
            bool p=participantsList[0].MuteParticipant(akount.Properties.sid, akount.Properties.auth_token, conferencelist[0].properties.sid,"false");
            Assert.AreEqual(false, p);
        }
    }
}
