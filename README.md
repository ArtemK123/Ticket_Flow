# Ticket_Flow
Service for booking cinema tickets - test branch

This branch is created for covering .Net core backend of the TicketFlow app by unit/integration tests.


Dependencies:

 - .Net Core 3.1: https://dotnet.microsoft.com/download/dotnet-core/3.1
 
 - PosrgreSQL: https://www.postgresql.org/download/
 
 - Consul: https://www.consul.io/downloads
 
 - NodeJS (for ReactJS): https://nodejs.org/en/download/


How to clone:

    - git clone -b dotnet_test https://github.com/ArtemK123/Ticket_Flow


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
