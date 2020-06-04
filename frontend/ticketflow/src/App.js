import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import "./App.css";
import HomePage from "./components/home_page/HomePage";
import LoginPage from "components/login_page/LoginPage.js";
import RegisterPage from "components/register_page/RegisterPage";
import ProfilePage from "./components/profile_page/ProfilePage";
import MoviePage from "./components/movie_page/MoviePage";
import OrderPage from "./components/order_page/OrderPage";
import Header from "./components/header/Header";
import Footer from "./components/footer/Footer";
import NotFoundPage from "./components/not_found_page/NotFoundPage";
import useProfileModel from "services/hooks/useProfileModel";
import useTicketsByUser from "services/hooks/useTicketsByUser";
import { Link } from "react-router-dom";
import Box from "@material-ui/core/Box";
import useMovies from "services/hooks/useMovies";

function App() {
    const [userState, changeUserState] = useState({
        isLoggedIn: false,
        username: "",
        token: null
    });

    const profileResponse = useProfileModel(userState.token);
    const ticketsByUser = useTicketsByUser(userState.token);
    const movies = useMovies();

    useEffect(() => {
        if (profileResponse !== null && profileResponse.success === false) {
            localStorage.removeItem("token");
            localStorage.removeItem("username");
            changeUserState(Object.assign({}, userState, {
                isLoggedIn: false,
                username: "",
                token: null
            }));
        }

        const storedToken = localStorage.getItem("token");
        const storedUsername = localStorage.getItem("username");

        const newUserState = {
            isLoggedIn: storedToken !== null,
            username: storedUsername !== null ? storedUsername : "",
            token: storedToken
        };

        if (JSON.stringify(newUserState) !== JSON.stringify(userState)) {
            changeUserState(newUserState);
        }
    }, [profileResponse, userState]);

    const reload = () => {
        changeUserState(Object.assign({}, userState));
    };

    return (
        <Box width={1}>
            <Router>
                <Header
                    isUserLoggedIn={userState.isLoggedIn}
                    username={userState.username}
                    reloadParent={reload}
                />
                <Switch>
                    <Route exact path="/">
                        <HomePage 
                            movies={movies}
                        />
                    </Route>
                    <Route path="/login">
                        <LoginPage 
                            reloadParent={reload}
                        />
                    </Route>
                    <Route path="/register" component={RegisterPage} />
                    <Route path="/profile">
                        <ProfilePage 
                            isUserLoggedIn={userState.isLoggedIn}
                            profileModel={profileResponse.profileModel}
                            tickets={ticketsByUser}
                        />
                    </Route>
                    <Route path="/movie" component={MoviePage} />
                    <Route path="/order" component={OrderPage} />
                    <Route component={NotFoundPage} />
                </Switch>
                <Footer/>
            </Router>
        </Box>
    );
}

export default App;