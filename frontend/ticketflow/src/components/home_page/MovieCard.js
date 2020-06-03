import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import PropTypes from "prop-types";
import { Typography } from "@material-ui/core";
import Box from "@material-ui/core/Box";

MovieCard.propTypes = {
    movie: PropTypes.shape({
        id: PropTypes.number,
        title: PropTypes.string,
        startTime: PropTypes.instanceOf(Date),
        cinemaHallName: PropTypes.string
    })
};

const useStyles = makeStyles(() => ({
    paper: {
        height: 270,
        width: 230,
        "&:hover": {
            backgroundColor: "HoneyDew"
        }
    },
}));

const getTimeFromDate = (date) => {
    let hours = date.getHours();
    hours = hours > 9 ? hours : "0" + hours;

    let minutes = date.getMinutes();
    minutes = minutes > 9 ? minutes : "0" + minutes;

    return `${hours}:${minutes}`;
};

function MovieCard(props) {
    const styles = useStyles();

    return (
        <Paper className={styles.paper} onClick="">
            <Box p={1}>
                <Typography variant="h5">{props.movie.title}</Typography>
                <Typography>Cinema hall: {props.movie.cinemaHallName}</Typography>
                <Typography>Time: {getTimeFromDate(props.movie.startTime)}</Typography>
            </Box>
        </Paper>
    );
}

export default MovieCard;