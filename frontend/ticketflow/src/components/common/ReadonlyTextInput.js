import React from "react";
import { TextField } from "@material-ui/core";
import PropTypes from "prop-types";

ReadonlyTextInput.propTypes = {
    label: PropTypes.string,
    value: PropTypes.string,
    isMultiline: PropTypes.bool,
};

function ReadonlyTextInput(props) {
    if (props.isMultiline) {
        return (
            <TextField
                label={props.label}
                defaultValue={props.value}
                variant="outlined"
                multiline
                InputProps={{
                    readOnly: true
                }}
            />
        ); 
    }
    
    return (
        <TextField
            label={props.label}
            defaultValue={props.value}
            variant="outlined"
            InputProps={{
                readOnly: true
            }}
        />
    );
}

export default ReadonlyTextInput;