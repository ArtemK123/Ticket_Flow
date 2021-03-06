import { useState, useEffect } from "react";
import createBackendServiceAsync from "services/backend_service/createBackendServiceAsync";

const useMovieById = (id) => {
    const [movie, setMovie] = useState(undefined);

    const defaultMovie = {
        "id": 1,
        "startTime": "2020-06-10T11:30",
        "film": {
            "id": 1,
            "title": "Terminator",
            "description": "",
            "premiereDate": "1980-01-01T00:00",
            "creator": "May be a studio or a single director",
            "duration": "202",
            "ageLimit": "18"
        },
        "cinemaHall": {
            "id": 1,
            "name": "Lux_place",
            "location": "Kyiv",
            "seatRows": 5,
            "seatsInRow": 5
        }
    };

    useEffect(() => {
        createBackendServiceAsync()
            .then(backendService => backendService.getMovieById(id))
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                else {
                    return new Promise(resolve => resolve(null));
                }
            })
            .then(fetchedMovie => setMovie(fetchedMovie));
    }, [id]);

    if (movie === undefined) {
        return defaultMovie;
    }

    return movie;
};

export default useMovieById;