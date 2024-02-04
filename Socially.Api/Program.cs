using Socially.Application.Services.Users;
using Socially.Domain.Models;


var builder = WebApplication.CreateBuilder(args);
var  policyName = "_myAllowSpecificOrigins";


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

builder.Services.AddTransient<IUserService,UserService>();



var app = builder.Build();

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
