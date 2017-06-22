using System;
using System.Collections.Generic;
using org.restcomm.connect.sdk.dotnet;

class MainClass
{
    public static void Main(string[] args)
    {
        //Login 
        //  Account akount = new Account("Enter your sid here", "Enter your authtoken here", "https://cloud.restcomm.com/restcomm/2012-04-24/");
        //Creates a client
        Account akount = new Account("AC13b4372c92ed5c07d951cf842e2664ff", "cb0936cfee986d3e3ec6d1d77cc57888", "https://cloud.restcomm.com/restcomm/2012-04-24/");
        Client client = akount.makeclient("paras121", "Demo@1234").Create();

        Console.WriteLine(client.Properties.sid);

        //Gets list of all client
        List<Client> clientlist = akount.GetClientList();
        foreach (Client c in clientlist)
        {
            Console.WriteLine(c.Properties.friendly_name);

        }

        //Changes password of client
        //  client.ChangePassword(akount.Properties.sid, akount.Properties.auth_token, "Demo@123");


        //Deletes client
        //  client.Delete(akount.Properties.sid, akount.Properties.auth_token);

        Console.ReadLine();
    }
}

