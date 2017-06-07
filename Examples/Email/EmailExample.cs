using System;
using RestComm;
namespace Email
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Login 
			 var akount = new Account ("AC13b4372c92ed5c07d951cf842e2664ff", "eff8eb1e1334884cf7dd59d3a00e687a");

			var EmailData = akount.SendEmail ("demo@demo.com", "demo2@demo.com", "This is a test email", "Test");


		}
	}
}
