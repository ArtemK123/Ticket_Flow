import React from "react";
import { TextField } from "@material-ui/core";
import PropTypes from "prop-types";

ReadonlyTextInput.propTypes = {
    label: PropTypes.string,
    value: PropTypes.string,
    isMultiline: PropTypes.bool,
};

function ReadonlyTextInput(props) {
    return (
        <TextField
            label={props.label}
            defaultValue={props.value}
            variant="outlined"
            multiline={props.isMultiline}
            InputProps={{
                readOnly: true
            }}
        />
    );
}

export default ReadonlyTextInput;