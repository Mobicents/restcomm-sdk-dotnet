using System;
using System.Collections.Generic;
using org.restcomm.connect.sdk.dotnet;

namespace Accounts
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Login 
            var akount = new Account("Account sid here ", "Authentication_Token", "https://restcomm_ip/restcomm/2012-04-24/");

            //prints Sid of Account
            Console.WriteLine(akount.Properties.sid);
           
          
                List<SubAccount> subaccounts = akount.GetSubAccountList();
                Console.WriteLine(subaccounts[0].Properties.sid);
         
            //Creates Subaccount
           SubAccount subaccount=akount.CreateSubAccount("DemoAccounts","demoaccounts@restcomm.com","Demo@123");

            //prints name of subaccount
            Console.WriteLine(subaccount.Properties.friendly_name);

            //Changes the password
            	subaccount.ChangePassword("Demo@123456");

            //Closes the subaccount
            	subaccount.CloseSubAccount();
            Console.ReadLine();
        }
    }
}
