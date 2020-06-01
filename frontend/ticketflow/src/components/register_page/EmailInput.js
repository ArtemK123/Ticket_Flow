import React from "react";
import PropTypes from "prop-types";
import TextField from "@material-ui/core/TextField";

EmailInput.propTypes = {
    isErrorState: PropTypes.bool,
    value: PropTypes.string,
    onChange: PropTypes.func,
};

function EmailInput(props) {
    if (props.isErrorState) {
        return (
            <TextField
                id="outlined-basic"
                label="Email"
                value={props.value}
                onChange={event => props.onChange(event)}
                error
                helperText="User with given email already exists"
            />
        );
    }
    return (
        <TextField
            id="outlined-basic"
            label="Email"
            value={props.value}
            onChange={event => props.onChange(event)}
        />
    );
}

export default EmailInput;