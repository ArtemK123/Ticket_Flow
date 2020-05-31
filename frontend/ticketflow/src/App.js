import React from 'react';
import './App.css';
import LoginPage from './components/login/LoginPage.js';

import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link
  } from "react-router-dom";

function App() {
    return (
        <div>
            <Router>
                <ul>
                    <li>
                        <Link to="/login">Login page</Link>
                    </li>
                </ul>

                <Switch>
                    <Route exact path="/login">
                        <LoginPage />
                    </Route>
                </Switch>
            </Router>
        </div>
    );
}

export default App;