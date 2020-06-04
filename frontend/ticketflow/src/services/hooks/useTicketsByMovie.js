import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useTicketsByMovie = (movieId) => {
    const [tickets, setTickets] = useState(undefined);

    useEffect(() => {
        createBackendService()
            .getTicketsByMovie(movieId)
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                else {
                    return new Promise(resolve => resolve(null));
                }
            })
            .then(tickets => setTickets(tickets));
    }, [movieId]);

    return tickets !== undefined 
        ? tickets
        : [];
};

export default useTicketsByMovie;