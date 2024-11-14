using WebApp.Models;
using WebApp.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper; // Added for AutoMapper
using WebApp.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add support for controllers and views
builder.Services.AddControllersWithViews();

// Configure DbContext for SQLite
builder.Services.AddDbContext<RecruitmentAgencyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));


// Register repositories for dependency injection
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();
builder.Services.AddScoped<IRecruiterRepository, RecruiterRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

// Add AutoMapper and specify the assembly for mapping profiles
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger only in development mode
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // "swagger" to make it accessible at /swagger
    });
}
else
{
    // Configure error handling for production environments
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HSTS for better security
}

// Add middleware

app.UseHttpsRedirection(); // Enforces HTTPS
app.UseStaticFiles(); // Serve static files (like CSS, JS, images)

app.UseRouting(); // Enables routing for the application

// Enable CORS before authorization
app.UseCors("AllowAllOrigins");

// Add Authorization middleware
app.UseAuthorization();

// Configure the routing for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();