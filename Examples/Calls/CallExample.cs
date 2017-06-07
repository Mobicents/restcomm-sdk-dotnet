using System;
using RestComm;
using System.Collections.Generic;

namespace Calls
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			//Login 
			Account akount = new Account ("AC13b4372c92ed5c07d951cf842e2664ff", "eff8eb1e1334884cf7dd59d3a00e687a");

			//Makes call
			var  OutBCall= akount.MakeCall("From","To","http://cloud.restcomm.com/restcomm/demos/hello-play.xml");
			//adds parameter in call
			OutBCall.AddParameter ("Timeout", "15");
			Call call= OutBCall.call ();

			//Prints call status
			Console.WriteLine(call.Properties.Status);

			//Gets list of all call
			List<Call> calllist=akount.GetCallDetail().Search();

			//Prints the name of client recieving call
			foreach (Call c in calllist) {

				Console.WriteLine (c.Properties.To);
			}
		}
	}
}
