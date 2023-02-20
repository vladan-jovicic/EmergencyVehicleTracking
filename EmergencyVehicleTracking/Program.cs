using System.Text;
using EmergencyVehicleTracking.DataAccess.Driver;
using EmergencyVehicleTracking.DataAccess.DriverVehicle;
using EmergencyVehicleTracking.DataAccess.Patient;
using EmergencyVehicleTracking.DataAccess.Requests;
using EmergencyVehicleTracking.DataAccess.User;
using EmergencyVehicleTracking.DataAccess.Vehicle;
using EmergencyVehicleTracking.Models.Config;
using EmergencyVehicleTracking.Models.Mapper;
using EmergencyVehicleTracking.Services;
using EmergencyVehicleTracking.Services.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// security
var jwtSecretToken = builder.Configuration.GetValue<string>($"{nameof(AuthenticationOptions)}:{nameof(AuthenticationOptions.JwtSecurityKey)}");
var jwtSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretToken));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = jwtSecurityKey,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1) // TBD if necessary
    };
});


// infrastructure
builder.Services.AddControllers();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// data layer
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
builder.Services.AddSingleton<IVehicleRepository, InMemoryVehicleRepository>();
builder.Services.AddSingleton<IDriverRepository, InMemoryDriverRepository>();
builder.Services.AddSingleton<IPatientRequestRepository, InMemoryPatientRequestRepository>();
builder.Services.AddSingleton<IDriverVehicleMapRepository, InMemoryDriverVehicleMapRepository>();

// application layer
builder.Services.AddSingleton<IAuthorizationService, MockAuthorizationService>()
    .Configure<AuthenticationOptions>(builder.Configuration.GetRequiredSection(nameof(AuthenticationOptions)));
builder.Services.AddSingleton<PatientService>();
builder.Services.AddSingleton<VehicleService>();
builder.Services.AddSingleton<DriverService>();
builder.Services.AddSingleton<PatientRequestService>();

// API versioning
builder.Services.AddApiVersioning(opts =>
{
    opts.DefaultApiVersion = new ApiVersion(1, 0);
    opts.AssumeDefaultVersionWhenUnspecified = true;
    opts.ReportApiVersions = true;
    opts.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("x-api-version"));
});

// Swagger
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Use swagger only in development environment
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();