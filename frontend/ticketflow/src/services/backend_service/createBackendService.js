const backendLink = "http://localhost:8080";
const referrerPolicy = "no-referrer-when-downgrade";

const createBackendService = () => ({
    async login(loginRequestModel) {
        return fetch(`${backendLink}/login`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(loginRequestModel),
            referrerPolicy: referrerPolicy
        });
    },
    async register(registerRequestModel) {
        return fetch(`${backendLink}/register`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(registerRequestModel),
            referrerPolicy: referrerPolicy
        });
    },
    async logout(token) {
        return fetch(`${backendLink}/logout`, {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token,
            referrerPolicy: referrerPolicy
        });
    },
    async getProfile(token) {
        return fetch(`${backendLink}/profile`, {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token,
            referrerPolicy: referrerPolicy
        });
    },
    async getTicketsByMovie(movieId) {
        return fetch(`${backendLink}/tickets/by-movie/${movieId}`, {
            method: "GET",
            referrerPolicy: referrerPolicy
        });
    },
    async getTicketsByUser(token) {
        return fetch(`${backendLink}/tickets/by-user`, {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token,
            referrerPolicy: referrerPolicy
        });
    },
    async order(orderModel) {
        return fetch(`${backendLink}/tickets/order`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(orderModel),
            referrerPolicy: referrerPolicy
        });
    },
    async getMovies() {
        return fetch(`${backendLink}/movies`, {
            method: "GET",
            referrerPolicy: referrerPolicy
        });
    },
    async getMovieById(id) {
        return fetch(`${backendLink}/movies/${id}`, {
            method: "GET",
            referrerPolicy: referrerPolicy
        });
    }
});

export default createBackendService;