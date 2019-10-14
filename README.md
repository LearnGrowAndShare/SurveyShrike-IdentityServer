## # **[SurveyShrike-IdentityServer](https://github.com/dreamerNcoder/SurveyShrike-IdentityServer)**
This is one of the main module for *SurveyShrike* application. The user management is done by this micro service. 


## Readings
**Identity server** – This project uses the [Identity server 4](http://docs.identityserver.io/en/latest/) 
In general identityServer4 is an OpenID Connect and OAuth 2.0 framework for ASP.NET Core.

It enables the following features in your applications:

 - Authentication as a Service - 
							 Centralized login logic and workflow for all of your applications (web, native, mobile, services). IdentityServer is an officially  [certified](https://openid.net/certification/)  implementation of OpenID Connect.
							 
 - Single Sign-on / Sign-out
			Single sign-on (and out) over multiple application types.
			
 - Access Control for APIs- 
 - Issue access tokens for APIs for various types of clients, e.g. server to server, web applications, SPAs and       native/mobile apps.
  
**Clean Architecture** – pplications that follow the Dependency Inversion Principle as well as the Domain-Driven Design (DDD) principles tend to arrive at a similar architecture. This architecture has gone by many names over the years. One of the first names was Hexagonal Architecture, followed by Ports-and-Adapters. More recently, it's been cited as the [Onion Architecture](https://jeffreypalermo.com/blog/the-onion-architecture-part-1/) or [Clean Architecture](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html).
![enter image description here](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-7.png)

Each layer communication diagram
![enter image description here](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-8.png)

## Getting Started

Use these instructions to get the project up and running.
### Prerequisites
You will need the following tools:

-   [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/)  (version 16.3 or later)
-   [.NET Core SDK 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)
-   [Node.js](https://nodejs.org/en/)  (version 10 or later) with npm (version 6.11.3 or later)
- 
### Setup
Follow these steps to get your development environment set up:

1.  Clone the repository
    
2.  Open the project with VS 2019.
    
    ```
    dotnet restore
    
    ```
    
3.  Next, build the solution by running:
    
    ```
    dotnet build
    
    ```
    
4.  Next, within the  `SurveyShrike-IdentityServer` (root)  directory, launch the identity server by running:
    
    ```
    dotnet run bin\Debug\netcoreapp3.0\SurveyShrike-IdentityServer.dll
    
    ```
    
5.  Once the server has started, within the  navigate to [http://localhost:5000/.well-known/openid-configuration](http://localhost:5000/.well-known/openid-configuration),
 If it does not give error, We have successfully configured identity server.
    
 ## Technologies
 -  .NET Core 3
-   ASP.NET Core 3
-   Entity Framework Core 3
