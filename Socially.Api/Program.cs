using Socially.Application.Services.Comments;
using Socially.Application.Services.Likes;
using Socially.Application.Services.Posts;
using Socially.Application.Services.Users;
using Socially.Domain.Models;
using Socially.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
var policyName = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://localhost:5173")
                            //.AllowAnyOrigin()
                            .WithMethods("GET", "POST", "PUT", "DELETE")
                            .AllowAnyHeader();
                      });
});



// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoDB")
);

// Inside ConfigureServices method of Startup.cs


builder.Services.AddControllers();





builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPostService, PostsService>();
builder.Services.AddTransient<ILikesService, LikesService>();
builder.Services.AddTransient<ICommentsService, CommentsService>();



var app = builder.Build();

// app.Map("/api/post/create", app =>
// {
//     app.UseMiddleware<JwtMiddleware>("my_very_super_super_long_and_very_secretive_secret_key");
    
// });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policyName);





app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
