az container create ^
	--resource-group TicketFlowResourceGroup ^
	--name ticketflow-ticket-service ^
	--image darkmode1012/ticketflow_ticket-service:1.0.1 ^
	--cpu 1 ^
	--memory 1 ^
	--dns-name-label ticketflow-ticket-service ^
	--ports 9003 ^
	--environment-variables ASPNETCORE_ENVIRONMENT=Azure ASPNETCORE_URLS=http://ticketflow-ticket-service:9003
	