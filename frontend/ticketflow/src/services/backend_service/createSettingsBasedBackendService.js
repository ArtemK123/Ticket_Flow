const createSettingsBasedBackendService = (referrer_policy) => {
    // eslint-disable-next-line no-undef
    const backendLink = process.env.REACT_APP_API_GATEWAY_URL;

    return {
        async login(loginRequestModel) {
            return fetch(`${backendLink}/login`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(loginRequestModel),
                referrerPolicy: referrer_policy
            });
        },
        async register(registerRequestModel) {
            return fetch(`${backendLink}/register`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(registerRequestModel),
                referrerPolicy: referrer_policy
            });
        },
        async logout(token) {
            return fetch(`${backendLink}/logout`, {
                method: "POST",
                headers: {
                    "Content-Type": "text/plain"
                },
                body: token,
                referrerPolicy: referrer_policy
            });
        },
        async getProfile(token) {
            return fetch(`${backendLink}/profile`, {
                method: "POST",
                headers: {
                    "Content-Type": "text/plain"
                },
                body: token,
                referrerPolicy: referrer_policy
            });
        },
        async getTicketsByMovie(movieId) {
            return fetch(`${backendLink}/tickets/by-movie/${movieId}`, {
                method: "GET",
                referrerPolicy: referrer_policy
            });
        },
        async getTicketsByUser(token) {
            return fetch(`${backendLink}/tickets/by-user`, {
                method: "POST",
                headers: {
                    "Content-Type": "text/plain"
                },
                body: token,
                referrerPolicy: referrer_policy
            });
        },
        async order(orderModel) {
            return fetch(`${backendLink}/tickets/order`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(orderModel),
                referrerPolicy: referrer_policy
            });
        },
        async getMovies() {
            return fetch(`${backendLink}/movies`, {
                method: "GET",
                referrerPolicy: referrer_policy
            });
        },
        async getMovieById(id) {
            return fetch(`${backendLink}/movies/${id}`, {
                method: "GET",
                referrerPolicy: referrer_policy
            });
        }
    };
};

export default createSettingsBasedBackendService;