using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.restcomm.connect.sdk.dotnet;
namespace TranscriptionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var akount = new Account("Account sid here ", "Authentication_Token", "https://restcomm_ip/restcomm/2012-04-24/");
            var transcriptionList= akount.GetTranscriptionList().Search();
            foreach(Transcription tr in transcriptionList)
            {
                Console.WriteLine(tr.Properties.transcription_text);
            }
        }
    }
}
