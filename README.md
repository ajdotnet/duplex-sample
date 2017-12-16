# duplex-sample
Sample applications showing a robust WCF Duplex implementation

## Setup
AJ.DuplexService: Run with administrative privileges or grant revoek permission to open an HTTP channel:
    netsh http add urlacl url=http://+:9010/ajdotnet.duplexservice user=%USERDOMAIN%\%USERNAME%
    netsh http add urlacl url=http://+:9011/ajdotnet.duplexservice user=%USERDOMAIN%\%USERNAME%

AJ.DuplexClient: Uses TCP/IP (netTcpBinding) by default; define USE_HTTP in the project settings for HTTP (wsDualHttpBinding).

## Quick start
The relevant classes in terms of duplex are:
- AJ.DuplexService/Subscriptions/ClientManager: Maintains the client callback channels and handles errors.
- AJ.DuplexClient/Repositories/SubscriptionRepository: Maintains the client and handles errors.


---- 
See https://ajdotnet.wordpress.com/ for background.
