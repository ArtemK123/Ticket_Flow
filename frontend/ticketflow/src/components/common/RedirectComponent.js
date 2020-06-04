import React from "react";
import { Redirect } from "react-router-dom";
import PropTypes from "prop-types";

RedirectComponent.propTypes = {
    link: PropTypes.string
};

function RedirectComponent(props) {
    if (props.link !== undefined) {
        return (<Redirect to={props.link}/>);
    } 
    else {
        return null;
    }
}

export default RedirectComponent;