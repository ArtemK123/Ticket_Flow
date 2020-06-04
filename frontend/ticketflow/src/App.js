import React, { useState, useEffect, useCallback } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import "./App.css";
import HomePage from "components/home_page/HomePage";
import LoginPage from "components/login_page/LoginPage.js";
import RegisterPage from "components/register_page/RegisterPage";
import ProfilePage from "components/profile_page/ProfilePage";
import MoviePage from "components/movie_page/MoviePage";
import OrderPage from "components/order_page/OrderPage";
import Header from "components/header/Header";
import Footer from "components/footer/Footer";
import NotFoundPage from "components/not_found_page/NotFoundPage";
import useProfileModel from "services/hooks/useProfileModel";
import Box from "@material-ui/core/Box";
import useMovies from "services/hooks/useMovies";
import createBackendService from "services/backend_service/createBackendService";
import RedirectComponent from "components/common/RedirectComponent";

function App() {
    const [rootState, changeRootUserState] = useState(undefined);

    const profile = useProfileModel(rootState ? rootState.token : null);
    const movies = useMovies();

    const loginCallback = (loginModel) => {
        localStorage.setItem("token", loginModel.token);
        localStorage.setItem("username", loginModel.username);
        changeRootUserState(Object.assign({}, rootState, {
            token: loginModel.token,
            username: loginModel.username,
            redirect: "/"
        }));
    };

    const redirectCallback = (link) => {
        if (rootState) {
            changeRootUserState(Object.assign({}, rootState, {
                redirect: link
            }));
        }
    };

    const cachedLogoutCallback = useCallback(() => {
        if (rootState) {
            createBackendService().logout(rootState.token).then(() => {
                localStorage.removeItem("token");
                localStorage.removeItem("username");
                changeRootUserState(undefined);
            });
        }
    }, [rootState]);

    useEffect(() => {
        if (rootState === undefined) {
            const storedUsername = localStorage.getItem("username");
            const storedToken = localStorage.getItem("token");
            changeRootUserState(Object.assign({}, rootState, {
                username: storedUsername,
                token: storedToken,
                redirect: undefined
            }));
        }
        else if (profile === null) {
            cachedLogoutCallback();
        }
    }, [rootState, cachedLogoutCallback, changeRootUserState, profile]);

    return (
        <Box width={1}>
            <Router>
                <RedirectComponent link={rootState ? rootState.redirect : undefined} redirectCallback={redirectCallback}/>
                <Header
                    isUserLoggedIn={rootState && rootState.token !== null}
                    username={rootState ? rootState.username : ""}
                    redirectCallback={redirectCallback}
                    logoutCallback={cachedLogoutCallback}
                />
                <Switch>
                    <Route exact path="/">
                        <HomePage 
                            movies={movies}
                        />
                    </Route>
                    <Route path="/login">
                        <LoginPage 
                            loginCallback={loginCallback}
                        />
                    </Route>
                    <Route path="/register">
                        <RegisterPage redirectCallback={redirectCallback}/>
                    </Route>
                    <Route path="/profile">
                        <ProfilePage 
                            token={rootState ? rootState.token : null}
                            profileModel={profile}
                        />
                    </Route>
                    <Route path="/movie" component={MoviePage} />
                    <Route path="/order" render={(props) => 
                        <OrderPage {...props} token={rootState ? rootState.token : null} logoutCallback={cachedLogoutCallback}/>} />
                    <Route component={NotFoundPage} />
                </Switch>
                <Footer/>
            </Router>
        </Box>
    );
}

export default App;