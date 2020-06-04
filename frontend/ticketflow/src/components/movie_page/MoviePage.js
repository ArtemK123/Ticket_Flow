import React, { useState } from "react";
import PropTypes from "prop-types";
import useMovieById from "services/hooks/useMovieById";
import { Box, Button, Grid, Typography, TextField } from "@material-ui/core";
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
        <Box w={1}>
            <Grid container direction="row">
                <Grid item xs={1}/>
                <Grid item xs={9}>
                    <Box m={5}>
                        <Grid container direction="column" spacing={2}>
                            <Grid item><Typography variant="h4">{movie.film.title}</Typography></Grid>
                            <Grid item>
                                <Typography>Location: {`${movie.cinemaHall.name} (${movie.cinemaHall.location})`}</Typography>
                            </Grid>
                            <Grid item>
                                <Typography>Duration: {`${movie.film.duration} min`}</Typography>
                            </Grid>
                            <Grid item>
                                <Typography>Creator: {`${movie.film.creator}`}</Typography>
                            </Grid>
                            <Grid item>
                                <Typography>Premiere date: {`${movie.film.premiereDate}`}</Typography>
                            </Grid>
                            <Grid item>
                                <Typography>Age limit: {`${movie.film.ageLimit}+`}</Typography>
                            </Grid>
                            <Grid item container direction="column">
                                <Grid item>
                                    <Typography>Description:</Typography>
                                </Grid>
                                <Grid item>
                                    <TextField
                                        defaultValue={movie.film.description}
                                        variant="outlined"
                                        multiline
                                        rowsMax={10}
                                        fullWidth="75%"
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />
                                </Grid>
                            </Grid>
                            <Grid item>
                                <Button variant="contained" color="primary" onClick={() => changeRedirectToOrderState(true)}>Order tickets</Button>
                            </Grid>
                        </Grid>
                    </Box>
                </Grid>
                <Grid item xs={2}/>
            </Grid>
        </Box>
    );
}

export default MoviePage;