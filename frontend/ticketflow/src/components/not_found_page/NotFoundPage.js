import React from "react";
import { Link } from "react-router-dom";

function NotFoundPage() {
    return (
        <div>
            <h3>NotFoundPage</h3>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
        </div>
    );
}

export default NotFoundPage;