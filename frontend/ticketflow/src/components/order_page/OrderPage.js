import React from "react";
import { Link } from "react-router-dom";

function OrderPage() {
    const token = localStorage.getItem("token");
    const username = localStorage.getItem("username");

    return (
        <div>
            <h3>OrderPage</h3>
            <p>Token is {token ? token : (token === undefined ? "undefined" : "null")}</p>
            <p>Username is {username ? username : (username === undefined ? "undefined" : "null")}</p>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
        </div>
    );
}

export default OrderPage;