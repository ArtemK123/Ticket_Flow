import React from "react";
import { Typography } from "@material-ui/core";
import PropTypes from "prop-types";

MoviePage.propTypes = {
    location: PropTypes.shape({
        state: PropTypes.shape({
            id: PropTypes.number
        })
    })
};

function MoviePage(props) {
    return (
        <div>
            <h3>MoviePage</h3>
            <Typography>{props.location.state ? props.location.state.id : "undefined"}</Typography>
        </div>
    );
}

export default MoviePage;