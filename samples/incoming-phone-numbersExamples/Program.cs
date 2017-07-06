using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.restcomm.connect.sdk.dotnet;

namespace incoming_phone_numbersExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Account akount = new Account("Account_Sid", "Auth_Token", "https://cloud.restcomm.com/restcomm/2012-04-24/");
            var incomingphoneList= akount.GetIncomingPhoneNumberList();
            foreach(var phonenumber in incomingphoneList)
            {
                Console.WriteLine(phonenumber.properties.friendly_name);
            };
            akount.AddNewPhoneNumber("+123456", Type: type.TollFree);
            Console.ReadLine();
        }
    }
}
