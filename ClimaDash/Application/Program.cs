using ClimaDash.Services;
using ClimaDash.Services.Implementations;
using ClimaDash.Services.Implementations.WeatherImplementations;
using ClimaDash.Services.WeatherServices;

namespace ClimaDash.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Things to do:
            //_ViewImports.razor / global using
            //Weather.razor cleanup
            //WeatherController API
            //remove CityInfo
            //WeatherHub
            //Routes
            //add tag helpers to index
            //CSS
            //Dockerise
            //Blazorise, BlazorModal, or other custom implementations

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSignalR();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<ICityService, CityService>();
            builder.Services.AddHttpClient<CityService>();

            builder.Services.AddSingleton<ICityPictureService, CityPictureService>(provider =>
            {
                var apiKey = Environment.GetEnvironmentVariable("PEXELS_API_KEY");

                return new CityPictureService(apiKey, new CityPictureCache());
            });
            builder.Services.AddHttpClient<CityPictureService>();

            builder.Services.AddSingleton<ICountryCodeService, CountryCodeService>();
            builder.Services.AddHttpClient<CountryCodeService>();

            builder.Services.AddSingleton<ICountryFlagService, CountryFlagService>();
            builder.Services.AddHttpClient<CountryFlagService>();

            builder.Services.AddSingleton<IUserSettingsService, UserSettingsService>();
            builder.Services.AddHttpClient<UserSettingsService>();

            builder.Services.AddSingleton<IWeatherService, WeatherService>();
            builder.Services.AddHttpClient<WeatherService>();

            builder.Services.AddSingleton<ICurrentWeatherService, CurrentWeatherService>();
            builder.Services.AddHttpClient<CurrentWeatherService>();

            builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddHttpClient<WeatherForecastService>();

            builder.Services.AddSingleton<IAirPollutionService, AirPollutionService>();
            builder.Services.AddHttpClient<AirPollutionService>();

            builder.Services.AddSingleton<IWeatherMapService, WeatherMapService>();
            builder.Services.AddHttpClient<WeatherMapService>();
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.MapRazorPages();

            app.MapBlazorHub();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHub<WeatherHub>("/weatherHub");
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.Run();
        }
    }
}