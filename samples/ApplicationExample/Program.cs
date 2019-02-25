using System;
using System.Collections.Generic;
using org.restcomm.connect.sdk.dotnet;
class MainClass
{
    public static void Main(string[] args)
    {
        //Login 
        var akount = new Account("Account sid here ", "Authentication_Token", "https://restcomm_ip/restcomm/2012-04-24/");
        Console.WriteLine("logged in");
        //Creates application
        Application app = akount.CreateApplication("testappps");

        Console.WriteLine(app.Properties.sid);

        //Get list of all application
        List<Application> applist = akount.GetApplicationList();
        //prints name of all 
        foreach (Application a in applist)
        {
            Console.WriteLine(a.Properties.friendly_Name);
        }
        //deletes application 
        app.Delete();
        Console.WriteLine("application deleted");
        Console.ReadLine();

    }
}

