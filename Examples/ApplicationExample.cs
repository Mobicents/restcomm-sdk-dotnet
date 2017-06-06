using System;
using RestComm;

class MainClass
{
	public static void Main(string[] args)
	{
		string Sid = Console.ReadLine();
		string Authtoken = Console.ReadLine ();
		Account account = new Account (Sid, Authtoken);

		//changes the password of account
		account.ChangePassword("Test@123");

		//Creates a new Application
		Application app= account.CreateApplication ("DemoApp", "2012-04-24");

		//
	}

}
