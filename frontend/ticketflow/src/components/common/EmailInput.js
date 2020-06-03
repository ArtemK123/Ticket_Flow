import React from "react";
import PropTypes from "prop-types";
import TextField from "@material-ui/core/TextField";

EmailInput.propTypes = {
    value: PropTypes.string,
    label: PropTypes.string,
    onChange: PropTypes.func,
    isErrorState: PropTypes.bool,
    helperText: PropTypes.string
};

function EmailInput(props) {
    if (props.isErrorState) {
        return (
            <TextField
                label={props.label}
                value={props.value}
                onChange={event => props.onChange(event)}
                error
                helperText={props.helperText}
            />
        );
    }
    return (
        <TextField
            label={props.label}
            value={props.value}
            onChange={event => props.onChange(event)}
        />
    );
}

export default EmailInput;