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
//Not complete
namespace RestComm
{
	//In 
	public partial class Account{
		public makecall MakeCall(string From,string To,string Url){
			RestClient client=new RestClient(baseurl+"Accounts/"+Properties.Sid+"/Calls");
			RestRequest makecall = new RestRequest (Method.POST);
			client.Authenticator =new HttpBasicAuthenticator(Properties.Sid, Properties.authtoken);
			makecall.AddParameter ("From", From);
			makecall.AddParameter ("To", To);
			makecall.AddParameter ("Url", Url);

			return new makecall (client,makecall);

		}
		public CallFilter GetCallDetail(){
			
			RestRequest makecall = new RestRequest (Method.GET);
			return new CallFilter (makecall,Properties.Sid,Properties.authtoken);

		}



	}
	public class CallFilter{
		RestClient client;
		RestRequest request;
		string Sid;
		string TokenNo;
		List<string> parametername=new List<string>();
		List<string> parametervalue=new List<string>();
		public CallFilter(RestRequest request,string Sid,string TokenNo){
			
			this.request = request;
			this.Sid = Sid;
			this.TokenNo = TokenNo;
		}
		public void AddSearchFilter(string ParameterName,string ParameterValue){
			parametername.Add (ParameterName);
			parametervalue.Add (ParameterValue);
		}
		public List<Call> Search(){
			string clienturl=Account.baseurl+"Accounts/"+Sid+"/Calls";
			if (parametername.Count != 0) {
				clienturl += "?";
				int i = 0;
				foreach (string s in parametername) {
					if(i!=0)
						clienturl+="&";
					clienturl += parametername [i];
					clienturl += "=" + parametervalue[i];
					i++;
				}


			}

			client = new RestClient (clienturl);
			client.Authenticator = new HttpBasicAuthenticator (Sid, TokenNo);
			IRestResponse response = client.Execute (request);

			List<Call> calllist=new List<Call>();
			List<string> Sidlist = response.Content.GetAccountsProperty ("Sid");
			if (Sidlist != null) {
				int count = Sidlist.Count;
				for (int j = 0; j < count; j++) {

					calllist.Add (new Call (response.Content, j));

				}
				return calllist;
			}
			return null;
		}
	}

	public class makecall{
		
		RestClient Client;
		RestRequest Request;
		public makecall(RestClient client,RestRequest request){
			Client = client;

			Request=request;

		}
		public void AddParameter(string ParameterName,string ParameterValue){
			
			Request.AddParameter (ParameterName, ParameterValue);

		}
		public Call call(){
			IRestResponse response = Client.Execute (Request);
			Console.WriteLine (response.Content);
			return new Call(response.Content);
		}


	}

	public class Call{
		
		public callProperties Properties;

		public Call(string xmlresponse){
			Properties.Sid = xmlresponse.GetAccountProperty ("Sid");
			Properties.ParentCallSid = xmlresponse.GetAccountProperty ("ParentCallSid");
			Properties.DateCreated = xmlresponse.GetAccountProperty ("DateCreated");
			Properties.DateUpdated = xmlresponse.GetAccountProperty ("DateUpdated");
			Properties.To = xmlresponse.GetAccountProperty ("To");
			Properties.From = xmlresponse.GetAccountProperty ("From");
			Properties.PhoneNumberSid = xmlresponse.GetAccountProperty ("PhoneNumberSid");
			Properties.Status = xmlresponse.GetAccountProperty ("Status");
			Properties.StartTime = xmlresponse.GetAccountProperty ("StartTime");
			Properties.EndTime = xmlresponse.GetAccountProperty ("EndTime");
			Properties.Duration = xmlresponse.GetAccountProperty ("Duration");
			Properties.Price = xmlresponse.GetAccountProperty ("Price");
			Properties.Direction = xmlresponse.GetAccountProperty ("Direction");
			Properties.AnsweredBy = xmlresponse.GetAccountProperty ("AnsweredBy");
			Properties.ApiVersion = xmlresponse.GetAccountProperty ("ApiVersion");
			Properties.ForwardFrom = xmlresponse.GetAccountProperty ("ForwardFrom");
			Properties.CallerName = xmlresponse.GetAccountProperty ("CallerName");
			Properties.Uri = xmlresponse.GetAccountProperty ("Uri");
		}
		//use this constructor when there is list of call in response
		public Call(string xmlresponse,int elementunmber){

			Properties.Sid = xmlresponse.GetAccountsProperty ("Sid")[elementunmber];
			Properties.ParentCallSid = xmlresponse.GetAccountsProperty ("ParentCallSid")[elementunmber];
			Properties.DateCreated = xmlresponse.GetAccountsProperty ("DateCreated")[elementunmber];
			Properties.DateUpdated = xmlresponse.GetAccountsProperty ("DateUpdated")[elementunmber];
			Properties.To = xmlresponse.GetAccountsProperty ("To")[elementunmber];
			Properties.From = xmlresponse.GetAccountsProperty ("From")[elementunmber];
			Properties.PhoneNumberSid = xmlresponse.GetAccountsProperty ("PhoneNumberSid")[elementunmber];
			Properties.Status = xmlresponse.GetAccountsProperty ("Status")[elementunmber];
			Properties.StartTime = xmlresponse.GetAccountsProperty ("StartTime")[elementunmber];
			Properties.EndTime = xmlresponse.GetAccountsProperty ("EndTime")[elementunmber];
			Properties.Duration = xmlresponse.GetAccountsProperty ("Duration")[elementunmber];
			Properties.Price = xmlresponse.GetAccountsProperty ("Price")[elementunmber];
			Properties.Direction = xmlresponse.GetAccountsProperty ("Direction")[elementunmber];
			Properties.AnsweredBy = xmlresponse.GetAccountsProperty ("AnsweredBy")[elementunmber];
			Properties.ApiVersion = xmlresponse.GetAccountsProperty ("ApiVersion")[elementunmber];
			Properties.ForwardFrom = xmlresponse.GetAccountsProperty ("ForwardFrom")[elementunmber];
			Properties.CallerName = xmlresponse.GetAccountsProperty ("CallerName")[elementunmber];
			Properties.Uri = xmlresponse.GetAccountsProperty ("Uri")[elementunmber];
		}
		public void ModifyCall(string ParameterName,string NewParameterValue,String AccountSId,String AuthToken){
			RestClient client=new RestClient(Account.baseurl+"Accounts/"+AccountSId+"/Calls/"+Properties.Sid);
			RestRequest makecallmodification = new RestRequest (Method.POST);
			client.Authenticator = new HttpBasicAuthenticator (AccountSId, AuthToken);
			makecallmodification.AddParameter(ParameterName,NewParameterValue);
			client.Execute (makecallmodification);
		}


	}
}

