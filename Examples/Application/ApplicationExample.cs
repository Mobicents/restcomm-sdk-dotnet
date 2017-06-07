using System;
using System.Collections.Generic;
using RestComm;
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Login 
		Account akount = new Account ("AC13b4372c92ed5c07d951cf842e2664ff", "eff8eb1e1334884cf7dd59d3a00e687a");

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

