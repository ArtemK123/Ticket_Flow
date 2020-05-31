# Ticket_Flow
Service for booking cinema tickets - Master version

Dependencies:
	
	1. java 14 jdk
	
	2. maven 3.6.3
	
	3. postgres 12
	
	4. rabbitmq 3.8.4

How to run:
	
	- Run postgres server and initialize databases. You can use init-database.sql file from /docs
	
	- Run rabbitmq with default params: (newly installed rabbitmq will have them by default)
		- host: localhost
		- port: 5672
		- username: guest
		- password: guest
	
	- cd ./backend
	
	- mvn clean install
	
	- java -jar ./eureka_server/target/eureka-server-1.0.0.jar
	
	- java -jar ./config_server/target/config-server-1.0.0.jar
	
	- java -jar ./identity_service/target/identity-service-1.0.0.jar
	
	- java -jar ./profile_service/target/profile-service-1.0.0.jar
	
	- java -jar ./ticket_service/target/ticket-service-1.0.0.jar
	
	- java -jar ./movie_service/target/movie-service-1.0.0.jar
	
	- java -jar ./api_gateway/target/api-gateway-1.0.0.jar

How to use:
	You can find public rest api description in /docs/api-description.txt
