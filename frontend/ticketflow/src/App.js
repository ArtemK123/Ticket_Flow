import React, { useState, useEffect } from "react";
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
import useProfileModel from "services/hooks/useProfileModel";
import useTicketsByUser from "services/hooks/useTicketsByUser";

function App() {
    const [userState, changeUserState] = useState({
        isLoggedIn: false,
        username: ""
    });

    const storedToken = localStorage.getItem("token");
    const profileModel = useProfileModel(storedToken);
    const ticketsByUser = useTicketsByUser(storedToken);

    useEffect(() => {
        const storedToken = localStorage.getItem("token");
        const storedUsername = localStorage.getItem("username");

        const newUserState = {
            isLoggedIn: storedToken !== null,
            username: storedUsername !== null ? storedUsername : ""
        };

        if (JSON.stringify(newUserState) !== JSON.stringify(userState)) {
            changeUserState(newUserState);
        }
    }, [userState]);

    const reload = () => {
        changeUserState(Object.assign({}, userState));
    };

    return (
        <div>
            <Router>
                <Header
                    isUserLoggedIn={userState.isLoggedIn}
                    username={userState.username}
                    reloadParent={reload}
                />
                <Switch>
                    <Route exact path="/" component={HomePage} />
                    <Route path="/login">
                        <LoginPage 
                            reloadParent={reload}
                        />
                    </Route>
                    <Route path="/register" component={RegisterPage} />
                    <Route path="/profile">
                        <ProfilePage 
                            isUserLoggedIn={userState.isLoggedIn}
                            profileModel={profileModel}
                            tickets={ticketsByUser}
                        />
                    </Route>
                    <Route path="/movie" component={MoviePage} />
                    <Route path="/order" component={OrderPage} />
                    <Route component={NotFoundPage} />
                </Switch>
                <Footer/>
            </Router>
        </div>
    );
}

export default App;