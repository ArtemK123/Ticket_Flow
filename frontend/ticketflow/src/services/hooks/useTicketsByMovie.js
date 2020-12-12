import { useState, useEffect } from "react";
import createBackendServiceAsync from "services/backend_service/createBackendServiceAsync";

const useTicketsByMovie = (movieId) => {
    const [tickets, setTickets] = useState(undefined);

    useEffect(() => {
        createBackendServiceAsync()
            .then(backendService => backendService.getTicketsByMovie(movieId))
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                else {
                    return new Promise(resolve => resolve(null));
                }
            })
            .then(ticketsArg => setTickets(ticketsArg));
    }, [movieId]);

    return tickets !== undefined 
        ? tickets
        : [];
};

export default useTicketsByMovie;