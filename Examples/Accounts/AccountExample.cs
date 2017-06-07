using System;
using RestComm;
namespace Accounts
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//Login 
			Account akount = new Account ("Sid", "AuthToken");

			//prints Sid of Account
			Console.WriteLine (akount.Properties.Sid);

			//Creates Subaccount
			SubAccount subaccount=akount.CreateSubAccount("DemoAccounts","demoaccounts@restcomm.com","Demo@123");

			//prints name of subaccount
			Console.WriteLine(subaccount.Properties.friendlyname);

			//Changes the password
			subaccount.ChangePassword("Demo@123456");

			//Closes the subaccount
			subaccount.CloseSubAccount();
		}
	}
}
