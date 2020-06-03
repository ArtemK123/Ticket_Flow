import React from "react";
import PropTypes from "prop-types";
import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";

HeaderUserPart.propTypes = {
    isUserLoggedIn: PropTypes.bool,
    username: PropTypes.string,
    style: PropTypes.string,
    loginAction: PropTypes.func,
    registerAction: PropTypes.func,
    profileAction: PropTypes.func,
    logoutAction: PropTypes.func
};

function HeaderUserPart(props) {
    if (props.isUserLoggedIn) {
        return (
            <Grid container justify="flex-end" alignItems="center" className={props.style}>
                <h4>{props.username}</h4>
                <Button onClick={props.profileAction}>Profile</Button>
                <Button onClick={props.logoutAction}>Log out</Button>
            </Grid> 
        );
    }
    else {
        return (
            <Grid container justify="flex-end" alignItems="center" className={props.style}>
                <Button onClick={props.loginAction}>Login</Button>
                <Button onClick={props.registerAction}>Register</Button>
            </Grid> 
        );
    }
};


export default HeaderUserPart;