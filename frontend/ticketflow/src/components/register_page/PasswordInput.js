import React from "react";
import PropTypes from "prop-types";
import TextField from "@material-ui/core/TextField";

PasswordInput.propTypes = {
    isErrorState: PropTypes.bool,
    label: PropTypes.string,
    value: PropTypes.string,
    onChange: PropTypes.func,
};

function PasswordInput(props) {
    if (props.isErrorState) {
        return (
            <TextField
                id="outlined-basic"
                type="password"
                label={props.label}
                value={props.value}
                error
                helperText="Passwords don`t match"
                onChange={props.onChange}
            />
        );
    }
    return (
        <TextField
            id="outlined-basic"
            type="password"
            label={props.label}
            value={props.value}
            onChange={props.onChange}
        />
    );
}

export default PasswordInput;