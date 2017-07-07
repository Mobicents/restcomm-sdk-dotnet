using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace org.restcomm.connect.sdk.dotnet
{
    /// <summary>
    /// type of Phone Number .
    /// </summary>
    public enum type {all,Local,TollFree,Mobile }
   public partial class Account
    {
        public List<IncomingPhoneNumber> GetIncomingPhoneNumberList(Dictionary<string,string> filterlist=null,type Type=type.all)
        {
            RestClient client;
            switch ((int)Type)
            {
                case 0: client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers.json");
                    break;
                case 1: client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers/Local.json");
                    break;
                case 2: client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers/TollFree.json");
                    break;
                case 3: client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers/Mobile.json");
                    break;
                default: client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers.json");
                    break;
            }

        client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
        RestRequest req = new RestRequest(Method.GET);
            if(filterlist!=null)
            foreach (var pair in filterlist) {
                req.AddQueryParameter(pair.Key,pair.Value);
                    }
            IRestResponse response = client.Execute(req);
            var content = response.Content;
           
            content = "[" + content.Split('[', ']')[1] + "]";
            
            var Propertieslist = JsonConvert.DeserializeObject<List<incomingnumberProperties>>(content);
            var IncomingPhonenumberList = new List<IncomingPhoneNumber>();
            
            foreach (var property in Propertieslist)
            {
                IncomingPhonenumberList.Add(new IncomingPhoneNumber(property));
            }
            return IncomingPhonenumberList;
    }
        public IncomingPhoneNumber AddNewPhoneNumber(string phoneNumber,Dictionary<string, string> parameterlist = null, type Type = type.all)
        {
            RestClient client;
            switch ((int)Type)
            {
                case 0:
                    client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers.json");
                    break;
                case 1:
                    client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers/Local.json");
                    break;
                case 2: 
                    client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers/TollFree.json");
                    break;
                case 3:
                    client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers/Mobile.json");
                    break;
                default:
                    client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/IncomingPhoneNumbers.json");
                    break;
            }
            RestRequest req = new RestRequest(Method.POST);
            req.AddParameter("PhoneNumber", phoneNumber);
            if(parameterlist!=null)
            foreach(var parameter in parameterlist)
            {
                req.AddParameter(parameter.Key, parameter.Value);
            }
            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            var content = client.Execute(req).Content;
            content = Regex.Replace(content, @"[^\u0000-\u007F]+", string.Empty);
           
            var newincomingPNproperty = JsonConvert.DeserializeObject<incomingnumberProperties>(content);
            return new IncomingPhoneNumber(newincomingPNproperty);
        }

    }
    
    public class IncomingPhoneNumber
    {
        public incomingnumberProperties properties;
        public IncomingPhoneNumber(incomingnumberProperties properties)
        {
            this.properties = properties;
        }
        public void Delete(string auth_token)
        {
            RestClient client = new RestClient(Account.baseurl + "Accounts/" + properties.account_sid + "/IncomingPhoneNumbers/"+properties.sid+".json");
            RestRequest req = new RestRequest(Method.DELETE);
            client.Authenticator=new HttpBasicAuthenticator(properties.account_sid, auth_token);
            client.Execute(req);
        }
    }
}
