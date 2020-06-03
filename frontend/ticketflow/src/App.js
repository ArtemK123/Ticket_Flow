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

function App() {
    const [userState, changeUserState] = useState({
        isLoggedIn: false,
        username: ""
    });

    const [profileModel, changeProfileModel] = useState({
        email: "",
        profile: {
            phoneNumber: 111,
            birthday: ""
        }
    });
    const storedToken = localStorage.getItem("token");

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

    const fetchedProfileModel = useProfileModel(storedToken);

    useEffect(() => {
        if (fetchedProfileModel !== null && JSON.stringify(profileModel) !== JSON.stringify(fetchedProfileModel)) {
            changeProfileModel(fetchedProfileModel);
        }
    }, [fetchedProfileModel, profileModel]);

    const reload = () => {
        changeUserState(Object.assign({}, userState));
    };


    const getTickets = () => [
        "213123213: Movie1. 2019-01-11 22:00. 20$",
        "213123213: Movie1. 2019-01-11 22:00. 20$",
        "213123213: Movie1. 2019-01-11 22:00. 20$"
    ];

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
                            tickets={getTickets()}
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