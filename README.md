# Ticket_Flow
Service for booking cinema tickets - dotnet branch

This is a .Net Core transfer of TicketFlow project. The frontend part is the same, but in backend folder there is a ./dotnet folder with .Net core solution

The consul is used as Service Discovery. The Postgres is used as database, the data scheme is the same as for Java backend.

How to run (Dotnet backend):

	1. git clone https://github.com/ArtemK123/Ticket_Flow

	2. Install and run consul https://www.consul.io/

	3. Install and run postgres https://www.postgresql.org/  . 
	    
		Create the following databases:
		    - ticketflow_identity
			- ticketflow_profile
			- ticketflow_ticket
			- ticketflow_movie
			
		Create the following user in postgres. The password for each user is TicketFlow:
	        - ticketflow_identity_user
			- ticketflow_profile_user
			- ticketflow_ticket_user
			- ticketflow_movie_user

	4. cd ./Ticket_Flow/backend/dotnet/TicketFlow

	5. dotnet run --project=./TicketFlow.IdentityService/TicketFlow.IdentityService.csproj

	6. dotnet run --project=./TicketFlow.ProfileService/TicketFlow.ProfileService.csproj

	7. dotnet run --project=./TicketFlow.MovieService/TicketFlow.MovieService.csproj

	8. dotnet run --project=./TicketFlow.TicketService/TicketFlow.TicketService.csproj

	9. dotnet run --project=./TicketFlow.ApiGateway/TicketFlow.ApiGateway.csproj
