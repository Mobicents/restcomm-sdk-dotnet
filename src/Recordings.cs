using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
namespace org.restcomm.connect.sdk.dotnet
{
	public partial class Account
	{
		public List<Recording> GetRecordingList(Dictionary<string, string> RecordingFIlter = null,Dictionary<string,string> PagingFilter=null)
		{
			RestClient client = new RestClient(baseurl + "Accounts/" + Properties.sid + "/Recordings.json");
			RestRequest request = new RestRequest(Method.GET);
			if (RecordingFIlter != null)
			{
				foreach (var pair in RecordingFIlter)
				{
					request.AddParameter(pair.Key, pair.Value);
				}
			}
			if (PagingFilter != null)
			{
				foreach (var pair in PagingFilter)
				{
					request.AddQueryParameter(pair.Key, pair.Value);
				}
			}
		client.Authenticator = new HttpBasicAuthenticator(Properties.sid, Properties.auth_token);
		IRestResponse response = client.Execute(request);
            var content = response.Content;
		content = "["+content.Split('[', ']')[1]+"]";
            
			var recordingPlist= new List<recordingProperties>();
			recordingPlist = JsonConvert.DeserializeObject<List<recordingProperties>>(content);
			var recordingList = new List<Recording>();
			foreach (var recordingsproperty in recordingPlist)
			{
				recordingList.Add(new Recording(recordingsproperty));
			}
			return recordingList;
		}
	}
	public class Recording
	{
		public recordingProperties Properties;
		public Recording(recordingProperties properties)
		{
			this.Properties = properties;
		}
	}
}
