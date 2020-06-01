import React from "react";
import { Link } from "react-router-dom";
import createBackendService from "../backend_service/createBackendService";

function LoginPage() {
    const backendService = createBackendService();

    return (
        <div>
            <h3>LoginPage</h3>
            <div>{backendService.login()}</div>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
        </div>
    );
}

export default LoginPage;