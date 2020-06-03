import React from "react";
import Box from "@material-ui/core/Box";
import Grid from "@material-ui/core/Grid";
import { Typography } from "@material-ui/core";
import { Divider } from "@material-ui/core";
import PropTypes from "prop-types";
import MovieCard from "components/home_page/MovieCard";

MoviesPerDate.propTypes = {
    date: PropTypes.instanceOf(Date),
    movies: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.number,
        title: PropTypes.string,
        startTime: PropTypes.instanceOf(Date),
        cinemaHallName: PropTypes.string
    })),
};

function MoviesPerDate(props) {
    
    const movieCardComponents = [];
    props.movies.forEach(movie => {
        movieCardComponents.push(<Grid item><MovieCard movie={movie}/></Grid>);
    });

    return (
        <Box border={1} p={2}>
            <Grid container>
                <Grid item container justify="space-between" xs={3}>
                    <Grid item>
                        <Typography>{props.date.toLocaleString("en-US", { day: "numeric", month: "short"})}</Typography>
                        <Typography>{props.date.toLocaleString("en-US", { weekday: "long" })}</Typography>
                    </Grid>
                    <Grid item>
                        <Divider orientation="vertical"/>
                    </Grid>
                    <Grid item/>
                </Grid>
                <Grid item container xs={9} spacing={2}>
                    {movieCardComponents}
                </Grid>
            </Grid>
        </Box>
    );
};

export default MoviesPerDate;