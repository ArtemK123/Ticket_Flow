package com.ticketflow.api_gateway.service.seeders;

import java.time.LocalDate;
import java.util.List;

import com.ticketflow.api_gateway.models.movie_service.Film;
import com.ticketflow.api_gateway.proxy.movie.films_api.FilmsApiProxy;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class FilmsSeeder {
    private FilmsApiProxy filmsApiProxy;
    
    @Autowired
    public FilmsSeeder(FilmsApiProxy filmsApiProxy) {
        this.filmsApiProxy = filmsApiProxy;
    }

    private List<Film> dataToSeed = List.of(
        new Film("Terminator", "I will be back", LocalDate.of(1984, 11, 26), "James Cameron", 107, 13),
        new Film("Star wars - A New Hope", "In the galaxy far far away...", LocalDate.of(1977, 5, 25), "George Lucas", 121, 8),
        new Film("Deadpool", "X gonna give it to ya", LocalDate.of(2016, 2, 8), "20th Century Fox", 108 , 18),
        new Film(
            "The Godfather",
            "Now you come to me and you say 'Don Corleone, give me justice', but you don't even ask with respect. " +
            "You don't offer friendship. You don't even think to call me Godfather",
            LocalDate.of(1972, 3, 14),
            "Paramount Pictures", 
            177,
            13),
        new Film("The Good, the Bad and the Ugly", "The best trio", LocalDate.of(1966, 12, 23), "Sergio Leone", 177, 13),
        new Film("Fight Club", "Where is my mind? In the water, see it swimming", LocalDate.of(1999, 9, 10), "David Fincher", 139 , 16),
        new Film("The matrix", "The spoon is a lie!", LocalDate.of(1999, 3, 31), "The Wachowskis", 136 , 13),
        new Film("Parasite", "This film will impress you", LocalDate.of(2019, 5, 29), "CJ Entertainment", 132 , 13),
        new Film("Forrest Gump", "Run, Forrest, Run", LocalDate.of(1994, 6, 23), "Robert Zemeckis", 142, 0),
        new Film("Back in future", "you built a time machine..... out of a delorian ?", LocalDate.of(1985, 7, 3), "Robert Zemeckis", 116 , 0)
    );

    public void seed() {
        if (filmsApiProxy.getAll().isEmpty()) {
            dataToSeed.forEach(filmsApiProxy::add);
        }
    }
}