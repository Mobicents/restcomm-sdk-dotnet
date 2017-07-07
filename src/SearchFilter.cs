using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restcomm_dot_net_sdk
{

    /// <summary>
    /// List of SearchFIlters for Calls
    /// </summary>
    public static class CallFilter
    {
        /// <summary>
        /// Only show calls to this phone number or Client identifier.
        /// </summary>
        public static string To { get { return "To"; } }
        /// <summary>
        /// Only show calls from this phone number or Client identifier.
        /// </summary>
        public static string From { get { return "From"; } }
        /// <summary>
        /// Only show calls currently in this status. May be queued, ringing, in-progress, canceled, completed, failed, busy, or no-answer.
        /// </summary>
        public static string Status { get { return "Status"; } }
        /// <summary>
        /// Only show calls that started on this date, given as YYYY-MM-DD. Also supports inequalities, such as StartTime=YYYY-MM-DD for calls that started at or before midnight on a date, and StartTime=YYYY-MM-DD for calls that started at or after midnight on a date.
        /// </summary>
        public static string StartTime { get { return "StartTime"; } }
        /// <summary>
        /// Only show calls spawned by the call with this Sid.
        /// </summary>
        public static string ParentCallSid { get { return "ParentCallSid"; } }
    }
    /// <summary>
    /// list of all available filters for AvailablePhoneNumber
    /// </summary>
    public static class AvailablePhoneNumberFilter
    {/// <summary>
     ///  	Find phone numbers in the specified area code. (US and Canada only)
     /// </summary>
        public static string AreaCode { get { return "AreaCode"; } }
        /// <summary>
        /// A pattern to match phone numbers on. Valid characters are '' and [0-9a-zA-Z]. The '' character will match any single digit.
        /// </summary>
        public static string Contains { get { return "Contains"; } }
        /// <summary>
        /// This indicates whether the phone numbers can receive text messages. Possible values are true or false.
        /// </summary>
        public static string SmsEnabled { get { return "SmsEnabled"; } }
        /// <summary>
        /// This indicates whether the phone numbers can receive MMS messages. Possible values are true or false.
        /// </summary>
        public static string MmsEnabled { get { return "MmsEnabled"; } }
        /// <summary>
        /// This indicates whether the phone numbers can receive calls. Possible values are true or false.
        /// </summary>
        public static string VoiceEnabled { get { return "VoiceEnabled"; } }
        /// <summary>
        /// Indicates whether the response includes phone numbers which require any Address. Possible values are true or false. If not specified, the default is false, and results could include phone numbers with an Address required.
        /// </summary>
        public static string ExcludeAllAddressRequired { get { return "ExcludeAllAddressRequired"; } }
        /// <summary>
        /// Indicates whether the response includes phone numbers which require a local Address. Possible values are true or false. If not specified, the default is false, and results could include phone numbers with a local Address required.
        /// </summary>
        public static string ExcludeLocalAddressRequired { get { return "ExcludeLocalAddressRequired"; } }
        /// <summary>
        /// Indicates whether the response includes phone numbers which require a foreign Address. Possible values are true or false. If not specified, the default is false, and results could include phone numbers with a foreign Address required.
        /// </summary>
        public static string ExcludeForeignAddressRequired { get { return "ExcludeForeignAddressRequired"; } }
        /// <summary>
        ///  Include phone numbers new to theRestcomm platform.Possible values are either true or false. Default is true.
        /// </summary>
        public static string Beta { get { return "Beta"; } }
    }
    /// <summary>
    /// list of all filter for smsFilter
    /// </summary>
    public static class SMSFilter
    {   /// <summary>
        /// Only show messages to this phone number or Client identifier.
        /// </summary>
        public static string To { get { return "To"; } }
        /// <summary>
        /// Only show messages from this phone number or Client identifier.
        /// </summary>
        public static string From { get { return "From"; } }
        /// <summary>
        /// Only show messages that started on this date, given as YYYY-MM-DD. Also supports inequalities, such as StartTime=YYYY-MM-DD for messages that started at or before midnight on a date, and StartTime=YYYY-MM-DD for messages that started at or after midnight on a date.
        /// </summary>
        public static string StartTime { get { return "StartTime"; } }
        /// <summary>
        /// Only show messages that ended on this date, given as YYYY-MM-DD. Also supports inequalities, such as StartTime=YYYY-MM-DD for messages that started at or before midnight on a date, and StartTime=YYYY-MM-DD for messages that started at or after midnight on a date.
        /// </summary>
        public static string EndTime { get { return "EndTime"; } }
        /// <summary>
        /// Only show messages that contain this body.
        /// </summary>
        public static string Body { get { return "Body"; } }
    }
    /// <summary>
    /// List of all filter for notificationfilter
    /// </summary>
    public static class NotifiationFilter {
        /// <summary>
        /// Only show notifications that started on this date, given as YYYY-MM-DD. Also supports inequalities, such as StartTime=YYYY-MM-DD for notifications that started at or before midnight on a date, and StartTime=YYYY-MM-DD for notifications that started at or after midnight on a date.
        /// </summary>
        public static string StartTime { get { return "StartTime"; } }
        /// <summary>
        /// Only show notifications that ended on this date, given as YYYY-MM-DD. Also supports inequalities, such as StartTime=YYYY-MM-DD for notifications that started at or before midnight on a date, and StartTime=YYYY-MM-DD for notifications that started at or after midnight on a date.
        /// </summary>
        public static string EndTime { get { return "EndTime"; } }
        /// <summary>
        /// Only show notifications that returned this Error Code
        /// </summary>
        public static string ErrorCode { get { return "ErrorCode"; } }
        /// <summary>
        /// Only show notifications that have this RequestUrl
        /// </summary>
        public static string RequestUrl { get { return "RequestUrl"; } }
        /// <summary>
        /// Only show notifications that contain this MessageText.
        /// </summary>
        public static string MessageText { get { return "MessageText"; } }
    }
    /// <summary>
    /// list of all available filter for IncomingPhoneNumber
    /// </summary>
    public static class IncomingPhoneNumberFilter
    {/// <summary>
     /// Only show the incoming phone number resources that match this pattern. You can specify partial numbers and use '*' as a wildcard for any digit.
     /// </summary>
        public static string PhoneNumber { get { return "PhoneNumber"; } }
        /// <summary>
        /// Only show the incoming phone number resources with friendly names that exactly match this name.
        /// </summary>
        public static string FriendlyName { get { return "FriendlyName"; } }
        /// <summary>
        /// Include phone numbers new to the Restcomm platform. Possible values are either true or false. Default is true.
        /// </summary>
        public static string Beta { get { return "Beta"; } }
    }
    /// <summary>
    /// List of all available filter for transcription
    /// </summary>
   public static class transcriptionsFIlter
    {
        /// <summary>
        /// Only show transcriptions that started on this date, given as YYYY-MM-DD. Also supports inequalities, such as StartTime=YYYY-MM-DD for transcriptions that started at or before midnight on a date, and StartTime=YYYY-MM-DD for transcriptions that started at or after midnight on a date.
        /// </summary>
        public static string StartTime { get { return "StartTime";  } }
        /// <summary>
        /// Only show transcriptions that ended on this date, given as YYYY-MM-DD. Also supports inequalities, such as StartTime=YYYY-MM-DD for transcriptions that started at or before midnight on a date, and StartTime=YYYY-MM-DD for transcriptions that started at or after midnight on a date.
        /// </summary>
        public static string EndTime { get { return "EndTime"; } }
        /// <summary>
        /// Only show transcriptions that contain this TranscriptionText
        /// </summary>
        public static string TranscriptionText { get { return "TranscriptionText"; } }
    }
    
}
