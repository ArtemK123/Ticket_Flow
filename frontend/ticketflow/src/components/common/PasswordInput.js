import React from "react";
import PropTypes from "prop-types";
import TextField from "@material-ui/core/TextField";

PasswordInput.propTypes = {
    label: PropTypes.string,
    value: PropTypes.string,
    onChange: PropTypes.func,
    isErrorState: PropTypes.bool,
    helperText: PropTypes.string
};

function PasswordInput(props) {
    if (props.isErrorState) {
        return (
            <TextField
                type="password"
                label={props.label}
                value={props.value}
                onChange={props.onChange}
                error
                helperText={props.helperText}
            />
        );
    }
    return (
        <TextField
            type="password"
            label={props.label}
            value={props.value}
            onChange={props.onChange}
        />
    );
}

export default PasswordInput;