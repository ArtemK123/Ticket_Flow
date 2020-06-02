import React from "react";
import PropTypes from "prop-types";
import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";

HeaderUserPart.propTypes = {
    isUserLoggedIn: PropTypes.bool,
    username: PropTypes.string,
    style: PropTypes.object
};

function HeaderUserPart(props) {
    if (props.isUserLoggedIn) {
        return (
            <Grid container justify="flex-end" alignItems="center" className={props.style}>
                <h4>{props.username}</h4>
                <Button>Profile</Button>
                <Button>Log out</Button>
            </Grid> 
        );
    }
    else {
        return (
            <Grid container justify="flex-end" alignItems="center" className={props.style}>
                <Button>Login</Button>
                <Button>Register</Button>
            </Grid> 
        );
    }
};


export default HeaderUserPart;