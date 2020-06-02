import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import "./App.css";
import HomePage from "./components/home_page/HomePage";
import LoginPage from "components/auth_pages/login_page/LoginPage.js";
import RegisterPage from "components/auth_pages/register_page/RegisterPage";
import ProfilePage from "./components/profile_page/ProfilePage";
import MoviePage from "./components/movie_page/MoviePage";
import OrderPage from "./components/order_page/OrderPage";
import Header from "./components/header/Header";
import Footer from "./components/footer/Footer";
import NotFoundPage from "./components/not_found_page/NotFoundPage";

function App() {
    return (
        <div>
            <Header></Header>
            <Router>
                <Switch>
                    <Route exact path="/" component={HomePage} />
                    <Route path="/login" component={LoginPage} />
                    <Route path="/register" component={RegisterPage} />
                    <Route path="/profile" component={ProfilePage} />
                    <Route path="/movie" component={MoviePage} />
                    <Route path="/order" component={OrderPage} />
                    <Route component={NotFoundPage} />
                </Switch>
            </Router>
            <Footer></Footer>
        </div>
    );
}

export default App;