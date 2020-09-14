# Ticket_Flow
Service for booking cinema tickets - Java_docker branch

This version uses Java backend and Docker for containerzation. Configs for project are fetched from github repo: - https://github.com/ArtemK123/Ticket_Flow_Configs

Dependencies:

    - Docker: https://docs.docker.com/docker-for-windows/install/

How to run:
	
	1. git clone -b java_docker https://github.com/ArtemK123/Ticket_Flow

	2. cd ./Ticket_Flow
	
	3. docker-compose -f docker-compose-java.yml build -q --parallel
	
	4. docker-compose -f docker-compose-java.yml up
	
How to use:

    - You can use frontend app at the http://localhost:3000

    - You can use public API at the http://localhost:8080 . Description of the API can be found in docs folder.
	
	- You can find all docs related to the project in docs folder