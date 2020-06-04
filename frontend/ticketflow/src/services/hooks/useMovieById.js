import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useMovieById = (id) => {
    const [movie, setMovie] = useState(null);

    const defaultMovie = {
        "id": 1,
        "startTime": "2020-06-10T11:30",
        "film": {
            "id": 1,
            "title": "Terminator",
            "description": "Lorem ipsum (may be quite big)",
            "premiereDate": "1980-01-01",
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
        createBackendService()
            .getMovieById(id)
            .then(response => response.json())
            .then(fetchedMovie => setMovie(fetchedMovie));
    }, [id]);

    return movie !== null ? movie : defaultMovie;
};

export default useMovieById;