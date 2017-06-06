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
using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace RestComm
{//
	public partial class Account{

		public NumberFilter SearchPhoneNumbers(string IsoCountryCode){

			RestClient client = new RestClient (baseurl + "Accounts/" + Properties.Sid + "/AvailablePhoneNumbers/" + IsoCountryCode + "/Local");

			client.Authenticator = new HttpBasicAuthenticator (Properties.Sid, Properties.authtoken);
			RestRequest req = new RestRequest (Method.GET);//cant figure out correct method for it . get and post wont work
			return new NumberFilter(client,req);

		}


	}
	public	class NumberFilter{
		private RestClient Client;
		private RestRequest Request;
		private List<string> searchparameter;
		private List<string> parameterValue;

		public NumberFilter(RestClient client, RestRequest request){
			Client = client;
			Request = request;
		}
		public void AddSearchParameter(string ParameterName,string ParameterValue){
			Request.AddQueryParameter (ParameterName, ParameterValue);
		}
		public void AddSearchParameter(List<String> ParameterName,List<String> ParameterValue){
			if (ParameterValue.Count == ParameterName.Count) {
				
				searchparameter.AddRange(ParameterName);
				parameterValue.AddRange(ParameterValue);
			}
		}
		public List<PhoneNumber> Search(){
			
			IRestResponse res = Client.Execute (Request);
			var content = res.Content;

			List<string> friendlyname = content.GetAccountsProperty ("FriendlyName");
			List<string> Phonenumbers = content.GetAccountsProperty ("PhoneNumber");
			List<string> IsoCountry = content.GetAccountsProperty ("IsoCountry");
			List<PhoneNumber> numberlist=new List<PhoneNumber>();
			int j = 0;
			foreach (string s in friendlyname) {
				
				numberlist.Add (new PhoneNumber (friendlyname [j], Phonenumbers [j], IsoCountry [j]));
				j++;
			}
			return numberlist;
		}





	}
	public class PhoneNumber{


		public numberProperties Properties;

		public PhoneNumber(string friendlyname,string phoneNumber,string isoCountry){

			Properties.FriendlyName = friendlyname;
			Properties.Number = phoneNumber;
			Properties.IsoCountry = isoCountry;



		}

	}
}

