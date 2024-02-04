using Microsoft.OpenApi.Models;
using Socially.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var policyName = "_myAllowSpecificOrigins";

// Add CORS policy
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

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

// Add IUserService/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIUserServiceGen();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoDB")
);

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPostService, PostsService>();

var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    // Optionally, set up other configurations like UI customization or passing parameters.
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseIUserService();
    app.UseIUserServiceUI();
}

app.UseCors(policyName);

// Add JWT middleware
app.Map("/api/Post", app =>
{
    app.UseMiddleware<JwtMiddleware>("my_very_super_super_long_and_very_secretive_secret_key");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
