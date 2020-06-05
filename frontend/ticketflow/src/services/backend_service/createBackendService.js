const backendLink = "http://localhost:8080";

const createBackendService = () => ({
    async login(loginRequestModel) {
        return fetch(`${backendLink}/login`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(loginRequestModel)
        });
    },
    async register(registerRequestModel) {
        return fetch(`${backendLink}/register`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(registerRequestModel)
        });
    },
    async logout(token) {
        return fetch(`${backendLink}/logout`, {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token
        });
    },
    async getProfile(token) {
        return fetch(`${backendLink}/profile`, {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token
        });
    },
    async getTicketsByMovie(movieId) {
        return fetch(`${backendLink}/tickets/by-movie/${movieId}`, {
            method: "GET"
        });
    },
    async getTicketsByUser(token) {
        return fetch(`${backendLink}/tickets/by-user`, {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token
        });
    },
    async order(orderModel) {
        return fetch(`${backendLink}/tickets/order`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(orderModel)
        });
    },
    async getMovies() {
        return fetch(`${backendLink}/movies`, {
            method: "GET"
        });
    },
    async getMovieById(id) {
        return fetch(`${backendLink}/movies/${id}`, {
            method: "GET"
        });
    }
});

export default createBackendService;