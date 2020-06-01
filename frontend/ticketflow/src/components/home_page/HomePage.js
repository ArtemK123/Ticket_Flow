import React from "react";
import { Link } from "react-router-dom";

function HomePage() {
    return (
        <div>
            <h3>HomePage</h3>
            <ul>
                <li><Link to="/login">LoginPage</Link></li>
                <li><Link to="/register">RegisterPage</Link></li>
                <li><Link to="/profile">ProfilePage</Link></li>
                <li><Link to="/movie">MoviePage</Link></li>
                <li><Link to="/order">OrderPage</Link></li>
            </ul>
        </div>
    );
}

export default HomePage;