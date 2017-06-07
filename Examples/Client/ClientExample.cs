using System;
using System.Collections.Generic;
using RestComm;

	class MainClass
{
		public static void Main (string[] args)
	{
			//Login 
		var akount = new Account ("Sid", "AuthToken");

			//Creates a client
		Client client=akount.makeclient("DemoClient","Demo@1234").Create();

			Console.WriteLine (client.Properties.Sid);
			
		//Gets list of all client
		List<Client> clientlist=akount.GetClientList();
		foreach (Client c in clientlist) 
		{
			Console.WriteLine (c.Properties.FriendlyName);

		}

		//Changes password of client
		client.ChangePassword(akount.Properties.Sid,akount.Properties.authtoken,"Demo@123");


		//Deletes client
		client.Delete(akount.Properties.Sid,akount.Properties.authtoken);


	}
}

