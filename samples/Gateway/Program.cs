using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.restcomm.connect.sdk.dotnet;

namespace Gateway
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("account_sid", "auth_token","baseurl here");
            var gatewaylist=account.GetGatewayList();
            foreach(var gateway in gatewaylist)
            {
                Console.WriteLine(gateway.Properties.friendly_name);
            }
            var newgateway= account.CreateGateway("MyGateway", "DefaultUser", "Password@123", "my.gateway.com", "true", "3600");
            Console.ReadLine();
        }
    }
}
