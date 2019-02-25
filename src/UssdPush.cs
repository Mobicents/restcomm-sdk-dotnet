using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
namespace org.restcomm.connect.sdk.dotnet
{
 public partial  class Account
    {
        public void SendUssdPushMessage(string from,string to,string url)
        {

            RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/UssdPush.json");

            client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
            RestRequest req = new RestRequest(Method.POST);
            req.AddParameter("From", from);
            req.AddParameter("To", to);
            req.AddParameter("Url", url);
            client.Execute(req);

        }
       
        
    }
}
