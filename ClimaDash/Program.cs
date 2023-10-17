namespace ClimaDash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Temp.GetData();
            string a = Temp.Data().Result;
            Console.WriteLine(a);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSignalR();
            builder.Services.AddHttpClient<WeatherService>();
            builder.Services.AddRazorPages();

            //CityData cityData = new CityData();

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

            app.MapRazorPages();

            //app.MapBlazorHub();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<WeatherHub>("/weatherHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}