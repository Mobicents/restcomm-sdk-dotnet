using System;
using System.Xml;
using System.Collections.Generic;

namespace RestComm
{
	//this class is used to create extension methods in string to get property value (like sid, ) from xml response
		
		public static class xmloperations
		{


			public static string GetAccountProperty(this string xmldoc,string node){
				
				XmlDocument xdoc = new XmlDocument ();
				xdoc.LoadXml (xmldoc);
			string finalpath =null;

			if (xdoc.FirstChild.FirstChild.LocalName == "Application")
				finalpath = "RestcommResponse/Application/" + node;
			else if (xdoc.FirstChild.FirstChild.LocalName == "Account")
				finalpath = "RestcommResponse/Account/" + node;
			
			return xdoc.SelectSingleNode(finalpath).InnerText;
			}


		public static List<string> GetAccountsProperty(this string xmldoc,string node)
		{	
			XmlDocument xdoc = new XmlDocument ();
			xdoc.LoadXml (xmldoc);
			string finalpath;

			if (xdoc.FirstChild.FirstChild.LocalName == "Accounts") {
				finalpath = "RestcommResponse/Accounts/Account";

			}
			else if (xdoc.FirstChild.FirstChild.LocalName == "Applications") {
				finalpath = "RestcommResponse/Applications/Application";
			} else {
				finalpath = null;

			}

			List<string> result = new List<string> ();

			foreach (XmlNode x in xdoc.SelectNodes(finalpath)) {
					
				result.Add (x.SelectSingleNode (node).InnerText);

			}
				return result;
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