using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Noted.Common;
using Noted.Data;
using Noted.Repositories;
using System.Text;
using System.Text.Json.Serialization;

var webAppBuilder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

webAppBuilder.Services.AddInfrastructure();

webAppBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("Noted")));

var section = webAppBuilder.Configuration.GetSection("JwtToken");
webAppBuilder.Services.Configure<JwtTokenConfig>(section);
var jwtTokenConfig = section.Get<JwtTokenConfig>()!;

webAppBuilder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

webAppBuilder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidAudience = jwtTokenConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

webAppBuilder.Services.AddAuthorization();

webAppBuilder.Services.AddControllers();
webAppBuilder.Services.AddEndpointsApiExplorer();
webAppBuilder.Services.AddSwaggerGen();
if (webAppBuilder.Environment.IsDevelopment())
{
    webAppBuilder.Services.AddEndpointsApiExplorer();
    webAppBuilder.Services.AddSwaggerGen(setup =>
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        setup.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
    });
}
var app = webAppBuilder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); //This needs to set everything allowed

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
