# Ticket_Flow
Service for booking cinema tickets - Master branch

The Docker containerization is used, so you should install Docker if you want to work with application.
As alternative, you can use version from local branch 

Configs for project are fetched from github repo: - https://github.com/ArtemK123/Ticket_Flow_Configs

How to run (Java backend):
	
	1. git clone https://github.com/ArtemK123/Ticket_Flow

	2. cd ./Ticket_Flow
	
	3. docker-compose -f docker-compose-java.yml build -q --parallel
	
	4. docker-compose -f docker-compose-java.yml up