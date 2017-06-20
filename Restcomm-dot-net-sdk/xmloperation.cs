using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
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
namespace org.restcomm.connect.sdk.dotnet
{
    //this class is used to create extension methods in string to get property value (like sid, ) from xml response
    //#pragma warning disable 0168 0169;
    public static class xmloperations
    {
        //



        public static string GetAccountProperty(this string xmldoc, string node)
        {
            try
            {

                XmlDocument xdoc = new XmlDocument();
                xdoc.Normalize();
                xdoc.LoadXml(xmldoc);
                xdoc.LoadXml(xmldoc.Substring(xmldoc.IndexOf(Environment.NewLine)));
                string finalpath = null;

                finalpath = xdoc.FirstChild.LocalName + "/" + xdoc.FirstChild.FirstChild.LocalName + "/" + node;


                var Node = xdoc.SelectSingleNode(finalpath);
                if (Node != null)
                    return xdoc.SelectSingleNode(finalpath).InnerText;
                else
                    return null;
            }
            catch (XmlException ex)
            {


                XmlException x = new XmlException(ex.Message + xmldoc);
                throw x;

            }
        }


        public static List<string> GetAccountsProperty(this string xmldoc, string node)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmldoc);
                XmlNode ydoc = xdoc.FirstChild.FirstChild;
                if (ydoc.HasChildNodes == false)
                {
                    return null;
                }

                else
                {

                    string finalpath = xdoc.FirstChild.LocalName + "/" + xdoc.FirstChild.FirstChild.LocalName + "/" + xdoc.FirstChild.FirstChild.FirstChild.LocalName;

                    List<string> result = new List<string>();

                    foreach (XmlNode x in xdoc.SelectNodes(finalpath))
                    {
                        var Node = x.SelectSingleNode(node);
                        if (Node != null)
                        {
                            result.Add(Node.InnerText);
                        }
                        else
                            result.Add(null);

                    }
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }











    }


    //list the properties to be extracted from xml
    public static class Property
    {
       // public static string Sid = "Sid";
        public static string AuthToken = "AuthToken";
        public static string EmailAddress = "EmailAddress";
        public static string Status = "Status";
        public static string AccountSid = "AccountSid";
        public static string ApiVersion = "ApiVersion";
        public static string DateCreated = "DateCreated";
        public static string DateUpdated = "DateUpdated";
        public static string Kind = "Kind";
        public static string FriendlyName = "FriendlyName";



    }


}

namespace Property
{
    class Account
    {
        public string Sid
        {
            get { return "Sid"; }
        }
        public string AuthToken
        {
            get { return "AuthToken"; }
        }



    }



}