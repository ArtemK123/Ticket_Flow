import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import "./App.css";
import HomePage from "./components/home_page/HomePage";
import LoginPage from "./components/login_page/LoginPage.js";
import RegisterPage from "./components/register_page/RegisterPage";
import ProfilePage from "./components/profile_page/ProfilePage";
import MoviePage from "./components/movie_page/MoviePage";
import OrderPage from "./components/order_page/OrderPage";
import Header from "./components/header/Header";
import Footer from "./components/footer/Footer";

function App() {
    return (
        <div>
            <Header></Header>
            <Router>
                <Switch>
                    <Route exact path="/">
                        <HomePage />
                    </Route>
                    <Route path="/login">
                        <LoginPage />
                    </Route>
                    <Route path="/register">
                        <RegisterPage />
                    </Route>
                    <Route path="/profile">
                        <ProfilePage />
                    </Route>
                    <Route path="/movie">
                        <MoviePage />
                    </Route>
                    <Route path="/order">
                        <OrderPage />
                    </Route>
                </Switch>
            </Router>
            <Footer></Footer>
        </div>
    );
}

export default App;