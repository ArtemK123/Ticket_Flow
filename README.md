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
 
	1. Build .Net solution:
	    
		- dotnet build <root folder path>/backend/dotnet/ticketflow/TicketFlow.sln
		
	2. 	Build webclient:
	    
		- cd <root folder path>/frontend/ticketflow
		
		- npm install


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
			
		Note: You can use init-database.sql file from /docs folder

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

    - You can use frontend app at the http://localhost:3000

    - You can use public API at the http://localhost:8080 . Description of the API can be found in docs folder.
	
	- You can find all docs related to the project in docs folder
