# duplex-sample
Sample applications showing a robust WCF Duplex implementation

## Setup
AJ.DuplexService: Run with administrative privileges or grant revoek permission to open an HTTP channel:
```
netsh http add urlacl url=http://+:9010/ajdotnet.duplexservice user=%USERDOMAIN%\%USERNAME%
netsh http add urlacl url=http://+:9011/ajdotnet.duplexservice user=%USERDOMAIN%\%USERNAME%
```
AJ.DuplexClient: Uses TCP/IP (netTcpBinding) by default; define USE_HTTP in the project settings for HTTP (wsDualHttpBinding).

## Quick start
The relevant classes in terms of duplex are:
- [AJ.DuplexService/Subscriptions/ClientManager](https://github.com/ajdotnet/duplex-sample/blob/master/AJ.DuplexService/Subscriptions/ClientManager.cs): Maintains the client callback channels and handles errors.
- [AJ.DuplexClient/Repositories/SubscriptionRepository](https://github.com/ajdotnet/duplex-sample/blob/master/AJ.DuplexClient/Repositories/SubscriptionRepository.cs): Maintains the client and handles errors.


NOTE: BLOG POSTS STILL UNDER CONSTRUCTION!


---- 
See https://ajdotnet.wordpress.com/ for background.


