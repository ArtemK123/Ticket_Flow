import React from "react";
import { Link } from "react-router-dom";

function ProfilePage() {
    return (
        <div>
            <h3>ProfilePage
            </h3>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
        </div>
    );
}

export default ProfilePage;