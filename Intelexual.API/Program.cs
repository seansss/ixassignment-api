using Microsoft.EntityFrameworkCore;
using Intelexual.Interface.IXProjects;
using Intelexual.DataService.Classes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


string postgresConnection = builder.Configuration.GetSection("Database").GetSection("Postgresql").GetSection("Connection").Value;

string migrationsAssembly = "Intelexual.API";

builder.Services.AddDbContext<Intelexual.Data.ProjectDbContext>(options => options.UseNpgsql(postgresConnection, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

builder.Services.AddTransient<IProjectService, ProjectService>((ctx) =>
{
    return new ProjectService(postgresConnection);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
Intelexual.API.Migrations.SeedData seedData = new Intelexual.API.Migrations.SeedData();
seedData.MigratedAndSeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");
app.UseAuthorization();

app.MapControllers();
app.Run();




