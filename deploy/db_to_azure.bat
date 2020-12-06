az container create ^
	--resource-group TicketFlowResourceGroup ^
	--name ticketflow-db ^
	--image darkmode1012/ticketflow_db:1.0.0 ^
	--cpu 1 ^
	--memory 1 ^
	--dns-name-label ticketflow-db ^
	--ports 5432 ^
	--environment-variables POSTGRES_USER=postgres POSTGRES_DB=postgres POSTGRES_HOST_AUTH_METHOD=trust
	