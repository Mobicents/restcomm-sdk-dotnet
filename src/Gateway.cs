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
{
	public partial class Account{
		/*public List<Gateway> GetGatewayList(){
			RestClient client = new RestClient (baseurl + "/Accounts/" + Sid + "/Management/Gateways");
			RestRequest request = new RestRequest (Method.GET);
			IRestResponse response=client.Execute()
		}*/


	}
	public class Gateway
	{
		private string Sid;
		private string AuthToken;
		public Gateway (Account account)
		{
			this.Sid = account.Properties.Sid;
			this.AuthToken = account.Properties.authtoken;
		}
		public Gateway(string Sid,string Authtoken){
			this.Sid = Sid;
			this.AuthToken = Authtoken;
		}

	}
}

