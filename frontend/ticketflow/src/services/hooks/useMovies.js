import { useState, useEffect } from "react";
import createBackendServiceAsync from "services/backend_service/createBackendServiceAsync";

const useMovies = () => {
    const [movies, setMovies] = useState(null);

    useEffect(() => {
        if (movies === null) {
            createBackendServiceAsync()
                .then(backendService => backendService.getMovies())
                .then(response => response.json())
                .then(fetchedMovies => {
                    setMovies(fetchedMovies);
                });
        }
    }, [movies]);

    return movies !== null ? movies : [];
};

export default useMovies;