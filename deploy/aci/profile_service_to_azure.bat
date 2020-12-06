az container create ^
	--resource-group TicketFlowResourceGroup ^
	--name ticketflow-profile-service ^
	--image darkmode1012/ticketflow_profile-service:1.1.0 ^
	--cpu 1 ^
	--memory 1 ^
	--dns-name-label ticketflow-profile-service ^
	--ports 9002 ^
	--environment-variables ASPNETCORE_ENVIRONMENT=Azure ASPNETCORE_URLS=http://ticketflow-profile-service:9002
	