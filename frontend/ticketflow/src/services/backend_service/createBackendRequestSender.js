const createBackendRequestSender = (referrer_policy, getBackendUrlBaseAsyncFunc) => {
    return {
        async login(loginRequestModel) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/login`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(loginRequestModel),
                referrerPolicy: referrer_policy
            });
        },
        async register(registerRequestModel) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/register`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(registerRequestModel),
                referrerPolicy: referrer_policy
            });
        },
        async logout(token) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/logout`, {
                method: "POST",
                headers: {
                    "Content-Type": "text/plain"
                },
                body: token,
                referrerPolicy: referrer_policy
            });
        },
        async getProfile(token) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/profile`, {
                method: "POST",
                headers: {
                    "Content-Type": "text/plain"
                },
                body: token,
                referrerPolicy: referrer_policy
            });
        },
        async getTicketsByMovie(movieId) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/tickets/by-movie/${movieId}`, {
                method: "GET",
                referrerPolicy: referrer_policy
            });
        },
        async getTicketsByUser(token) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/tickets/by-user`, {
                method: "POST",
                headers: {
                    "Content-Type": "text/plain"
                },
                body: token,
                referrerPolicy: referrer_policy
            });
        },
        async order(orderModel) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/tickets/order`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(orderModel),
                referrerPolicy: referrer_policy
            });
        },
        async getMovies() {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/movies`, {
                method: "GET",
                referrerPolicy: referrer_policy
            });
        },
        async getMovieById(id) {
            return fetch(`${await getBackendUrlBaseAsyncFunc()}/movies/${id}`, {
                method: "GET",
                referrerPolicy: referrer_policy
            });
        }
    };
};

export default createBackendRequestSender;