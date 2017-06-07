using System;
using RestComm;
namespace Email
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Login 
			var akount = new Account ("Sid", "AuthToken");

			var EmailData = akount.SendEmail ("demo@demo.com", "demo2@demo.com", "This is a test email", "Test");


		}
	}
}
