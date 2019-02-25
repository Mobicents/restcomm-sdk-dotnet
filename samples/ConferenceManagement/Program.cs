using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.restcomm.connect.sdk.dotnet;

namespace ConferenceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var akount = new Account("Account sid here ", "Authentication_Token", "https://restcomm_ip/restcomm/2012-04-24/");
           var conferenceList= akount.GetConferenceList();
            var participantlist = conferenceList[0].GetParticipantList(akount.Properties.sid, akount.Properties.auth_token);
          bool isMuted=  participantlist[0].MuteParticipant(akount.Properties.sid, akount.Properties.auth_token, conferenceList[0].properties.sid);

        }
    }
}
