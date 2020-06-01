import React from "react";
import { useHistory } from "react-router-dom";
import Button from "@material-ui/core/Button";

function RegisterPage() {

    const history = useHistory();
    return (
        <div>
            <h3>RegisterPage</h3>
            <Button variant="contained" color="primary" onClick={() => { history.push("/"); }}>Go to Home Page</Button>
        </div>
    );
}

export default RegisterPage;