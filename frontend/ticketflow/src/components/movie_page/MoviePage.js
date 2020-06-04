import React from "react";
import { Typography } from "@material-ui/core";
import PropTypes from "prop-types";
import useMovieById from "services/hooks/useMovieById";
import Box from "@material-ui/core/Box";

MoviePage.propTypes = {
    location: PropTypes.shape({
        state: PropTypes.shape({
            id: PropTypes.number
        })
    })
};

function MoviePage(props) {
    const movie = useMovieById(props.location.state.id);

    return (
        <Box spacing={2}>
            <Typography variant="h4">{movie.film.title}</Typography>
            <Typography>Location: {`${movie.cinemaHall.name} (${movie.cinemaHall.location})`}</Typography>
            <Typography>Duration: {`${movie.film.duration}`}</Typography>
            <Typography>Creator: {`${movie.film.creator}`}</Typography>
            <Typography>Premiere date: {`${movie.film.premiereDate}`}</Typography>
            <Typography>Age limit: {`${movie.film.ageLimit}+`}</Typography>
            <Typography>Discription: {`${movie.film.description}`}</Typography>
        </Box>
    );
}

export default MoviePage;