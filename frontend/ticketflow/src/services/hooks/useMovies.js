import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useMovies = () => {
    const [movies, setMovies] = useState(null);
    const backendService = createBackendService();

    const fetchMovies = () => {
        backendService
            .getMovies()
            .then(response => response.json())
            .then(fetchedMovies => {
                setMovies(fetchedMovies);
            });
    };

    useEffect(() => {
        if (movies === null) {
            fetchMovies();
        }
    }, [movies]);

    return movies !== null ? movies : [];
};

export default useMovies;