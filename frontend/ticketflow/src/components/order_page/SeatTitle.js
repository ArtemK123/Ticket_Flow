import React from "react";
import PropTypes from "prop-types";
import { Typography } from "@material-ui/core";
import Grid from "@material-ui/core/Grid";

SeatTitle.propTypes = {
    value: PropTypes.string
};

function SeatTitle(props) {
    return (
        <Grid container justify="center" alignItems="center">
            <Grid item>
                <Typography>{props.value}</Typography>
            </Grid>
        </Grid>
    );
}

export default SeatTitle;