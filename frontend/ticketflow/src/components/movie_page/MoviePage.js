import React, {useState} from "react";
import { Typography } from "@material-ui/core";
import PropTypes from "prop-types";
import useMovieById from "services/hooks/useMovieById";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import { Redirect } from "react-router-dom";

MoviePage.propTypes = {
    location: PropTypes.shape({
        state: PropTypes.shape({
            id: PropTypes.number
        })
    })
};

function MoviePage(props) {
    const movieId = props.location.state !== undefined ? props.location.state.id : -1;
    const [redirectToOrder, changeRedirectToOrderState] = useState(false);
    const movie = useMovieById(movieId);

    if (movie === null) {
        return <Redirect to="/not-found"/>;
    }

    if (redirectToOrder) {
        return <Redirect to={{
            pathname: `/order/${movieId}`,
            state: {movieId: movieId}
        }}/>;
    }

    return (
        <Box spacing={2}>
            <Typography variant="h4">{movie.film.title}</Typography>
            <Typography>Location: {`${movie.cinemaHall.name} (${movie.cinemaHall.location})`}</Typography>
            <Typography>Duration: {`${movie.film.duration}`}</Typography>
            <Typography>Creator: {`${movie.film.creator}`}</Typography>
            <Typography>Premiere date: {`${movie.film.premiereDate}`}</Typography>
            <Typography>Age limit: {`${movie.film.ageLimit}+`}</Typography>
            <Typography>Discription: {`${movie.film.description}`}</Typography>
            <Button variant="contained" color="primary" onClick={() => changeRedirectToOrderState(true)}>Order tickets</Button>
        </Box>
    );
}

export default MoviePage;