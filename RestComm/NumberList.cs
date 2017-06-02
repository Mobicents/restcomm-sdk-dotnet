using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;

namespace RestComm
{//
	public partial class Account{

		public NumberFilter SearchPhoneNumbers(string IsoCountryCode){

			RestClient client = new RestClient (baseurl + "Accounts/" + Sid + "/AvailablePhoneNumbers/" + IsoCountryCode + "/Local");

			client.Authenticator = new HttpBasicAuthenticator (Sid, authtoken);
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
			int i = 0;
			/*foreach (string s in searchparameter) {
				Request.AddQueryParameter(searchparameter [i], parameterValue [i]);
				i++;

			}*/
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

		public string FriendlyName;
		public string Number;
		public string IsoCountry;


		public PhoneNumber(string friendlyname,string phoneNumber,string isoCountry){

			FriendlyName = friendlyname;
			Number = phoneNumber;
			IsoCountry = isoCountry;



		}

	}
}

