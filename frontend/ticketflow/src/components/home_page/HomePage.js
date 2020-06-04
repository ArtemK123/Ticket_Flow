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
    const dateGroupings = [];
    const dateFormat = { day: "numeric", month: "short", year: "numeric"};

    for (let movie of movies) {
        const key = new Date(movie.startTime).toLocaleString("en-US", dateFormat);
        const grouping = dateGroupings.find(record => record.key === key);
        if (grouping === undefined) {
            dateGroupings.push({
                key: key,
                movies: [movie]
            });
        }
        else {
            grouping.movies.push(movie);
        }
    }

    return dateGroupings.sort((a, b) => new Date(a.key) - new Date(b.key));

    // movies.forEach((movie) => {
    //     movie.startTime = new Date(movie.startTime);
    //     const dateFormat = { day: "numeric", month: "short", year: "numeric"};
    //     const dayInString = movie.startTime.toLocaleString("en-US", dateFormat);

    //     if (dateGroupings[dayInString] === undefined) {
    //         dateGroupings[dayInString] = [movie];
    //     }
    //     else {
    //         dateGroupings[dayInString].push(movie);
    //     }
    // });
    // return dateGroupings;
};

function HomePage(props) {
    const styles = useStyles();
    const moviesByDateGroupingArray = groupMoviesByDay(props.movies);
  
    const moviesPerDateComponents = [];
    moviesByDateGroupingArray.forEach(dateGrouping => {
        moviesPerDateComponents.push(<MoviesPerDate
            key={dateGrouping.key}
            date={new Date(dateGrouping.key)}
            movies={dateGrouping.movies}
        />);
    });

    return (
        <Box w={1}>
            {moviesPerDateComponents}
            <Box className={styles.footerHolder}>
            </Box>
        </Box>
    );
}

export default HomePage;