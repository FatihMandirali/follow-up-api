using AutoMapper;
using Follow_Up.Application.Mapping;
using Follow_Up.Application.Middleware;
using Follow_Up.Application.Models.Options;
using Follow_Up.Application.Services.Interfaces;
using Follow_Up.Domain;
using Follow_Up.Infrastructure.Services;
using LocalizationSampleSingleResxFile;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

namespace Follow_Up.API.Extensions
{
    public static class ServiceCollectionExtensionsAPI
    {
        public static IServiceCollection AddApplicationLayerAPI(this IServiceCollection services, string connectionString)
        {
            #region Default
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            #endregion

            #region Swagger
            services.AddSwaggerGen();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
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

            return services;
        }

    }
}
