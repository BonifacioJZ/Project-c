using Microsoft.EntityFrameworkCore;
using Persistence;
using AutoMapper;
using App.Interfaces;
using App.Services;
using App.validations;
using FluentValidation;
using Domain.Dto.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//AutoMapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Db Configuration
builder.Services.AddDbContext<DbCtx>();
/* This code block is configuring the authentication and authorization settings for the application
using JWT (JSON Web Tokens). */
builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("secretKey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(config=>{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer= false,
        ValidateAudience = false
    };
});
builder.Services.AddControllers();
//validation configuration
builder.Services.AddScoped<IValidator<CategoryInDto>,CategoryValidation>();
builder.Services.AddScoped<IValidator<CategoryUpdate>,CategoryUpdateValidation>();
//Injection dependency
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductService,ProductService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/* The code block is creating a scope for dependency injection and retrieving the service provider from
the application's services. It then tries to get an instance of the `DbCtx` service from the service
provider and calls the `Migrate()` method on its `Database` property. This is used to apply any
pending migrations to the database. */
using(var environment = app.Services.CreateScope()){
    var service = environment.ServiceProvider;

    try{
        var context = service.GetRequiredService<DbCtx>();
        context.Database.Migrate();
    }catch(Exception e){
        var log = service.GetRequiredService<ILogger<Program>>();
        log.LogError(e, "Error in the migration");
    }


}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
