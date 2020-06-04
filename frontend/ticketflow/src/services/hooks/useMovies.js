import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useMovies = () => {
    const [movies, setMovies] = useState(null);
    const backendService = createBackendService();

    useEffect(() => {
        if (movies === null) {
            backendService
                .getMovies()
                .then(response => response.json())
                .then(fetchedMovies => {
                    setMovies(fetchedMovies);
                });
        }
    }, [movies, backendService]);

    return movies !== null ? movies : [];
};

export default useMovies;