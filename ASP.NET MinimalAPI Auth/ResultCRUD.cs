using MinimalJwt.Models;

namespace MinimalJwt.Services
{
    public class ResultCRUD
    {
        // MOVIE SERVICE CRUD
        public static IResult Create(Movie movie, IMovieService service)
        {
            var result = service.Create(movie);
            return Results.Ok(result);
        }

        public static IResult Get(int id, IMovieService service)
        {
            var movie = service.Get(id);

            if (movie is null) return Results.NotFound("Movie not found");

            return Results.Ok(movie);
        }

        public static IResult List(IMovieService service)
        {
            var movies = service.List();

            return Results.Ok(movies);
        }


        public static IResult Update(Movie newMovie, IMovieService service)
        {
            var updatedMovie = service.Update(newMovie);

            if (updatedMovie is null) Results.NotFound("Movie not found");

            return Results.Ok(updatedMovie);
        }

        public static IResult Delete(int id, IMovieService service)
        {
            var result = service.Delete(id);

            if (!result) Results.BadRequest("Something went wrong");

            return Results.Ok(result);
        }

    }
}
