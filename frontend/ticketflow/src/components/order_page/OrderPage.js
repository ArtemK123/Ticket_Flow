import React from "react";
import { Link } from "react-router-dom";

function OrderPage() {
    const token = localStorage.getItem("token");

    return (
        <div>
            <h3>OrderPage</h3>
            <span>Token is {token ? token : (token === undefined ? "undefined" : "null")}</span>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
        </div>
    );
}

export default OrderPage;