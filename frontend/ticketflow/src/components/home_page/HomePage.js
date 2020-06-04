import React from "react";
import Box from "@material-ui/core/Box";
import PropTypes from "prop-types";
import MoviesPerDate from "components/home_page/MoviesPerDate";
import { makeStyles } from "@material-ui/core/styles";

HomePage.propTypes = {
    movies: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.number,
        title: PropTypes.string,
        startTime: PropTypes.string,
        cinemaHallName: PropTypes.string
    })),
};

const useStyles = makeStyles(() => ({
    footerHolder: {
        height: 100
    },
}));

const groupMoviesByDay = (movies) => {
    const dateGroupings = {};
    movies.forEach((movie) => {
        movie.startTime = new Date(movie.startTime);
        const dateFormat = { day: "numeric", month: "short", year: "numeric"};
        const dayInString = movie.startTime.toLocaleString("en-US", dateFormat);

        if (dateGroupings[dayInString] === undefined) {
            dateGroupings[dayInString] = [movie];
        }
        else {
            dateGroupings[dayInString].push(movie);
        }
    });
    return dateGroupings;
};

function HomePage(props) {
    const styles = useStyles();
    const moviesByDate = groupMoviesByDay(props.movies);
  
    const moviesPerDateComponents = [];
    Object.entries(moviesByDate).forEach(([dateString, moviesArray]) => {
        moviesPerDateComponents.push(<MoviesPerDate
            key={dateString}
            date={new Date(dateString)}
            movies={moviesArray}
        />);
    });

    return (
        <Box>
            {moviesPerDateComponents}
            <Box className={styles.footerHolder}>
            </Box>
        </Box>
    );
}

export default HomePage;