import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import createBackendService from "../backend_service/createBackendService";

function LoginPage() {
    const backendService = createBackendService();
    const [state, changeState] = useState({
        callFetch: false,
        fetchResult: "Default"
    });

    const makeFetch = async() => {
        await fetch("http://localhost:8080", {
            method: "GET"
        }).then(response => {
            alert("response");
            changeState(Object.assign({}, state, {fetchResult: response.status}));
        });
        changeState(Object.assign({}, state, {callFetch: false}));
    };

    useEffect(() => {
        if (state.callFetch) {
            makeFetch();
        }
    }, [state]);

    const handleButtonClick = () => {
        changeState(Object.assign({}, state, {callFetch: true}));
    };

    return (
        <div>
            <h3>LoginPage</h3>
            <div>{backendService.login()}</div>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
            <span>{state.fetchResult}</span>
            <button onClick={handleButtonClick}>Send fetch</button>
        </div>
    );
}

export default LoginPage;