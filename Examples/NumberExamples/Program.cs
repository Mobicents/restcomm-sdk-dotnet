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
            //   Account akount = new Account("Enter your sid here", "Enter your authtoken here", "https://cloud.restcomm.com/restcomm/2012-04-24/");
            Account akount = new Account("AC13b4372c92ed5c07d951cf842e2664ff", "cb0936cfee986d3e3ec6d1d77cc57888", "https://cloud.restcomm.com/restcomm/2012-04-24/");
            var phonenumberlist=akount.SearchPhoneNumbers("US").Search();
            foreach(PhoneNumber number in phonenumberlist)
            {
                Console.WriteLine(number.Properties.friendlyName);

            }
            Console.ReadLine();
        }
    }
}
