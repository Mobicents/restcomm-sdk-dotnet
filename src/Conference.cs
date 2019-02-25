using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

using RestSharp;
using RestSharp.Authenticators;
using System.Text.RegularExpressions;

namespace org.restcomm.connect.sdk.dotnet
{
    public partial class Account
    {
        public List<Conference> GetConferenceList()
        {
            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Conferences.json");

            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            RestRequest req = new RestRequest(Method.GET);
            var content = client.Execute(req).Content;
            content = "[" + content.Split('[', ']')[1] + "]";
            List<conferenceProperties> properties = JsonConvert.DeserializeObject<List<conferenceProperties>>(content);
            List<Conference> conferencelist = new List<Conference>();
            foreach(conferenceProperties property in properties)
            {
                conferencelist.Add(new Conference(property));
            }

            return conferencelist;
        }
    }
   public class Conference
    {
        public conferenceProperties properties;
        public Conference(conferenceProperties properties)
        {
            this.properties = properties;
        }
        public List<Participants> GetParticipantList(string account_sid,string auth_token)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + account_sid + "/Conferences/"+this.properties.sid+"/Participants.json");

            client.Authenticator = new HttpBasicAuthenticator(account_sid, auth_token);
            RestRequest req = new RestRequest(Method.GET);
            var content = client.Execute(req).Content;
           
            content = "[" + content.Split('[', ']')[1] + "]";
            List<participantProperties> properties = JsonConvert.DeserializeObject<List<participantProperties>>(content);
            var participantlist = new List<Participants>();
            foreach (var property in properties)
            {
                participantlist.Add(new Participants(property));
            }
            return participantlist;
        }
        
       

    }
    public class Participants
    {

        public participantProperties properties;
        public Participants(participantProperties properties)
        {
            this.properties = properties;
        }
        public  bool MuteParticipant(string Account_Sid,string Auth_Token,string Conference_sid,string mute="true")
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + Account_Sid + "/Conferences/" + Conference_sid + "/Participants/"+properties.sid+".json");

            client.Authenticator = new HttpBasicAuthenticator(Account_Sid, Auth_Token);
            RestRequest req = new RestRequest(Method.POST);
            req.AddParameter("Mute", mute);
            var content = client.Execute(req).Content;
          
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
            properties = JsonConvert.DeserializeObject<participantProperties>(content);
            if (properties.muted == "true")
                return true;
            else return false;
            
        }
        
    }
   
}
