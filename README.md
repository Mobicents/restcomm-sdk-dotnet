


[Try Restcomm Cloud NOW for FREE!](https://www.restcomm.com/sign-up/) Zero download and install required.


All Restcomm [docs](https://www.restcomm.com/docs/) and [downloads](https://www.restcomm.com/downloads/) are now available at [Restcomm.com](https://www.restcomm.com).


# Restcomm Connect Dot Net Sdk

This sdk makes it easy for the developer to integerate restcomm connect services in to their dot net application.

## Installation

Import this library in to your project by using **Nuget package manager**.
To install Restcomm-dot-net-sdk, run the following command in the Package Manager Console 

```
Install-Package Restcomm-dot-net-sdk 
```
 or you can create nuget/dll yourself by compiling this project in Visual Studio , Xamarin Studio or monodevelop.

 use **org.restcomm.connect.sdk.dotnet** namespace to access elements from this sdk.

## Dependencies 


        Newtonsoft.Json (>= 10.0.2)
        RestSharp (>= 105.2.3)


### Sample Usage

Some basic examples on how to initiate and use this sdk to make call and send email.


```
Account account=new Account("Account_Sid","Auth_Token","baseurl");

 var OutBCall = accountount.MakeCall("+1234567890", "client:democlients", "http://cloud.restcomm.com/restcomm/demos/hello-play.xml");
 
  OutBCall.AddParameter("Timeout", "15");
  Call call = OutBCall.call();
  Console.WriteLine(call.Properties.status);
  var EmailData = akount.SendEmail("demo123@restcomm.com", "abcd@myemail.com", "This is a test email", "Test");
  EmailData.Send();
```
For more examples ,please visit this [link].
 
#  Want to Contribute

[See our Contributors Guide](https://github.com/RestComm/Restcomm-Connect/wiki/Contribute-to-RestComm)


# Issue Tracking and Roadmap

[Issue Tracker](https://github.com/RestComm/restcomm-sdk-dotnet/issues)

# License

 RestComm is lead by TeleStax, Inc. and developed collaboratively by a community of individual and enterprise contributors.
  
  RestComm is licensed under dual license policy. The default license is the Free Open Source GNU Affero GPL v3.0. Alternatively a commercial license can be obtained from Telestax ([contact form](https://telestax.com/contact/#InquiryForm)).

[link]: <https://github.com/RestComm/restcomm-sdk-dotnet/tree/master/Examples>
