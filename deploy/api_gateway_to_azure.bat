az container create ^
	--resource-group TicketFlowResourceGroup ^
	--name ticketflow-api-gateway ^
	--image darkmode1012/ticketflow_api-gateway:1.0.1 ^
	--cpu 1 ^
	--memory 1 ^
	--dns-name-label ticketflow-api-gateway ^
	--ports 8080 ^
	--environment-variables ASPNETCORE_ENVIRONMENT=Azure ASPNETCORE_URLS=http://ticketflow-api-gateway:8080
	