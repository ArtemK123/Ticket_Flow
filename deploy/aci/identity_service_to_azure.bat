az container create ^
	--resource-group TicketFlowResourceGroup ^
	--name ticketflow-identity-service ^
	--image darkmode1012/ticketflow_identity-service:1.0.2 ^
	--cpu 1 ^
	--memory 1 ^
	--dns-name-label ticketflow-identity-service ^
	--ports 9001 ^
	--environment-variables ASPNETCORE_ENVIRONMENT=Azure ASPNETCORE_URLS=http://ticketflow-identity-service:9001
	