az webapp create ^
	--resource-group TicketFlowAppServiceResourceGroup ^
	--plan TicketFlowComposeAppServicePlan ^
	--name darkmode-ticketflow ^
	--multicontainer-config-type compose ^
	--multicontainer-config-file ../../docker-compose-dockerhub.yml