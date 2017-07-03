using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.restcomm.connect.sdk.dotnet;

namespace NumberExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var akount = new Account("Account sid here ", "Authentication_Token", "https://restcomm_ip/restcomm/2012-04-24/");

            var phonenumberlist=akount.SearchPhoneNumbers("US").Search();
            foreach(PhoneNumber number in phonenumberlist)
            {
                Console.WriteLine(number.Properties.friendlyName);

            }
            Console.ReadLine();
        }
    }
}
