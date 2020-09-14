# Ticket_Flow
Service for booking cinema tickets - java_local branch

Dependencies:

	1. java 14 jdk

	2. maven 3.6.3
	
	3. postgres 12
	
	4. rabbitmq 3.8.4
	
	5. node 12.17

Configs for project are fetched from github repo:
	- https://github.com/ArtemK123/Ticket_Flow_Configs

How to run:
	
	- git clone -b java_local https://github.com/ArtemK123/Ticket_Flow
	
	- Run postgres server and initialize databases. You can use init-database.sql file from /docs
	
	- Run rabbitmq with default params: (newly installed rabbitmq will have them by default)
		- host: localhost
		- port: 5672
		- username: guest
		- password: guest
	
	- cd <root folder path>/backend/java
	
	- mvn clean install
	
	- java -jar ./eureka_server/target/eureka-server-1.0.0.jar
	
	- java -jar ./config_server/target/config-server-1.0.0.jar
	
	- java -jar ./identity_service/target/identity-service-1.0.0.jar
	
	- java -jar ./profile_service/target/profile-service-1.0.0.jar
	
	- java -jar ./ticket_service/target/ticket-service-1.0.0.jar
	
	- java -jar ./movie_service/target/movie-service-1.0.0.jar
	
	- java -jar ./api_gateway/target/api-gateway-1.0.0.jar
	
	- cd <root folder path>/frontend/ticketflow
	
	- npm install
	
	- npm start

API:

	- public rest api: localhost:8080
	
	- react client: localhost:3000

How to use:

    - You can use frontend app at the http://localhost:3000

    - You can use public API at the http://localhost:8080 . Description of the API can be found in docs folder.
	
	- You can find all docs related to the project in docs folder
