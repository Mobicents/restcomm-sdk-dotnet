using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.restcomm.connect.sdk.dotnet;
namespace recordingExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Account akount = new Account("account_sid", "auth_token", "Base_url");
            var recordingList=akount.GetRecordingList();
            foreach(var recording in recordingList)
            {
                Console.WriteLine("Audio_Url: " + recording.Properties.file_uri);
            }
        }
    }
}
