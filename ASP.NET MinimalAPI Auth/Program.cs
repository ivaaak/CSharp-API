using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MinimalJwt.Models;
using MinimalJwt.Services;

var builder = WebApplication.CreateBuilder(args);

//Add Swagger with security settings, add JWT auth + services
builder.Services.AddSwaggerWithSecurity();
builder.Services.AddAuthenticationAuthorization(builder);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();




// Mapping MinimapAPI Routes
app.MapGet("/", () => "Hello World!")
    .ExcludeFromDescription();

// Post - /login /create
app.MapPost("/login",
(UserLogin user, IUserService service) => ResultAuth.Login(user, service, builder))
    .Accepts<UserLogin>("application/json")
    .Produces<string>();

app.MapPost("/create",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(Movie movie, IMovieService service) => ResultCRUD.Create(movie, service))
    .Accepts<Movie>("application/json")
    .Produces<Movie>(statusCode: 200, contentType: "application/json");

// Get - /get /list
app.MapGet("/get",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
(int id, IMovieService service) => ResultCRUD.Get(id, service))
    .Produces<Movie>();

app.MapGet("/list",
    (IMovieService service) => ResultCRUD.List(service))
    .Produces<List<Movie>>(statusCode: 200, contentType: "application/json");

//Misc - /update /delete
app.MapPut("/update",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(Movie newMovie, IMovieService service) => ResultCRUD.Update(newMovie, service))
    .Accepts<Movie>("application/json")
    .Produces<Movie>(statusCode: 200, contentType: "application/json");

app.MapDelete("/delete",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(int id, IMovieService service) => ResultCRUD.Delete(id, service));



app.UseSwaggerUI();
app.Run();
