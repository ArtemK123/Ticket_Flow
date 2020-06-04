const createBackendService = () => ({
    async login(loginRequestModel) {
        return fetch("http://localhost:8080/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(loginRequestModel)
        });
    },
    async register(registerRequestModel) {
        return fetch("http://localhost:8080/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(registerRequestModel)
        });
    },
    async logout(token) {
        return fetch("http://localhost:8080/logout", {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token
        });
    },
    async getProfile(token) {
        return fetch("http://localhost:8080/profile", {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token
        });
    },
    async getTicketsByMovie(movieId) {
        return fetch(`http://localhost:8080/tickets/by-movie/${movieId}`, {
            method: "GET"
        });
    },
    async getTicketsByUser(token) {
        return fetch("http://localhost:8080/tickets/by-user", {
            method: "POST",
            headers: {
                "Content-Type": "text/plain"
            },
            body: token
        });
    },
    async getMovies() {
        return fetch("http://localhost:8080/movies", {
            method: "GET"
        });
    },
    async getMovieById(id) {
        return fetch(`http://localhost:8080/movies/${id}`, {
            method: "GET"
        });
    }
});

export default createBackendService;