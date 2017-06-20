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

namespace org.restcomm.connect.sdk.dotnet
{
    public struct classproperty
    {
        //public	account Account;

    }
    public struct accountProperties
    {
        public string Sid { get; set; }
        public string friendlyname { get; set; }
        public string status { get; set; }
        public string dateupdated { get; set; }
        public string datecreated { get; set; }
        public string authtoken { get; set; }
        public string uri { get; set; }
    }
    public struct callProperties
    {

        public string Sid { get; set; }
        public string ParentCallSid { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string PhoneNumberSid { get; set; }
        public string Status { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string Direction { get; set; }
        public string AnsweredBy { get; set; }
        public string ApiVersion { get; set; }
        public string ForwardFrom { get; set; }
        public string CallerName { get; set; }
        public string Uri { get; set; }

    }
    public struct applicationProperties
    {
        public String Sid { get; set; }
        public String AccountSid { get; set; }
        public String FriendlyName { get; set; }
        public String DateUpdated { get; set; }
        public String DateCreated { get; set; }
        public String APIversion { get; set; }
        public String Kind { get; set; }
        public String authtoken { get; set; }

    }
    public struct numberProperties
    {

        public string FriendlyName { get; set; }
        public string Number { get; set; }
        public string IsoCountry { get; set; }

    }
    public struct clientProperties
    {
        public string Sid { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string FriendlyName { get; set; }
        public string AccountSid { get; set; }
        public string ApiVersion { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string VoiceUrl { get; set; }
        public string VoiceMethod { get; set; }
        public string VoiceFallbackUrl { get; set; }
        public string VoiceFallbackMethod { get; set; }
        public string VoiceApplication { get; set; }
        public string Uri { get; set; }

    }
    public struct gatewayproperties
    {
        public string Sid { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string FriendlyName { get; set; }
        public string Password { get; set; }
        public string Proxy { get; set; }
        public string Register { get; set; }
        public string UserName { get; set; }
        public string TimeToLive { get; set; }
        public string Uri { get; set; }


    }

}

