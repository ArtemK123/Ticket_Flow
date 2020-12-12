az container create ^
	--resource-group TicketFlowResourceGroup ^
	--name ticketflow-consul ^
	--image consul:1.8.5 ^
	--cpu 1 ^
	--memory 1 ^
	--dns-name-label ticketflow-consul ^
	--ports 8500 ^
	