using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();
SeedDatabase();
app.MapControllers();

app.Run();

void SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        if (categoryService.GetAllAsync().Result.Data.Count < 2)
        {
            categoryService.CreateAsync(new CategoryDto { Name = "Asp.net Core Kursu" }).Wait();
            categoryService.CreateAsync(new CategoryDto { Name = "Asp.net Core API Kursu" }).Wait();
        }
    }
}