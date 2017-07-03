using System;
using System.Collections.Generic;
using org.restcomm.connect.sdk.dotnet;

namespace NotificationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var akount = new Account("Account sid here ", "Authentication_Token", "https://restcomm_ip/restcomm/2012-04-24/");
            var parameter = new Dictionary<string, string>();
            parameter.Add("EndTime", "2017-06-02");
            List<Notification> NotificationList=akount.GetNotificationList(parameter);
            foreach(Notification n in NotificationList)
            {
                Console.WriteLine(n.Properties.message_text);
            }
            Console.ReadLine();
        }
    }
}
