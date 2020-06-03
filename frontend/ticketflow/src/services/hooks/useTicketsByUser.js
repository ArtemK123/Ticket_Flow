import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useTicketsByUser = (token) => {
    const [tickets, setTickets] = useState(null);
    const backendService = createBackendService();

    const fetchTickets = (token) => {
        backendService
            .getTicketsByUser(token)
            .then(response => response.json())
            .then(tickets => setTickets(tickets));
    };

    useEffect(() => {
        if (token !== null && tickets === null) {
            fetchTickets(token);
        }
    });

    return tickets !== null 
        ? tickets
        : [];
};

export default useTicketsByUser;