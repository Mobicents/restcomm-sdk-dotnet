using System;
using System.Xml;
using System.Collections.Generic;

namespace RestComm
{
	//this class is used to create extension methods in string to get property value (like sid, ) from xml response
		
		public static class xmloperations
		{
		//

			public static string GetAccountProperty(this string xmldoc,string node){
				
				XmlDocument xdoc = new XmlDocument ();
				xdoc.LoadXml (xmldoc);
				string finalpath =null;


			switch (xdoc.FirstChild.FirstChild.LocalName) {
			case "Application":
				finalpath = "RestcommResponse/Application/" + node;
				break;
			case "Account":
				finalpath = "RestcommResponse/Account/" + node;
				break;
			case "Call":
				finalpath = "RestcommResponse/Call/" + node;
				break;
			}
			
			return xdoc.SelectSingleNode(finalpath).InnerText;
			}


		public static List<string> GetAccountsProperty(this string xmldoc,string node)
		{	try{
			XmlDocument xdoc = new XmlDocument ();
			xdoc.LoadXml (xmldoc);
			string finalpath=null;

			switch (xdoc.FirstChild.FirstChild.LocalName) {
			case "Applications":
				finalpath = "RestcommResponse/Applications/Application";
				break;
			case "Accounts":
				finalpath = "RestcommResponse/Accounts/Account" ;
				break;
			case "AvailablePhoneNumbers":
				finalpath = "RestcommResponse/AvailablePhoneNumbers/AvailablePhoneNumber";
				break;
			case "Calls":
				finalpath = "RestcommResponse/Calls/Call";
				break;
			}

			List<string> result = new List<string> ();
				Console.WriteLine(xmldoc);
			foreach (XmlNode x in xdoc.SelectNodes(finalpath)) {
				var Node = x.SelectSingleNode (node);
				if (Node != null) {
					result.Add (Node.InnerText);
				} else
					result.Add (null);

			}
				return result;
			}
			catch(Exception ex){

				throw ex;
			}
			}
			

		




		

		

	}


//list the properties to be extracted from xml
	public static class Property
	{
		public static string Sid="Sid";
		public static string AuthToken="AuthToken";
		public static string EmailAddress="EmailAddress";
		public static string Status="Status";
		public static string AccountSid = "AccountSid";
		public static string ApiVersion="ApiVersion";
		public static string DateCreated="DateCreated";
		public static string DateUpdated = "DateUpdated";
		public static string Kind="Kind";
		public static string FriendlyName="FriendlyName";
	


	}


}