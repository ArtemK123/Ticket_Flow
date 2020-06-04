import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useTicketsByUser = (token) => {
    const [tickets, setTickets] = useState(null);

    useEffect(() => {
        if (token !== null) {
            createBackendService()
                .getTicketsByUser(token)
                .then(response => response.json())
                .then(tickets => setTickets(tickets));
        }
    }, [token]);

    return tickets !== null 
        ? tickets
        : [];
};

export default useTicketsByUser;