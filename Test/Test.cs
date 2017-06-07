// /*
//  * TeleStax, Open Source Cloud Communications
//  * Copyright 2011-2016, Telestax Inc and individual contributors
//  * by the @authors tag.
//  *
//  * This is free software; you can redistribute it and/or modify it
//  * under the terms of the GNU Lesser General Public License as
//  * published by the Free Software Foundation; either version 2.1 of
//  * the License, or (at your option) any later version.
//  *
//  * This software is distributed in the hope that it will be useful,
//  * but WITHOUT ANY WARRANTY; without even the implied warranty of
//  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  * Lesser General Public License for more details.
//  *
//  * You should have received a copy of the GNU Lesser General Public
//  * License along with this software; if not, write to the Free
//  * Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
//  * 02110-1301 USA, or see the FSF site: http://www.fsf.org.
//  */
//
using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using RestComm;
namespace Test
{//In Credentials.txt(Project/bin/Debug)
	//First Line : Account Sid
	//Second Line :Authentication Token
	//Third Line :Login Password
	[TestFixture ]
	public class NUnitTestClass
	{

		string[] dictionary=new string[3];
		Account akount;



		[SetUp()]
		public void Login(){
			if (akount==null) {
				dictionary = System.IO.File.ReadAllLines (@"Credentials.txt");
				akount = new Account (dictionary [0], dictionary [1]);

			}
		}

		[Test()]
		[Ignore("It will create an extra closed Subaccount in the list")]
		public void  CreateSubAount()
		{

			SubAccount a = akount.CreateSubAccount ("Test", "demo@demo.com", "Demo@1234");
			Assert.IsNotEmpty (a.Properties.Sid);
			a.CloseSubAccount ();

		}

		[Test]
		public void ChangePassword(){
			if (dictionary.Length == 2||dictionary [2] == null )
				Assert.Fail ("Enter password in Credentials.txt");
			else {
				string oldauthtoken = akount.Properties.authtoken;
				akount.ChangePassword ("Demo@123456");

				string newauthtoken = akount.Properties.authtoken;


				akount.ChangePassword (dictionary[2]);
				Assert.AreNotEqual (newauthtoken, oldauthtoken);

			}
		}

		[Test]
		public void SubAccountListing(){

			List<SubAccount> subaccountlist= akount.GetSubAccountList ();
			if (subaccountlist.Count == 0) {
				SubAccount a = akount.CreateSubAccount ("Test", "demo@demo.com", "Demo@1234");
				subaccountlist= akount.GetSubAccountList ();
				Assert.AreNotEqual (subaccountlist.Count, 0);
				a.CloseSubAccount ();
			}
		}

		[Test]
		public void SearchPhoneNumber(){

			var numsearch =	akount.SearchPhoneNumbers ("US");
			numsearch.AddSearchParameter ("AreaCode", "305");
			List<PhoneNumber> numberlist = numsearch.Search ();
			if (numberlist.Count == 0)
				Assert.Fail ();
		}

		[Test]
		public void CreateClientANDMakeCall(){

			Client newclient = akount.makeclient ("Demo123", "Demo@123").Create();

			var callinit=akount.MakeCall ("+1234567", newclient.Properties.Login, "http://cloud.restcomm.com/restcomm/demos/hello-play.xml");

			Call lastcall =callinit.call ();
			Assert.AreEqual(lastcall.Properties.To,newclient.Properties.Login);
			newclient.Delete(akount.Properties.Sid,akount.Properties.authtoken);
		}
		[Test]
		public void CallDetail(){


			List<Call> calllist = akount.GetCallDetail ().Search ();
			if (calllist.Count == 0) {

				CreateClientANDMakeCall ();
				calllist = akount.GetCallDetail ().Search ();
				Assert.AreNotEqual (0, calllist.Count);
			}

		}
		[Test]
		public void CreateApplication(){
			Application app= akount.CreateApplication ("DemoApp", "2012-04-24");
			Assert.IsNotNullOrEmpty (app.Properties.Sid);
			app.Delete ();
		}
		[Test]
		public void ApplicationList(){
		//	Application app= akount.CreateApplication ("DemoApp", "2012-04-24");
			List<Application> applist = akount.GetApplicationList ();
			if (applist==null) {

				Application app= akount.CreateApplication ("DemoApp", "2012-04-24");
				applist = akount.GetApplicationList();
				Assert.IsNotNull(applist);
				app.Delete ();
			}


		}
		[Test]
		public void SendEmail(){
			akount.SendEmail ("demofrom@restcomm.com", "demoto@restcomm.com", "This is a test email", "Test Email");
		}
	}
}
