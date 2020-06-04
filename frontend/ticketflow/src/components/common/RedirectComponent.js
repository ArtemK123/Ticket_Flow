import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import PropTypes from "prop-types";

RedirectComponent.propTypes = {
    link: PropTypes.string,
    redirectCallback: PropTypes.func
};

function RedirectComponent(props) {
    const history = useHistory();

    useEffect(() => {
        if (props.link !== undefined) {
            history.push(props.link);
            props.redirectCallback(undefined);
        }
    });

    return null;
}

export default RedirectComponent;