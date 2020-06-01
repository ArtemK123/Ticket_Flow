import React from "react";
import { Link } from "react-router-dom";

function LoginPage() {
    return (
        <div>
            <h3>LoginPage</h3>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
        </div>
    );
}

export default LoginPage;