#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE USER ticketflow_identity_user;
	CREATE USER ticketflow_profile_user;
	CREATE USER ticketflow_ticket_user;
	CREATE USER ticketflow_movie_user;

	ALTER USER ticketflow_identity_user WITH PASSWORD 'TicketFlow';
	ALTER USER ticketflow_profile_user WITH PASSWORD 'TicketFlow';
	ALTER USER ticketflow_ticket_user WITH PASSWORD 'TicketFlow';
	ALTER USER ticketflow_movie_user WITH PASSWORD 'TicketFlow';

	CREATE DATABASE ticketflow_identity WITH OWNER=ticketflow_identity_user;
	CREATE DATABASE ticketflow_profile WITH OWNER=ticketflow_profile_user;
	CREATE DATABASE ticketflow_ticket WITH OWNER=ticketflow_ticket_user;
	CREATE DATABASE ticketflow_movie WITH OWNER=ticketflow_movie_user;
EOSQL