using AutoMapper;
using Follow_Up.Application.Helpers.JWT;
using Follow_Up.Application.Mapping;
using Follow_Up.Application.Middleware;
using Follow_Up.Application.Models.Options;
using Follow_Up.Application.Services.Interfaces;
using Follow_Up.Domain;
using Follow_Up.Infrastructure.Services;
using LocalizationSampleSingleResxFile;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Follow_Up.API.Extensions
{
    public static class ServiceCollectionExtensionsAPI
    {
        public static IServiceCollection AddApplicationLayerAPI(this IServiceCollection services, string connectionString, IConfiguration configuration)
        {

            #region Default
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            #endregion

            #region Cors
            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));
            #endregion

            #region Swagger
            services.AddSwaggerGen();
            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JSON Web Token based security",
            };

            var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};

            var contact = new OpenApiContact()
            {
                Name = "Mohamad Lawand",
                Email = "hello@mohamadlawand.com",
                Url = new Uri("http://www.mohamadlawand.com")
            };

            var license = new OpenApiLicense()
            {
                Name = "Free License",
                Url = new Uri("http://www.mohamadlawand.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Minimal API - JWT Authentication with Swagger demo",
                Description = "Implementing JWT Authentication in Minimal API",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", info);
                o.AddSecurityDefinition("Bearer", securityScheme);
                o.AddSecurityRequirement(securityReq);
            });
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            #endregion

            #region AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AddProfile(new FollowUpProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region Mediatr
            services.AddMediatR(typeof(Follow_Up.Application.Enums.ProcessStatusEnum).Assembly);
            #endregion

            #region MsSql
            services.AddDbContext<FollowUpDbContext>(x =>
            {
                x.UseSqlServer(connectionString,
                    option =>
                    {
                        option.MigrationsAssembly(Assembly.GetAssembly(typeof(FollowUpDbContext)).GetName().Name);
                    });
            });
            #endregion

            #region Multi-Language
            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("tr")
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "tr", uiCulture: "tr");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new[] { new RouteDataRequestCultureProvider { IndexOfCulture = 1, IndexofUICulture = 1 } };
                });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });
            #endregion

            #region ExceptionService
            services.AddScoped<ExceptionCatcherMiddleware>();
            #endregion

            #region Authentication
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true, //expariotion for isrequired
                    ValidateIssuerSigningKey = true, //expariotion for isrequired
                    RequireExpirationTime = true, //expariotion for isrequired
                    ClockSkew = TimeSpan.Zero //expariotion for isrequired
                };
            });

            services.AddAuthorization();
            #endregion

            return services;
        }

    }
}
