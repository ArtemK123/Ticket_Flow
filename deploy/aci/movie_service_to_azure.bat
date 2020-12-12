az container create ^
	--resource-group TicketFlowResourceGroup ^
	--name ticketflow-movie-service ^
	--image darkmode1012/ticketflow_movie-service:1.1.0 ^
	--cpu 1 ^
	--memory 1 ^
	--dns-name-label ticketflow-movie-service ^
	--ports 9004 ^
	--environment-variables ASPNETCORE_ENVIRONMENT=Azure ASPNETCORE_URLS=http://ticketflow-movie-service:9004
	