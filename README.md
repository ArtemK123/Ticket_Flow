# Ticket_Flow
Service for booking cinema tickets - master branch

This is a service with frontend/backend API architecture. The backend part is created in the microservice-oriented way.

Currently, there is 2 versions of the app: with Java backend and .Net core backend. The master branch uses .Net version. 
Also, the app uses ReactJS as frontend framework, PostgreSQL as database and Consul/Eureka as Service Discovery (Consul for .Net core, Eureka for Java). 
The docker is used for containerization and orchestration.

In the following section you can find the dependencies, how-to-run and how-to-use sections, which are relevant mostly for .Net core version of the app. 
The same information for Java version you can find in the approptiate branches (java_docker, java_local).

Dependencies:

 - .Net Core 3.1: https://dotnet.microsoft.com/download/dotnet-core/3.1
 
 - PosrgreSQL: https://www.postgresql.org/download/
 
 - Consul: https://www.consul.io/downloads
 
 - NodeJS (for ReactJS): https://nodejs.org/en/download/


How to clone:

    - git clone https://github.com/ArtemK123/Ticket_Flow


How to build:
 
    1. Setup nuget cache in local folder. 
	It is needed because almost all the time projects uses dev version of custom Nuget packages and those packages should be shared via local folder 
	    
		dotnet nuget add source <path to your local folder>
		
	2. Build Common project.
	
		- dotnet build <root folder path>/backend/dotnet/ticketflow/TicketFlow.Common/TicketFlow.Common.csproj
		
		- copy all *.nupkg files from <root folder path>/backend/dotnet/ticketflow/TicketFlow.Common/bin/Debug to local nuget folder (point 1)
		
	3. Build Client project of services:
	
	    - dotnet build <root folder path>/backend/dotnet/ticketflow/TicketFlow.IdentityService.Client/TicketFlow.IdentityService.Client.csproj
		
		- copy all *.nupkg files from <root folder path>/backend/dotnet/ticketflow/TicketFlow.IdentityService.Client/bin/Debug to local nuget folder
		
		- dotnet build <root folder path>/backend/dotnet/ticketflow/TicketFlow.ProfileService.Client/TicketFlow.ProfileService.Client.csproj
		
		- copy all *.nupkg files from <root folder path>/backend/dotnet/ticketflow/TicketFlow.ProfileService.Client/bin/Debug to local nuget folder
		
		- dotnet build <root folder path>/backend/dotnet/ticketflow/TicketFlow.MovieService.Client/TicketFlow.MovieService.Client.csproj
		
		- copy all *.nupkg files from <root folder path>/backend/dotnet/ticketflow/TicketFlow.MovieService.Client/bin/Debug to local nuget folder
		
		- dotnet build <root folder path>/backend/dotnet/ticketflow/TicketFlow.TicketService.Client/TicketFlow.TicketService.Client.csproj
		
		- copy all *.nupkg files from <root folder path>/backend/dotnet/ticketflow/TicketFlow.TicketService.Client/bin/Debug to local nuget folder
		
	4. Build whole .Net solution:
	    
		- dotnet build <root folder path>/backend/dotnet/ticketflow/TicketFlow.sln
		
	5. 	Build webclient:
	    
		- cd <root folder path>/frontend/ticketflow
		
		- npm install
	
	- Note1: You can simplify the build of Nuget packages (actions 2-3) by running build of whole .Net solution couple of times. 
	The algorithm is following: build solution (action 4), in root folder run search for all *.nupkg files and copy them to nuget local folder. 
	Firstly, only Common project will be successfully built, all others will be failed. After the second build all client projects will be built. 
	After the third one the whole project will be successfully built.
	
	- Note2: If your want to re-build nuget packages, you should before that clear them in global nuget cache. 
	Without that step your changes in package will not be fetched by other projects. Use should delete the re-builded package`s folder in global cache. 
	The location of the cache can be found in the article: 
	https://docs.microsoft.com/en-us/nuget/consume-packages/managing-the-global-packages-and-cache-folders
	

How to run:

	1. Install and run Consul.

	2. Install and run PostgreSQL 
	    
		Ensure that the following databases are created in PostgreSQL:
		    - ticketflow_identity
			- ticketflow_profile
			- ticketflow_ticket
			- ticketflow_movie
			
		Ensure that the following users are created in PostgreSQL. The password for each user is TicketFlow:
	        - ticketflow_identity_user (for database ticketflow_identity)
			- ticketflow_profile_user (for database ticketflow_profile)
			- ticketflow_ticket_user (for database ticketflow_ticket)
			- ticketflow_movie_user (for database ticketflow_movie)

	3. dotnet run --project=<root folder path>/backend/dotnet/TicketFlow/TicketFlow.IdentityService/TicketFlow.IdentityService.csproj

	4. dotnet run --project=<root folder path>/TicketFlow.ProfileService/TicketFlow.ProfileService.csproj

	5. dotnet run --project=<root folder path>/TicketFlow.MovieService/TicketFlow.MovieService.csproj

	6. dotnet run --project=<root folder path>/TicketFlow.TicketService/TicketFlow.TicketService.csproj

	7. dotnet run --project=<root folder path>/TicketFlow.ApiGateway/TicketFlow.ApiGateway.csproj

    8. Run frontend app:
	
	    - cd <root folder path>/frontend/ticketflow
		
		- npm start
	

How to test:

    - Currently, only backend tests are supproted. You can run all tests in solution or test for single *.csproj file. Command for all tests in the solution:
	
	- dotnet test <root folder path>/backend/dotnet/ticketflow/TicketFlow.sln
	

How to use:

    - You can use frontend app at the http://localhost:8000

    - You can use public API at the http://localhost:8080 . Description of the API can be found in docs folder.
	
	- You can find all docs related to the project in docs folder
