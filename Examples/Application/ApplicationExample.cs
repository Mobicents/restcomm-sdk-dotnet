using System;
using System.Collections.Generic;
using RestComm;
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Login 
		Account akount = new Account ("Sid", "AuthToken");

			//Creates application
			Application app=akount.CreateApplication("demoapp");

		Console.WriteLine(app.Properties.Sid);

		//Get list of all application
		List<Application> applist=akount.GetApplicationList();
		//prints name of all 
		foreach (Application a in applist) {
			Console.WriteLine (a.Properties.FriendlyName);
			}
		//deletes application 
		app.Delete();

		}
	}

